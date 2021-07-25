using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibrary1.Models
{
    public class MatchupModel
    {
        public int id { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";
                foreach(MatchupEntryModel entry in Entries)
                {
                    string teamName = "";
                    if (entry.TeamCompeting != null)
                    {
                        teamName = entry.TeamCompeting.TeamName;
                    }
                    else teamName = "TBD";

                    if(output.Length == 0)
                    {
                        output = teamName;
                    }
                    else
                    {
                        output += $" vs {teamName}";
                    }
                }
                return output;
            }
        }
    }
}
