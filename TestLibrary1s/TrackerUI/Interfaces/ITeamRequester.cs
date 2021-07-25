using System;
using System.Collections.Generic;
using System.Text;
using TestLibrary1.Models;

namespace TrackerUI.Interfaces
{
    public interface ITeamRequester
    {
        void TeamComplete(TeamModel model);
    }
}
