using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibrary1.Models
{
    public class PrizeModel
    {
        public int id { get; set; }
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public decimal PrizeAmount { get; set; }
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }

        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
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
        public PrizeModel(int placeNumber, string placeName, decimal prizeAmount, double prizePercentage)
        {
            PlaceName = placeName;
            PlaceNumber = placeNumber;
            PrizeAmount = prizeAmount;
            PrizePercentage = prizePercentage;
        }
    }
}
