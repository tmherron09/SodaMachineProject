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
                wallet.InitializeWalletCoins(8, 15, 20, 50);
            }
            else
            {
                wallet = new Wallet();
                backpack = new Backpack();
            }
        }
        /// <summary>
        /// Main interaction between customer and a soda machine.
        /// </summary>
        /// <param name="sodaMachine">The soda machine instance being used.</param>
        /// <returns>True/False if customer successfully got a soda.</returns>
        public bool UseSodaMachine(SodaMachine sodaMachine)
        {
            UserInterface.DisplayMainScreen();
            string sodaChoiceName = UserInterface.SodaSelectionScreen(sodaMachine.sodaSelection);

            return sodaMachine.Transaction(this, sodaChoiceName, ChoseCoinsToInsert());
        }
        /// <summary>
        /// Call for 4 inputs from User to determine how many coins to insert into vending machine.
        /// </summary>
        /// <returns>A list of coins customer wishes to insert.</returns>
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
        /// <summary>
        /// Checks if customer has enough remaining money to purchase selected soda.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool CheckIfCanAfford(double price)
        {
            if (wallet.TotalAmount > price)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Pass a list of coins to the customer's wallet.
        /// </summary>
        /// <param name="change">Coins to put in the customer wallet.</param>
        public void RecieveChange(ref List<Coin> change)
        {
            if (change != null)
            {
                wallet.AddCoins(ref change);
            }
        }

        #region Unused/ Depreciated

        //public void RecieveSoda(Can soda)
        //{
        //    backpack.AddCan(soda);
        //}


        //public List<Coin> CheckCustomerWalletContents()
        //{
        //    return wallet.GetCoinList();  // Returns a copy of the Coin List
        //}

        #endregion




    }
}
