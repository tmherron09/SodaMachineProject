using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public static class CoinCalculator
    {

        /// <summary>
        /// Get the value of a list of coins.
        /// </summary>
        /// <param name="coins">List of coins you want the total value.</param>
        /// <returns>The value of all the coins in the list.</returns>
        public static double GetValueOfCoins(List<Coin> coins)
        {
            double totalValue = 0;
            foreach(Coin coin in coins)
            {
                totalValue += coin.Value;
            }
            return Math.Round(totalValue, 2);
        }






        #region Depreciated/ Unused

        // Redundant switch case.
        //public static double GetValueOfCoins(List<Coin> coins)
        //{
        //    double totalValue = 0;
        //    foreach (Coin coin in coins)
        //    {
        //        switch (coin.name)
        //        {
        //            case "quarter":
        //                totalValue += coin.Value;
        //                break;
        //            case "dime":
        //                totalValue += coin.Value;
        //                break;
        //            case "nickel":
        //                totalValue += coin.Value;
        //                break;
        //            case "penny":
        //                totalValue += coin.Value;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return totalValue;
        //}

        #endregion

    }
}
