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

        // Is this a bad use of making a variable dynamic?
        private double totalAmount;
        public double TotalAmount
        {
            get
            {
                this.totalAmount = CoinCalculator.GetValueOfCoins(coins);
                return this.totalAmount;
            }
        }
        
        
        public Wallet()
        {
            coins = new List<Coin>();
        }
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

        // Wallet can be initialized with coins.
        // Wallet can be intialized with no coins
        // Wallet can report how many coins it has.
        // Wallet can have coins removed.
        // Wallet can have coins added.

        // Return a copy of the List


        public double CalculateTotalAmount()
        {
            // Loop over each coin, tally values, set into TotalAmount/totalAmount.
            return CoinCalculator.GetValueOfCoins(coins);
        }

        public void AddCoins(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies)
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
            Coin.OrderByValue(ref coins);
        }

        public void AddCoins(List<Coin> coins)
        {
            coins.InsertRange(0, coins);
        }

        // Refactor into One for loop.
        public void AddCoins(string coinName, int numberOfCoins)
        {
            switch (coinName)
            {
                case "quarter":
                    {
                        for (int i = 0; i < numberOfCoins; i++)
                        {
                            coins.Add(new Quarter());
                        }

                        break;
                    }

                case "dime":
                    {
                        for (int i = 0; i < numberOfCoins; i++)
                        {
                            coins.Add(new Dime());
                        }

                        break;
                    }

                case "nickel":
                    {
                        for (int i = 0; i < numberOfCoins; i++)
                        {
                            coins.Add(new Nickel());
                        }

                        break;
                    }

                case "penny":
                    {
                        for (int i = 0; i < numberOfCoins; i++)
                        {
                            coins.Add(new Penny());
                        }

                        break;
                    }
                default:
                    throw new Exception();
                    break;
            }
            Coin.OrderByValue(ref coins);
        }

        public void RemoveCoin(string coinName)
        {
            int index = coins.FindIndex(x => x.name == coinName);
            coins.RemoveAt(index);
        }
        private void OrganizeCoinsInWallet()
        {
            coins = coins.OrderBy(x => x.name).ToList();
        }

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

        #region Depreciated/ Refactored

        public List<Coin> GetCoinList()
        {
            Coin.OrderByValue(ref coins);
            
            return coins;
        }
        #endregion


        

    }
}
