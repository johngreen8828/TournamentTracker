using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Unique ID for MatcupModel
        /// </summary>
        public int Id { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// Use WinnerId for SQL to identify the winner
        /// </summary>
        public int WinnerId { get; set; }
        /// <summary>
        /// Use this for data 
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Which round this match is part of
        /// </summary>
        public int MatchupRound { get; set; }
        /// <summary>
        /// For displaying the team in the round
        /// </summary>
        public string DisplayName
        {
            get
            {
                string output = "";
                foreach (MatchupEntryModel me in Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = me.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += $" vs. {me.TeamCompeting.TeamName }";
                        }
                    }
                    else
                    {
                        output = "Matchup Not Yet Determined";
                        break;
                    }
                }
                return output;
            }
        }
    }
}
