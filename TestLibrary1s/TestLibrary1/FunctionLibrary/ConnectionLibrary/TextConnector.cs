using System;
using System.Collections.Generic;
using System.Text;
using TestLibrary1.Interfaces;
using TestLibrary1.Models;
using TestLibrary1.FunctionLibrary.ConnectionLibrary.TextConnectionHelper;
using System.Linq;

namespace TestLibrary1.FunctionLibrary.ConnectionLibrary
{
    public class TextConnector : IDataConnection
    {
        public void CompleteTournament(TournamentModel model)
        {
            //remove from actives
            List<TournamentModel> tournaments = GlobalConfig.TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels();

            tournaments.Remove(model);
            tournaments.SaveTournamentToFile(GlobalConfig.TournamentFile);
            MatchupHelper.UpdateTournamentResults(model);

            List<TournamentModel> finishedTournaments = GlobalConfig.FinishedTournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();
            finishedTournaments.Add(model);
            finishedTournaments.SaveTournamentToFile(GlobalConfig.FinishedTournamentsFile);

        }

        public void CreatePerson(PersonModel model)
        {
            List<PersonModel> people = GlobalConfig.PersonFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.id).First().id + 1;
            }

            model.id = currentId;

            people.Add(model);
            people.SaveToPersonFile();
        }

        public void CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.id).First().id + 1;
            }

            model.id = currentId;

            prizes.Add(model);
            prizes.SaveToPrizeFile();
        }

        public void CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
            int currentId = 1;

            if(teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.id).First().id + 1;
            }
            model.id = currentId;

            teams.Add(model);
            teams.SaveToTeamFile();
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GlobalConfig.TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels();
            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.id).First().id + 1;
            }
            model.id = currentId;

            model.SaveRoundsToFile();

            tournaments.Add(model);
            tournaments.SaveTournamentToFile(GlobalConfig.TournamentFile);
            MatchupHelper.UpdateTournamentResults(model);
        }

        public List<PersonModel> GetPerson_All()
        {
            return GlobalConfig.PersonFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            return GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }

        public List<TournamentModel> GetTournament_All()
        {
            return GlobalConfig.TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            model.UpdateMatchupToFile();
        }
    }
}
