using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibrary1.Models
{
    public class TeamModel
    {

        public int id { get; set; }
        public string TeamName { get; set; } 
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        public TeamModel(string teamName, List<PersonModel> teamMembers)
        {
            TeamName = teamName;
            TeamMembers = teamMembers;
        }
        public TeamModel()
        {

        }
    }
}
