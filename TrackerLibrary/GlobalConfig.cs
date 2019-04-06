using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TrackerLibrary.DataAccess;


namespace TrackerLibrary
{
    public static class GlobalConfig


    {
        public const string PrizesFile = "PrizeModel.csv";
        public const string PeopleFile = "PersonModel.csv";
        public const string TeamFile = "TeamModels.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntryFile = "MatchupEntryModels.csv";


        //property to hold connections --2
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseType db) //These are the connections to be initialized or set up at the beginning of the app.  Call this method
        {
            if (db == DatabaseType.Sql)
            {
                //TODO Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;

            }

            else if (db == DatabaseType.TextFile)
            {
                //TODO Create the Text Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
