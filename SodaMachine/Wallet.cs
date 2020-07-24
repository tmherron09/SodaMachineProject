using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Wallet
    {
        public List<Coin> coins;
        public Card card;

        // Is this a bad use of making a variable dynamic?
        private double totalAmount;
        public double TotalAmount
        {
            get
            {
                this.totalAmount = Math.Round(CoinCalculator.GetValueOfCoins(coins), 2);
                return this.totalAmount;
            }
        }
        
        /// <summary>
        /// Generic Wallet initialization with no coins.
        /// </summary>
        public Wallet()
        {
            coins = new List<Coin>();
            card = new Card(5.00); // Begin with default amount on card, standard with new accounts.
        }
        /// <summary>
        /// Wallet constructor with inputs to define number of each coin in wallet.
        /// </summary>
        /// <param name="numberOfQuarters">Number of Quarters to put in wallet.</param>
        /// <param name="numberOfDimes">Number of Dimes to put in wallet.</param>
        /// <param name="numberOfNickels">Number of Nickels to put in wallet.</param>
        /// <param name="numberOfPennies">Number of Pennies to put in wallet.</param>
        public Wallet(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies)
        {
            coins = new List<Coin>();
            for (int i = 0; i < numberOfQuarters; i++)
            {
                coins.Add(new Quarter());
            }
            for (int i = 0; i < numberOfDimes; i++)
            {
                coins.Add(new Dime());
            }
            for (int i = 0; i < numberOfNickels; i++)
            {
                coins.Add(new Nickel());
            }
            for (int i = 0; i < numberOfPennies; i++)
            {
                coins.Add(new Penny());
            }
        }
        /// <summary>
        /// Used to initialize the number of coins in the Wallet class. Easy define number of each coin.
        /// </summary>
        /// <param name="numberOfQuarters">Number of Quarters to put in wallet.</param>
        /// <param name="numberOfDimes">Number of Dimes to put in wallet.</param>
        /// <param name="numberOfNickels">Number of Nickels to put in wallet.</param>
        /// <param name="numberOfPennies">Number of Pennies to put in wallet.</param>
        public void InitializeWalletCoins(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies)
        {
            for (int i = 0; i < numberOfQuarters; i++)
            {
                coins.Add(new Quarter());
            }
            for (int i = 0; i < numberOfDimes; i++)
            {
                coins.Add(new Dime());
            }
            for (int i = 0; i < numberOfNickels; i++)
            {
                coins.Add(new Nickel());
            }
            for (int i = 0; i < numberOfPennies; i++)
            {
                coins.Add(new Penny());
            }
            CoinCalculator.OrderByValue(ref coins);
        }
        /// <summary>
        /// Add a list of coins into the Wallet class.
        /// </summary>
        /// <param name="change">List of coins to add into wallet.</param>
        public void AddCoins(ref List<Coin> change)
        {
            foreach(Coin coin in change)
            {
                coins.Add(coin);
            }
        }
        /// <summary>
        /// Remove a coin by name from the wallet.
        /// </summary>
        /// <param name="coinName"></param>
        public void RemoveCoin(string coinName)
        {
            int index = coins.FindIndex(x => x.name == coinName);
            coins.RemoveAt(index);
        }
        /// <summary>
        /// Loops through and counts how many of each coin in the wallet.
        /// </summary>
        /// <returns></returns>
        public int[] CountOfCoins()
        {
            int numberOfQuarters = 0;
            int numberOfDimes = 0;
            int numberOfNickels = 0;
            int numberOfPennies = 0;
            foreach (Coin coin in coins)
            {
                switch (coin.name)
                {
                    case "quarter":
                        numberOfQuarters++;
                        break;
                    case "dime":
                        numberOfDimes++;
                        break;

                    case "nickel":
                        numberOfNickels++;
                        break;
                    case "penny":
                        numberOfPennies++;
                        break;
                }
            }
            return new int[] { numberOfQuarters, numberOfDimes, numberOfNickels, numberOfPennies };
        }



















        #region Original Comments for planning.

        // Wallet can be initialized with coins.
        // Wallet can be intialized with no coins
        // Wallet can report how many coins it has.
        // Wallet can have coins removed.
        // Wallet can have coins added.
        // Return a copy of the List

        #endregion

    }
}
