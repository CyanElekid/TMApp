using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TestLibrary1.Models;

namespace TestLibrary1.FunctionLibrary
{
    public static class MatchupHelper
    {

        //TODO: teamsperround a property and stuff
        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count, 2);
            int byes = NumberByes(rounds, randomizedTeams.Count, 2);
            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams, 2));
            CreateRoundsAfterFirst(model, rounds, 2);

           // UpdateTournamentResults(model);

        }

        private static void CreateRoundsAfterFirst(TournamentModel model, int rounds, int teamsPerRound)
        {
            int roundNumber = 2;
            List<MatchupModel> currentRound = new List<MatchupModel>();
            List<MatchupModel> prevRound = model.Rounds[0];
            MatchupModel currentMatchup = new MatchupModel();

            while(roundNumber <= rounds)
            {
                foreach (MatchupModel matchup in prevRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = matchup });
                    if (currentMatchup.Entries.Count >= teamsPerRound)
                    {
                        currentMatchup.MatchupRound = roundNumber;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }
                model.Rounds.Add(currentRound);
                prevRound = currentRound;
                currentRound = new List<MatchupModel>();
                roundNumber += 1;
            }
        }

        private static int CheckCurrentRound(this TournamentModel model)
        {
            int output = 1;
            foreach(List<MatchupModel> round in model.Rounds)
            {
                if (round.All(x => x.Winner != null))
                {
                    output += 1;
                }
                //This assumes you go through in order, otherwise it fails
                else return output;

            }
            CompleteTournament(model);
            return output-1;
        }

        //only handles first and second place, and tournaments with 2 teams per matchup
        private static void CompleteTournament(TournamentModel model)
        {
            GlobalConfig.Connection.CompleteTournament(model);
            TeamModel winner = model.Rounds.Last().First().Winner;
            TeamModel second = model.Rounds.Last().First().Entries.Where(x => x.TeamCompeting != winner).First().TeamCompeting;

            decimal winnerPrize = 0;
            decimal secondPrize = 0;

            if(model.Prizes.Count > 0)
            {
                decimal totalIncome = model.EnteredTeams.Count * model.EntryFee;

                PrizeModel firstPlacePrize = model.Prizes.Where(x => x.PlaceNumber == 1).FirstOrDefault();
                PrizeModel secondPlacePrize = model.Prizes.Where(x => x.PlaceNumber == 2).FirstOrDefault();
                if (firstPlacePrize != null)
                {
                    winnerPrize = CalculatePayout(firstPlacePrize, totalIncome);
                }
                if (secondPlacePrize != null)
                {
                    secondPrize = CalculatePayout(secondPlacePrize, totalIncome);
                }
            }
        }

        private static decimal CalculatePayout(PrizeModel model, decimal income)
        {
            decimal output = 0;
            if(model.PrizeAmount > 0)
            {
                output = model.PrizeAmount;
            }
            else
            {
                output = Decimal.Multiply(Convert.ToDecimal(model.PrizePercentage / 100), income);
            }
            return output;
        }

        public static void UpdateTournamentResults(TournamentModel model)
        {
            int startingRound = model.CheckCurrentRound();
            List<MatchupModel> toScore = new List<MatchupModel>();


            foreach(List<MatchupModel> round in model.Rounds)
            {
                foreach(MatchupModel m in round)
                {
                    if(m.Winner == null && (m.Entries.Any(x => x.Score != 0) || m.Entries.Count == 1))
                    {
                        toScore.Add(m);
                    }
                }
            }
            MarkWinnerMatchups(toScore);
            AdvanceWinners(toScore, model);

            foreach (MatchupModel m in toScore)
            {
                GlobalConfig.Connection.UpdateMatchup(m); 
            }

            int endingRound = model.CheckCurrentRound();

            if (endingRound > startingRound)
            {
                model.AlertUsersNewRound();
                
            }

            //Equvelant to above 
            //toScore.ForEach(x=> GlobalConfig.Connection.UpdateMatchup(x));
        }

        public static void AlertUsersNewRound(this TournamentModel model)
        {
            int currentRoundNumber = model.CheckCurrentRound();
            //List<MatchupModel> currentRound = model.Rounds[currentRoundNumber-1]; <-- should work here, but below is 'safer'
            List<MatchupModel> currentRound = model.Rounds.Where(x => x.First().MatchupRound == currentRoundNumber).First();

            foreach(MatchupModel matchup in currentRound)
            {
                foreach(MatchupEntryModel m in matchup.Entries)
                {
                    foreach(PersonModel member in m.TeamCompeting.TeamMembers)
                    {
                        AlertPersonNewRound(member, m.TeamCompeting, matchup.Entries.Where(x=> x.TeamCompeting != m.TeamCompeting).ToList());
                    }
                }
            }
        }

        private static void AlertPersonNewRound(PersonModel person, TeamModel team, List<MatchupEntryModel> opponents)
        {
            if (person.EmailAddress.Length == 0)
            {
                return;
            }
            string subject = "";
            List<string> to = new List<string>();
            to.Add(person.EmailAddress);

            StringBuilder body = new StringBuilder();
            if (opponents != null)
            {
                subject = "Match with";
                foreach (MatchupEntryModel op in opponents)
                {
                    subject += $" {op.TeamCompeting.TeamName}";
                }

                body.AppendLine("<h1>You have a new matchup</h1>");
                if (opponents.Count > 1)
                {
                    body.Append("<p><strong> Opponents: </strong>");
                    foreach (MatchupEntryModel op in opponents)
                    {
                        body.Append(op.TeamCompeting.TeamName);
                    }
                }
                else
                {
                    body.Append("<p><strong> Opponent: </strong>");
                    body.Append(opponents.First().TeamCompeting.TeamName);
                }
            }
            else
            {
                subject = "Bye Round";
                body.Append("<p>You have a bye this round.");
                body.AppendLine("Please wait for the next round :).</p>");
            }


            body.AppendLine();
            body.AppendLine("</p>");


            EmailHelper.SendEmail(to, subject, body.ToString());
        }

        //TODO : implement for multiple teams. order entries by score, grab highest and mark as winner

        private static void AdvanceWinners(List<MatchupModel> models, TournamentModel tournament)
        {
            foreach (MatchupModel m in models)
            {
                foreach (List<MatchupModel> round in tournament.Rounds)
                {
                    foreach (MatchupModel rm in round)
                    {
                        foreach (MatchupEntryModel entry in rm.Entries)
                        {
                            if (entry.ParentMatchup != null)
                            {
                                if (entry.ParentMatchup.id == m.id)
                                {
                                    entry.TeamCompeting = m.Winner;
                                    //GlobalConfig.Connection.UpdateMatchup(rm);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void MarkWinnerMatchups (List<MatchupModel> models)
        {
            //1 -> true, 0 -> false
            string highWins = ConfigurationManager.AppSettings["highWins"];

            foreach (MatchupModel m in models)
            {
                //checks bye
                if(m.Entries.Count == 1)
                {
                    m.Winner = m.Entries[0].TeamCompeting;
                    continue;
                }
                if (highWins == "0")
                {
                    if(m.Entries[0].Score < m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                    }
                    else if (m.Entries[0].Score > m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[1].TeamCompeting;
                    }
                    else
                    {
                        throw new Exception("Tie in matchup. Do a tiebreaker before submitting");
                    }
                }
                //default to high wins
                else
                {
                    if (m.Entries[0].Score < m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[1].TeamCompeting;
                    }
                    else if (m.Entries[0].Score > m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                    }
                    else
                    {
                        throw new Exception("Tie in matchup. Do a tiebreaker before submitting");
                    }
                } 
            }
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams, int teamsPerRound)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel current = new MatchupModel();

            foreach(TeamModel team in teams)
            {
                
                current.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes > 0 || current.Entries.Count >= teamsPerRound)
                {
                    current.MatchupRound = 1;
                    output.Add(current);
                    current = new MatchupModel();
                    if(byes  > 0)
                    {
                        byes -= 1;
                    }
                }
            }
            return output;
        }

        private static int NumberByes(int rounds, int teamsNumber, int teamsPerRound)
        {
            int output = 0;
            int totalTeams = 1;

            for(int i = 1; i<= rounds; i++)
            {
                totalTeams *= teamsPerRound;
            }
            output = totalTeams - teamsNumber;
            return output;
        }

        private static int FindNumberOfRounds(int teamCount, int teamsPerRound)
        {
            int output = 0;
            int matchups = 1;

            /* Already does this implicitly
             * if(teamCount <= 1)
            {
                return 0;
            }*/
            while (matchups < teamCount)
            {
                output += 1;
                matchups *= teamsPerRound;
            }
            return output;
        }

        //TODO: Make this generic, maybe improve algorithm for randomizing
        private static List<TeamModel> RandomizeOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }



        //TODO: Make this hold info (non static)
       /* List<MatchupModel> matchups = new List<MatchupModel>();

        public MatchupHelper()
        {

        }

        public MatchupHelper(List<MatchupModel> matchups)
        {
            this.matchups = matchups;
        }*/
    }
}
