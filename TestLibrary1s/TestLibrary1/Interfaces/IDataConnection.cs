using System;
using System.Collections.Generic;
using System.Text;
using TestLibrary1.Models;

namespace TestLibrary1.Interfaces
{
    public interface IDataConnection
    {
        void CreatePrize(PrizeModel model);

        void CreatePerson(PersonModel person);

        void CreateTeam(TeamModel team);

        void CreateTournament(TournamentModel tournament);

        void UpdateMatchup(MatchupModel model);

        void CompleteTournament(TournamentModel tournament);

        List<PersonModel> GetPerson_All();

        List<TeamModel> GetTeam_All();

        List<TournamentModel> GetTournament_All();


    }
}
