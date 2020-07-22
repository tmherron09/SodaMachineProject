using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Customer
    {
        private Wallet wallet;
        private Backpack backpack;

        public Customer()
        {
            wallet = new Wallet();
            // define number of coins in wall.
            backpack = new Backpack();
        }

        // Customer can give its wallets contents.
        // Customer can check contents of Backpack.
        // Check if price of soda is affordable.
        // Make a payment, transfer coins out of wallet and into soda machine.
        // Customer can recieve a soda
        // Customer can recieve change


        public List<Coin> CheckCustomerWalletContents()
        {
            return wallet.GetCoinList();  // Returns a copy of the Coin List
        }
        public Backpack GetCustomerBackpack()
        {

            return backpack;
        }
        public bool HasEnoughMoney(double price)
        {
            throw new NotImplementedException();
        }
        public List<Coin> MakePayment(List<Coin> approvedTransaction, SodaMachine sodaMachine, Can soda) // Customer then directly inserts money
        {
            // Remove change from wallet.
            // Call soda machine and insert change.
            // Recieve soda from machine.
            // Put soda into backpack.
            throw new NotImplementedException();
        }
        public void RecieveChange(List<Coin> change)
        {
            throw new NotImplementedException();
        }
        public void RecieveSoda(Can soda)
        {
            throw new NotImplementedException();
        }

        public void UseSodaMachine(SodaMachine sodaMachine)
        {
            Can sodaChoice = null;
            do
            {
                sodaChoice = UserInterface.AskForSodaSelection(sodaMachine.sodaSelection);
            } while (CheckIfCanAfford(sodaChoice.Price));
            
            List<Coin> insertedCoins = ChoseCoinsToInsert();

            if(InsertedCoinsAccepted(sodaMachine))
            {


            }

        }

        

        private bool CheckIfCanAfford(double price)
        {
            throw new NotImplementedException();
        }
        private bool CheckIfCanAfford(string priceAsString)
        {
            double price = Convert.ToDouble(priceAsString);


            throw new NotImplementedException();
        }

        

        private List<Coin> ChoseCoinsToInsert()
        {
            bool hasEnoughCoins = false;
            int[] countOfCoins = wallet.CountOfCoins();
            do
            {
                List<Coin> coinChoices = new List<Coin>();

                int numberOfQuarters = UserInterface.AskForCoinAmount("quarters", countOfCoins[0]);
                int numberOfDimes = UserInterface.AskForCoinAmount("dimes", countOfCoins[1]);
                int numberOfNickels = UserInterface.AskForCoinAmount("nickels", countOfCoins[2]);
                int numberOfPennies = UserInterface.AskForCoinAmount("pennies", countOfCoins[3]);

                if(numberOfQuarters <= countOfCoins[0] && numberOfDimes <= countOfCoins[1] && numberOfNickels <= countOfCoins[2] && numberOfPennies <= countOfCoins[3])
                {
                    hasEnoughCoins = true;
                }
                else
                {
                    hasEnoughCoins = false;
                    UserInterface.DisplayList("You do not have that many coins in your wallet.", false, true, true);
                }

                if (hasEnoughCoins)
                {
                    for (int i = 0; i < numberOfQuarters; i++)
                    {
                        coinChoices.Add(new Quarter());
                    }
                    for (int i = 0; i < numberOfDimes; i++)
                    {
                        coinChoices.Add(new Dime());
                    }
                    for (int i = 0; i < numberOfNickels; i++)
                    {
                        coinChoices.Add(new Nickel());
                    }
                    for (int i = 0; i < numberOfPennies; i++)
                    {
                        coinChoices.Add(new Penny());
                    }
                }

            } while (hasEnoughCoins);

            return ;
        }

        private bool InsertedCoinsAccepted(SodaMachine sodaMachine)
        {
            throw new NotImplementedException();
        }

    }
}
