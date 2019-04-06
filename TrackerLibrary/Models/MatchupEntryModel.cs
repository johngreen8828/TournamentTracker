﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Unique ID for MatcupEntryModel
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///    Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        ///     Represents the score for this particular team
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Represents the matchup that this team came from
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
