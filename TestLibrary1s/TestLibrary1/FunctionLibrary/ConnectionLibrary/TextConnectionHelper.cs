using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Linq;
using TestLibrary1.Models;

namespace TestLibrary1.FunctionLibrary.ConnectionLibrary.TextConnectionHelper
{
    public static class TextConnectionHelper
    {
        /// <summary>
        /// Prepends root file path to given file name
        /// </summary>
        /// <param name="filename"> name (from root folder)</param>
        /// <returns> full file path</returns>
       //extension method for string
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["textStorageRoot"] }\\{fileName}";
        }

        public static List<string> LoadFile (this string file)
        {
            if (!File.Exists(file)){
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        //TODO: make this generic
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel();
                p.id = int.Parse(cols[0]);
                p.FirstName = (cols[1]);
                p.LastName = cols[2];
                p.EmailAddress = (cols[3]);
                p.CellPhoneNumber = (cols[4]);
                output.Add(p);
            }
            return output;
        }

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                MatchupModel p = new MatchupModel();
                p.id = int.Parse(cols[0]);
                p.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                int winnerId = 0;
                if (int.TryParse(cols[2], out winnerId))
                {
                    p.Winner = GetTeam_ById(winnerId);
                }
                else
                {
                    p.Winner = null;
                }
                p.MatchupRound = int.Parse(cols[3]);
                output.Add(p);
            }
            return output;
        }

        public static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<string> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile();
            List<string> matchingEntries = new List<string>();

            foreach(string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');
                    if (cols[0] == id)
                    {
                        matchingEntries.Add(entry);
                    }
                }
            }
            output = matchingEntries.ConvertToMatchupEntryModels();

            return output;
        }


        //TODO: Make generic, for all props or somethin?
        public static void SaveToPrizeFile(this List<PrizeModel> models)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
        }

        public static void SaveToPersonFile(this List<PersonModel> models)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.CellPhoneNumber}");
            }
            File.WriteAllLines(GlobalConfig.PersonFile.FullFilePath(), lines);
        }

       /* public static void SaveToFile(this List<T> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (T t in models)
            {
                //stringbuilder here for props
                //loop through props and add
                //add line to lines
                lines.Add(x);
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }*/

        public static List<TeamModel> ConvertToTeamModels( this List<string> lines)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = GlobalConfig.PersonFile.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach ( string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel team = new TeamModel();
                team.id = int.Parse(cols[0]);
                team.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach( string id in personIds)
                {
                    //throws if no id is empty
                    team.TeamMembers.Add(people.Where(x => x.id == int.Parse(id)).First());
                }
                output.Add(team);
            }
            return output;
        }

        public static void SaveToTeamFile (this List<TeamModel> models)
        {
            List<string> lines = new List<string>();

            foreach(TeamModel team in models)
            {
                lines.Add($"{team.id},{team.TeamName},{ConvertPersonListToString(team.TeamMembers)}");
            }
            File.WriteAllLines(GlobalConfig.TeamFile.FullFilePath(), lines);
        }

        private static string ConvertPersonListToString( List<PersonModel> people)
        {
            string output = "";
            if(people.Count == 0)
            {
                return "";
            }
            foreach(PersonModel person in people)
            {
                output += $"{person.id}|";
            }
          
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines)
        {
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tm = new TournamentModel();
                tm.id = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split('|');
                string[] prizeIds = cols[4].Split('|');
                string[] rounds = cols[5].Split('|');

                foreach (string id in teamIds)
                {
                    tm.EnteredTeams.Add(teams.Where(x => x.id == int.Parse(id)).First());
                }
                foreach (string id in prizeIds)
                {
                    if (id != "")
                    {
                        tm.Prizes.Add(prizes.Where(x => x.id == int.Parse(id)).First());
                    }
                }

                foreach(string round in rounds)
                {
                    string[] matchIds = round.Split('§');
                    List<MatchupModel> ms = new List<MatchupModel>();

                    foreach (string id in matchIds)
                    {
                        ms.Add(matchups.Where(x => x.id == int.Parse(id)).First());
                    }
                    tm.Rounds.Add(ms);
                }

                output.Add(tm);
            }
            return output;
        }

        //edited for fin tm
        public static List<TournamentModel> SaveTournamentToFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach(TournamentModel tm in models)
            {
                lines.Add($"{tm.id},{tm.TournamentName},{tm.EntryFee},{ConvertTeamListToString(tm.EnteredTeams)},{ConvertPrizeListToString(tm.Prizes)},{ConvertRoundListToString(tm.Rounds)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
            return models;
        }

        public static void SaveRoundsToFile(this TournamentModel model)
        {
            //List<string> lines = new List<string>();
            

            foreach (List<MatchupModel> round in model.Rounds)
            {
        //               
                foreach(MatchupModel match in round)
                {
                    match.SaveMatchupToFile();
                }
               
            }
            //File.WriteAllLines(matchupFile.FullFilePath(), lines);
        }

        public static void SaveMatchupToFile(this MatchupModel model)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            List<string> lines = new List<string>();

            int currentId = 1;

            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.id).First().id + 1;
            }
            model.id = currentId;

            matchups.Add(model);

            foreach (MatchupEntryModel entry in model.Entries)
            {
                entry.SaveMatchupEntryToFile();
            }
            foreach(MatchupModel match in matchups)
            {
                string winner = "";
                if (match.Winner != null)
                {
                    winner = match.Winner.id.ToString();
                }

                lines.Add($"{match.id},{ConvertEntryListToString(match.Entries)},{winner},{match.MatchupRound}");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }

        public static void SaveMatchupEntryToFile(this MatchupEntryModel model)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            List<string> lines = new List<string>();

            int currentId = 1;

            if (entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.id).First().id + 1;
            }
            model.id = currentId;
            entries.Add(model);

            foreach(MatchupEntryModel entry in entries)
            {
                string parent = "";
                if(entry.ParentMatchup != null)
                {
                    parent = entry.ParentMatchup.id.ToString();
                }
                string teamCompeting = "";
                if (entry.TeamCompeting != null)
                {
                    teamCompeting = entry.TeamCompeting.id.ToString();
                }
                lines.Add($"{entry.id},{teamCompeting},{entry.Score},{parent}");
            }


            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }

        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                MatchupEntryModel p = new MatchupEntryModel();
                p.id = int.Parse(cols[0]);
                if(cols[1].Length == 0)
                {
                    p.TeamCompeting = null;
                }
                else p.TeamCompeting = GetTeam_ById(int.Parse(cols[1]));
                p.Score = double.Parse(cols[2]);
                int parentId = 0;
                if (int.TryParse((cols[3]), out parentId))
                {
                    p.ParentMatchup = GetMatchup_ById(parentId);
                }
                else
                {
                    p.ParentMatchup = null;
                }

                output.Add(p);
            }
            return output;
        }

        public static void UpdateMatchupToFile(this MatchupModel model)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            List<string> lines = new List<string>();

            MatchupModel oldMatchup = new MatchupModel() ;
            foreach(MatchupModel matchup in matchups)
            {
                if(model.id == matchup.id)
                {
                    oldMatchup = (matchup);
                }
            }
            matchups.Remove(oldMatchup);
            matchups.Add(model);

            foreach (MatchupEntryModel entry in model.Entries)
            {
                entry.UpdateMatchupEntryToFile();
            }
            foreach (MatchupModel match in matchups)
            {
                string winner = "";
                if (match.Winner != null)
                {
                    winner = match.Winner.id.ToString();
                }

                lines.Add($"{match.id},{ConvertEntryListToString(match.Entries)},{winner},{match.MatchupRound}");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }
        //TODO:  make this and the one above generic?
        public static void UpdateMatchupEntryToFile(this MatchupEntryModel model)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            List<string> lines = new List<string>();

            MatchupEntryModel oldEntry = new MatchupEntryModel();
            foreach (MatchupEntryModel entry in entries)
            {
                if (model.id == entry.id)
                {
                    oldEntry = entry;
                }
            }
            entries.Remove(oldEntry);

            entries.Add(model);

            foreach (MatchupEntryModel entry in entries)
            {
                string parent = "";
                if (entry.ParentMatchup != null)
                {
                    parent = entry.ParentMatchup.id.ToString();
                }
                string teamCompeting = "";
                if (entry.TeamCompeting != null)
                {
                    teamCompeting = entry.TeamCompeting.id.ToString();
                }
                lines.Add($"{entry.id},{teamCompeting},{entry.Score},{parent}");
            }


            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }

        private static TeamModel GetTeam_ById(int id)
        {
            List<string> allTeams = GlobalConfig.TeamFile.FullFilePath().LoadFile();
          //  return allTeams.Where(x => x.id == id).First();

            foreach (string team in allTeams)
            { 
                string[] cols = team.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingTeams = new List<string>();
                    matchingTeams.Add(team);
                    return matchingTeams.ConvertToTeamModels().First();
                }  
            }
            return null;
        }

        private static MatchupModel GetMatchup_ById(int id)
        {
            List<string> allMatchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile();
            // return allMatchups.Where(x => x.id == id).First();
            foreach (string matchup in allMatchups)
            {
                string[] cols = matchup.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingMatchups = new List<string>();
                    matchingMatchups.Add(matchup);
                    return matchingMatchups.ConvertToMatchupModels().First();
                }
            }
            return null;
        }




    //TODO: Make all this generic

    private static string ConvertTeamListToString (List<TeamModel> teams)
        {
            string output = "";

            if (teams.Count == 0) 
            {
                return output;
            }

            foreach (TeamModel team in teams)
            {
                output += $"{team.id}|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";

            if (prizes.Count == 0)
            {
                return output;
            }

            foreach (PrizeModel prize in prizes)
            {
                output += $"{prize.id}|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            if (rounds.Count == 0)
            {
                return output;
            }

            foreach (List<MatchupModel> m in rounds)
            {
                output += $"{ConvertMatchupListToString(m)}|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";

            if (matchups.Count == 0)
            {
                return output;
            }

            foreach (MatchupModel matchup in matchups)
            {
                output += $"{matchup.id}§";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertEntryListToString(List<MatchupEntryModel> entries)
        {
            string output = "";

            if (entries.Count == 0)
            {
                return output;
            }

            foreach (MatchupEntryModel entry in entries)
            {
                output += $"{entry.id}|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
