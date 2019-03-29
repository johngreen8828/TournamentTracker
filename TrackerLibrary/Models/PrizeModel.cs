using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// Unique ID for the PrizeModel row
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numeric Identifier for the place
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// Friendly name for the place
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// Fixed amount this place earns or zero if it is not used
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// The number that represents the percentage of the overall take or zero if not used
        /// </summary>
        public double PrizePercentage { get; set; }
        /// <summary>
        /// Em
        /// </summary>
        public PrizeModel()
        {

        }
        /// <summary>
        /// Overridden method to parse the values in the form and put them into the PrizeModel properties
        /// </summary>
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }


    }
}