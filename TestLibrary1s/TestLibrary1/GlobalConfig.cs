using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TestLibrary1.FunctionLibrary.ConnectionLibrary;
using TestLibrary1.Interfaces;

namespace TestLibrary1
{
    public static class GlobalConfig
    {
        public const string PrizesFile = "PrizeModels.csv";
        public const string PersonFile = "PersonModels.csv";
        public const string TeamFile = "TeamModels.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntryFile = "MatchupEntryModels.csv";
        public const string FinishedTournamentsFile = "FinishedTournaments.csv";
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(string connectionType)
        {
            if (connectionType == "sql")
            {
                // TODO: sql connection
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (connectionType == "text")
            {
                // TODO: txt file connection
                TextConnector text = new TextConnector();
                Connection = text;
            }

            else throw new ArgumentException();
        }
        public static string CnnString(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        public static string AppSettingsLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
