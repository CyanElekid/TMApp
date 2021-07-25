using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibrary1.Models
{
    public class MatchupEntryModel
    {
        public int id { get; set; }
        public TeamModel TeamCompeting { get; set; }
        public double Score { get; set; }
        /// <summary>
        /// Represents the matchup this team came from
        /// aka just won this match
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
