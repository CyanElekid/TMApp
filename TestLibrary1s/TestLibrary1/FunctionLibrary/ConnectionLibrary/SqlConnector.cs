using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TestLibrary1.Interfaces;
using TestLibrary1.Models;

namespace TestLibrary1.FunctionLibrary.ConnectionLibrary
{
    public class SqlConnector : IDataConnection
    {

        private const string db = "Tournaments";

        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var person = new DynamicParameters();
                person.Add("@FirstName", model.FirstName);
                person.Add("@LastName", model.LastName);
                person.Add("@EmailAddress", model.EmailAddress);
                person.Add("@CellPhoneNumber", model.CellPhoneNumber);
                person.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPerson_Insert", person, commandType: CommandType.StoredProcedure);

                model.id = person.Get<int>("@id");
            }
        }

        public void CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var prize = new DynamicParameters();
                prize.Add("@PlaceNumber", model.PlaceNumber);
                prize.Add("@PlaceName", model.PlaceName);
                prize.Add("@PrizeAmount", model.PrizeAmount);
                prize.Add("@PrizePercentage", model.PrizePercentage);
                prize.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", prize, commandType: CommandType.StoredProcedure);

                model.id = prize.Get<int>("@id");
            }
        }

        public void CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var team = new DynamicParameters();
                team.Add("@TeamName", model.TeamName);
                team.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", team, commandType: CommandType.StoredProcedure);

                model.id = team.Get<int>("@id");

                foreach (PersonModel member in model.TeamMembers)
                {
                    team = new DynamicParameters();
                    team.Add("@TeamId", model.id);
                    team.Add("@PersonId", member.id);
                    connection.Execute("dbo.spTeamMembers_Insert", team, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var tm = new DynamicParameters();
                tm.Add("@TournamentName", model.TournamentName);
                tm.Add("@EntryFee", model.EntryFee);
                tm.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournaments_Insert", tm, commandType: CommandType.StoredProcedure);

                model.id = tm.Get<int>("@id");

                var p = new DynamicParameters();

                foreach (PrizeModel prize in model.Prizes)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", model.id);
                    p.Add("@PrizeId", prize.id);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
                }

                foreach (TeamModel team in model.EnteredTeams)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", model.id);
                    p.Add("@TeamId", team.id);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("dbo.spTournamentEntires_Insert", p, commandType: CommandType.StoredProcedure);
                }

                SaveTournamentRounds(connection, model);
                MatchupHelper.UpdateTournamentResults(model);

            }
        }

            public List<PersonModel> GetPerson_All()
            {
                List<PersonModel> output;
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<PersonModel>("dbo.spPerson_GetAll").ToList();
                }
                return output;
            }

            public List<TeamModel> GetTeam_All()
            {
                List<TeamModel> output;
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<TeamModel>("dbo.spTeams_GetAll").ToList();

                    foreach (TeamModel team in output)
                    {
                        var members = new DynamicParameters();
                        members.Add("@TeamId", team.id);

                        team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", members, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return output;
            }

            public List<TournamentModel> GetTournament_All()
            {
                List<TournamentModel> output;
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<TournamentModel>("dbo.spTournament_GetAll").ToList();
                    foreach(TournamentModel t in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TournamentId", t.id);
                    t.Prizes = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();
                    t.EnteredTeams = connection.Query<TeamModel>("dbo.spTeams_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                    foreach(TeamModel team in t.EnteredTeams)
                    {
                       team.TeamMembers = GetMembersByTeamID(team.id, connection);
                    }

                    //start from 17h ish in if this doesn't work
                    List<MatchupModel> allMatchups = new List<MatchupModel>();
                    p = new DynamicParameters();
                    p.Add("@TournamentId", t.id);
                    // Populates values gotten from database
                    
                    List<dynamic> dbMatchups = connection.Query<dynamic>("dbo.spMatchups_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();
                    foreach(dynamic matchup in dbMatchups)
                    {
                        MatchupModel current = new MatchupModel();
                        if(matchup.WinnerId != null && matchup.WinnerId != 0)
                        {
                            current.Winner = GetTeam_ById(matchup.WinnerId, connection);
                        }
                        current.id = matchup.id;
                        current.MatchupRound = matchup.MatchupRound;
                        allMatchups.Add(current);
                    }

                    //populates matchupentries
                    foreach(MatchupModel matchup in allMatchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", matchup.id);
                        //get  ids from competing teams and parent matchups
                        //List<int> teamsCompeting = connection.Query<int>("dbo.spMatchupEntries_GetTeamIdByMatchupId", p, commandType: CommandType.StoredProcedure).ToList();
                        //List<int> parentMatchups = connection.Query<int>("dbo.spMatchupEntries_GetParentIdByMatchupId", p, commandType: CommandType.StoredProcedure).ToList();
                        List<dynamic> matchupEntries = connection.Query<dynamic>("dbo.spMatchupEntries_GetByMatchupId", p, commandType: CommandType.StoredProcedure).ToList();
                        List<MatchupEntryModel> entries = new List<MatchupEntryModel>();
                        foreach(dynamic entry in matchupEntries)
                        {
                            MatchupEntryModel current = new MatchupEntryModel();
                            current.id = entry.id;

                            if (entry.TeamCompetingId != null && entry.TeamCompetingId != 0)
                            {
                                current.TeamCompeting = GetTeam_ById(entry.TeamCompetingId, connection); 
                            }
                            if(entry.score != null)
                            {
                                current.Score = entry.Score;
                            }
                            if (entry.ParentMatchupId != null)
                            {
                                current.ParentMatchup = GetMatchup_ById(entry.ParentMatchupId, connection);
                            }
                            entries.Add(current);
                        }
                        matchup.Entries = entries;


                        //Store rest of the data from database
                        //matchup.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchupId", p, commandType: CommandType.StoredProcedure).ToList();

                        ////Populate teams competing
                        //for(int i = 0; i < teamsCompeting.Count; i++)
                        //{
                        //    if (teamsCompeting[i] > 0)
                        //    {
                        //        matchup.Entries[i].TeamCompeting = GetTeam_ById(teamsCompeting[i], connection);
                        //    }
                        //}

                        ////populate parent matchups, should be null if nothin is there
                        //for (int i = 0; i < parentMatchups.Count; i++)
                        //{
                        //    if (parentMatchups[i] > 0)
                        //    {
                        //        matchup.Entries[i].ParentMatchup = allMatchups.Where(x => x.id == parentMatchups[i]).First();
                        //    }
                        //}
                    }

                    //rounds is null, so rounds.count starts at 0...
                    int numberOfRounds = 1;
                    foreach(MatchupModel m in allMatchups)
                    {
                        if (m.MatchupRound > numberOfRounds)
                        {
                            numberOfRounds = m.MatchupRound;
                        }
                    }
                    for (int i = 1; i <= numberOfRounds; i++)
                    {
                        List<MatchupModel> round = new List<MatchupModel>();
                        foreach(MatchupModel matchup in allMatchups)
                        {
                            if (matchup.MatchupRound == i)
                            {
                                round.Add(matchup);
                            }
                        }
                        t.Rounds.Add(round);
                    }
                }
                }
                return output;
            }

            private void SaveTournamentRounds(IDbConnection connection, TournamentModel model)
            {
                foreach(List<MatchupModel> round in model.Rounds)
                {
                    foreach( MatchupModel matchup in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@TournamentId", model.id);
                    p.Add("@MatchupRound", matchup.MatchupRound);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                    matchup.id = p.Get<int>("@id");

                    foreach(MatchupEntryModel entry in matchup.Entries)
                    {
                        var en = new DynamicParameters();
                        en.Add("@MatchupId", matchup.id);
                        if (entry.ParentMatchup == null)
                        {
                            en.Add("@ParentMatchupId", null);
                        }
                        else en.Add("@ParentMatchupId", entry.ParentMatchup.id);
                        if(entry.TeamCompeting == null)
                        {
                            en.Add("@TeamCompetingId", null);
                        }
                        else en.Add("@TeamCompetingId", entry.TeamCompeting.id);
                        en.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        connection.Execute("dbo.spMatchupEntries_Insert", en, commandType: CommandType.StoredProcedure);
                    }
                }
                }
            }

        private static List<PersonModel> GetMembersByTeamID(int teamId, IDbConnection connection)
        {
            var p = new DynamicParameters();
            p.Add("@TeamId", teamId);
            return connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
        }

        private static TeamModel GetTeam_ById(int teamId, IDbConnection connection)
        {
            var p = new DynamicParameters();
            p.Add("@id", teamId);
            TeamModel output =  connection.Query<TeamModel>("dbo.spTeams_GetById", p, commandType: CommandType.StoredProcedure).First();
            output.TeamMembers = GetMembersByTeamID(teamId, connection);

            return output;
        }

        private static MatchupModel GetMatchup_ById(int matchupId, IDbConnection connection)
        {
            var p = new DynamicParameters();
            p.Add("@id", matchupId);
            MatchupModel output = connection.Query<MatchupModel>("dbo.spMatchup_GetById", p, commandType: CommandType.StoredProcedure).First();

            return output;
        }

        public void UpdateMatchup(MatchupModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@id", model.id);
                p.Add("@WinnerId", model.Winner.id);
                connection.Execute("dbo.spMatchups_Update", p, commandType: CommandType.StoredProcedure);

                //TODO: get matchup who has this as parent, add winner as entry
               

                foreach(MatchupEntryModel entry in model.Entries)
                {
                    if ( entry.TeamCompeting != null)
                    {
                        p = new DynamicParameters();
                        p.Add("@id", entry.id);
                        p.Add("@TeamCompetingId", entry.TeamCompeting.id);
                        p.Add("@Score", entry.Score);
                        connection.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure); 
                    }
                    if(entry.TeamCompeting == model.Winner)
                    {
                        // get the next matchup and insert winner into it
                        p = new DynamicParameters();
                        p.Add("@ParentMatchupId", model.id);
                        dynamic nextMatch = connection.Query("dbo.spMatchupEntries_GetByParentMatchupId", p, commandType: CommandType.StoredProcedure).First();

                        p = new DynamicParameters();
                        p.Add("@id", nextMatch.id);
                        p.Add("@TeamCompetingId", model.Winner.id);
                        p.Add("@Score", nextMatch.Score);
                        connection.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        public void CompleteTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                    var p = new DynamicParameters();
                    p.Add("@id", model.id);
                    connection.Execute("dbo.spTournaments_Complete", p, commandType: CommandType.StoredProcedure);
            }
        }

        //TODO: refactor above into this

        //private void UpdateMatchupEntry(MatchupEntryModel model, IDbConnection connection)
        //{

        //}
    }
}