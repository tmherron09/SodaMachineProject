using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// Coin Calculator holds commonly used methods to count and order coins in a List or Wallet."
    /// </summary>
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

        /// <summary>
        /// Static method to order a List of coins by highest value first.
        /// </summary>
        /// <param name="coins">List of coins to ReOrder</param>
        public static void OrderByValue(ref List<Coin> coins)
        {
            coins = coins.OrderByDescending(x => x.Value).ToList();
        }


    }
}
