using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Customer
    {
        public Wallet wallet;
        public Backpack backpack;


        /// <summary>
        /// Makes new customer instance with empty wallet and backpack.
        /// </summary>
        public Customer()
        {
            wallet = new Wallet();
            backpack = new Backpack();
        }
        /// <summary>
        /// Makes new customer instance with default user stories values
        /// </summary>
        /// <param name="userstoriesDefaults">True: Set to user stories defaults</param>
        public Customer(bool userstoriesDefaults)
        {
            if (userstoriesDefaults)
            {
                wallet = new Wallet();
                backpack = new Backpack();
                wallet.AddCoins(8, 15, 20, 50);
            }
            else
            {
                wallet = new Wallet();
                backpack = new Backpack();
            }
        }


        public bool UseSodaMachine(SodaMachine sodaMachine)
        {
            Can sodaChoice = null;
            do
            {
                sodaChoice = UserInterface.AskForSodaSelection(sodaMachine.sodaSelection);
            } while (!CheckIfCanAfford(sodaChoice.Price));
            List<Coin> insertedCoins = ChoseCoinsToInsert();
            double changeAmount = CoinCalculator.GetValueOfCoins(insertedCoins);
            return sodaMachine.Transaction(this, sodaChoice, insertedCoins, ref changeAmount);
        }




        public List<Coin> ChoseCoinsToInsert()
        {
            int[] countOfCoins = wallet.CountOfCoins();
            List<Coin> coinChoices = new List<Coin>();

            int numberOfQuarters = UserInterface.AskForCoinAmount("quarters", countOfCoins[0]);
            int numberOfDimes = UserInterface.AskForCoinAmount("dimes", countOfCoins[1]);
            int numberOfNickels = UserInterface.AskForCoinAmount("nickels", countOfCoins[2]);
            int numberOfPennies = UserInterface.AskForCoinAmount("pennies", countOfCoins[3]);

            for (int i = 0; i < numberOfQuarters; i++)
            {
                wallet.RemoveCoin("quarter");
                coinChoices.Add(new Quarter()); ;
            }
            for (int i = 0; i < numberOfDimes; i++)
            {
                wallet.RemoveCoin("dime");
                coinChoices.Add(new Dime()); ;
            }
            for (int i = 0; i < numberOfNickels; i++)
            {
                wallet.RemoveCoin("nickel");
                coinChoices.Add(new Nickel()); ;
            }
            for (int i = 0; i < numberOfPennies; i++)
            {
                wallet.RemoveCoin("penny");
                coinChoices.Add(new Penny()); ;
            }
            return coinChoices;
        }
        public bool CheckIfCanAfford(double price)
        {
            if (wallet.TotalAmount > price)
            {
                return true;
            }
            return false;
        }
        public void RecieveChange(ref List<Coin> change)
        {
            if (change != null)
            {
                wallet.AddCoins(ref change);
            }
        }
        






        #region Unused/ Depreciated

        public void RecieveSoda(Can soda)
        {
            backpack.AddCan(soda);
        }


        public List<Coin> CheckCustomerWalletContents()
        {
            return wallet.GetCoinList();  // Returns a copy of the Coin List
        }

        #endregion




    }
}
