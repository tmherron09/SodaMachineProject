using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class SodaMachine
    {
        public List<Coin> register;
        public List<Can> inventory;
        public List<Can> sodaSelection; // All the sodas displayed on the machine for sale.

        // On construct define starting coins
        public SodaMachine()
        {
            register = new List<Coin>();
            inventory = new List<Can>();
            InitializeSodaSelection();
        }
        public SodaMachine(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies)
        {
            inventory = new List<Can>();
            InitializeSodaSelection();
            register = new List<Coin>();
            for (int i = 0; i < numberOfQuarters; i++)
            {
                register.Add(new Quarter());
            }
            for (int i = 0; i < numberOfDimes; i++)
            {
                register.Add(new Dime());
            }
            for (int i = 0; i < numberOfNickels; i++)
            {
                register.Add(new Nickel());
            }
            for (int i = 0; i < numberOfPennies; i++)
            {
                register.Add(new Penny());
            }

        }
        public SodaMachine(int numberOfRootBeet, int numberOfCola, int numberOfOrangeSoda )
        {
            register = new List<Coin>();
            InitializeSodaSelection();
            inventory = new List<Can>();
            for (int i = 0; i < numberOfCola; i++)
            {
                inventory.Add(new Cola());
            }
            for (int i = 0; i < numberOfOrangeSoda; i++)
            {
                inventory.Add(new OrangeSoda());
            }
            for (int i = 0; i < numberOfRootBeet; i++)
            {
                inventory.Add(new RootBeer());
            }
        }
        public SodaMachine(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies, int numberOfRootBeet, int numberOfCola, int numberOfOrangeSoda )
        {
            // Populate Register with coins.
            register = new List<Coin>();
            for (int i = 0; i < numberOfQuarters; i++)
            {
                register.Add(new Quarter());
            }
            for (int i = 0; i < numberOfDimes; i++)
            {
                register.Add(new Dime());
            }
            for (int i = 0; i < numberOfNickels; i++)
            {
                register.Add(new Nickel());
            }
            for (int i = 0; i < numberOfPennies; i++)
            {
                register.Add(new Penny());
            }
            // Populate Inventory with cans
            inventory = new List<Can>();
            for (int i = 0; i < numberOfCola; i++)
            {
                inventory.Add(new Cola());
            }
            for (int i = 0; i < numberOfOrangeSoda; i++)
            {
                inventory.Add(new OrangeSoda());
            }
            for (int i = 0; i < numberOfRootBeet; i++)
            {
                inventory.Add(new RootBeer());
            }
            // Initialize Soda Selection
            InitializeSodaSelection();

        }
        public SodaMachine(List<Coin> coins, List<Can> cans)
        {
            register = coins;
            inventory = cans;
            InitializeSodaSelection();
        }
        public void InitializeSodaSelection()
        {
            sodaSelection = new List<Can>()
            {
                new RootBeer(),
                new Cola(),
                new OrangeSoda()
            };
        }
        
        #region Original Planning Comments

        // Methods
        // Check if can is available.
        // Return the price of a can.
        // Calculate if enough coins inserted.
        // Calculate Change value
        // Calculate if enough coins can be returned.
        // If enough coins inserted and enough change, dispense soda and subtract from inventory
        // Give change and subtract from register.

        #endregion

        /// <summary>
        /// Main method for processing a transaction on the Soda Machine. It takes a customer, their soda choice and their insert coins. It first validats if the machine can process the transactions. If valid, it will accept and add the coins. Dispense a soda to the customer class and return change if required.
        /// </summary>
        /// <param name="customer">Customer using the Soda Machine</param>
        /// <param name="sodaChoice">A reference to the chosen soda.</param>
        /// <param name="insertedCoins">The list of coins inserted by the customer."</param>
        /// <returns></returns>
        public bool Transaction(Customer customer, Can sodaChoice, List<Coin> insertedCoins)
        {
            double changeAmount = CoinCalculator.GetValueOfCoins(insertedCoins);
            if (CheckTransAction(sodaChoice, insertedCoins, ref changeAmount))
            {
                AcceptPayment(insertedCoins);
                customer.backpack.AddCan(DispenseSodaCan(sodaChoice));
                DispenseChange(changeAmount, customer);
                UserInterface.DisplayList($"Enjoy your {sodaChoice.name}! Have a Great Day!", true, true, true);
                return true;
            }
            
            UserInterface.DisplayList("Please grab your change.", false, true, true);
            DispenseChange(insertedCoins, customer);
            //DispenseChange(CoinCalculator.GetValueOfCoins(insertedCoins), customer);
            return false;
        }
        // Validation stage
        public bool CheckTransAction(Can sodaChoice, List<Coin> insertedCoins, ref double changeAmount)
        {
            if (CheckInventoryForSoda(sodaChoice))
            {
                if (CheckValidMoneyExchange(sodaChoice, insertedCoins, ref changeAmount))
                {
                    return true;
                }
                return false;
            }
            UserInterface.DisplayList("OUT OF STOCK", true);
            return false;
        }
        public bool CheckInventoryForSoda(Can soda)
        {
            // Check if any matching sodas.

            return inventory.Exists(x => x.name == soda.name);
        }
        public bool CheckValidMoneyExchange(Can soda, List<Coin> customerCoins, ref double changeAmount)
        {
            if (changeAmount >= soda.Price)
            {
                // Create a temp, if Can give change fails, we will have unaltered changeAmount to return to customer.
                double tempChangeAmount = Math.Round(changeAmount - soda.Price, 2);
                if (CanGiveChange(tempChangeAmount))
                {
                    changeAmount = tempChangeAmount;
                    return true;
                }
                UserInterface.DisplayList("Sorry, this machine does not have the required amount of change.", true, true);
                return false;
            }
            UserInterface.DisplayList("We're sorry, but the amount you entered is not enough.", true);
            return false;
        }
        // Payment/Change Methods
        public bool CanGiveChange(double changeAmount)
        {
            if (changeAmount == 0)
            {
                return true;
            }
            // Set highest coins first in register, check if redundant
            //Coin.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value, 2) == changeAmount)
                {
                    return true;
                }
                else if (changeValue + coin.Value < changeAmount)
                {
                    changeValue += coin.Value;
                }
            }
            return false;
        }
        public void AcceptPayment(List<Coin> insertedCoins)
        {
            // Add coins to register
            register.InsertRange(0, insertedCoins); //
            CoinCalculator.OrderByValue(ref register); // Reorder register with highest value coins first
        }
        public void DispenseChange(double amountToDispense, Customer customer)
        {
            if (amountToDispense == 0)
            {
                return;
            }
            List<Coin> change = new List<Coin>();
            // Set highest coins first in register
            CoinCalculator.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value, 2) == amountToDispense)
                {
                    changeValue += coin.Value;
                    change.Add(coin);
                    break;
                }
                else if (changeValue + coin.Value < amountToDispense)
                {
                    changeValue += coin.Value;
                    change.Add(coin);
                }
            }
            foreach (Coin coin in change)
            {
                int index = register.FindIndex(x => x.name == coin.name);
                register.RemoveAt(index);
            }
            customer.RecieveChange(ref change);
        }
        public void DispenseChange(List<Coin> insertedCoins, Customer customer)
        {
            customer.RecieveChange(ref insertedCoins);
        }

        // Soda Can Methods
        public Can DispenseSodaCan(Can soda)
        {
            int index = inventory.FindIndex(x => x.name == soda.name);
            inventory.RemoveAt(index);
            switch (soda.name)
            {
                case "Root Beer":
                    return new RootBeer();
                case "Cola":
                    return new Cola();
                case "Orange Soda":
                    return new OrangeSoda();
                default:
                    throw new Exception();
            }
        }



        #region Method Alternative approach
        /* Method Alternative Approach
        public bool CanGiveChange(double changeAmount, ref List<Coin> coinChange)
        {
            if (changeAmount == 0)
            {
                return true;
            }
            // Set highest coins first in register
            Coin.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value, 2) == changeAmount)
                {
                    coinChange.Add(coin);
                    return true;
                }
                else if (changeValue + coin.Value < changeAmount)
                {
                    changeValue += coin.Value;
                    coinChange.Add(coin);
                }
            }
            return false;
            }
        */
        #endregion


        #region Depreciated/ Dead Code

        /* Depcriated/ Dead Code
        public bool Transaction(Customer customer, Can sodaChoice, List<Coin> insertedCoins)
        {
            if (CheckTransAction(sodaChoice, insertedCoins))
            {
                AcceptPayment(insertedCoins);
                Can dispensedSoda = DispenseSodaCan(sodaChoice);
                customer.backpack.AddCan(dispensedSoda);
                double requiredChange = CaluclateChange(insertedCoins, sodaChoice);
                customer.RecieveChange(DispenseChange(requiredChange));
                return true;
            }

            UserInterface.DisplayList("Please grab your change.", true, true, true);
            DispenseChange(CoinCalculator.GetValueOfCoins(insertedCoins), customer);
            return false;
        }
        public bool CheckTransAction(Can sodaChoice, List<Coin> insertedCoins)
        {
            if (CheckInventoryForSoda(sodaChoice))
            {
                if (CheckValidMoneyExchange(sodaChoice, insertedCoins))
                {
                    return true;
                }
                return false;
            }
            UserInterface.DisplayList("OUT OF STOCK");
            return false;
        }

        public bool CheckValidMoneyExchange(Can soda, List<Coin> customerCoins)
        {
            if (CoinCalculator.GetValueOfCoins(customerCoins) > soda.Price)
            {
                double requiredChange = CaluclateChange(customerCoins, soda);
                if (CanGiveChange(requiredChange))
                {
                    return true;
                }
                UserInterface.DisplayList("Sorry, this machine does not have the required amount of change.", true, true);
                return false;
            }
            UserInterface.DisplayList("We're sorry, but the amount you entered is not enough.", true, true);
            return false;
        }
        public double CaluclateChange(List<Coin> customerCoins, Can soda)
        {
            return Math.Round(CoinCalculator.GetValueOfCoins(customerCoins) - soda.Price, 2);
        }
        public List<Coin> DispenseChange(double amountToDispense)
        {
            if (amountToDispense == 0)
            {
                return null;
            }
            List<Coin> change = new List<Coin>();
            // Set highest coins first in register
            Coin.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value, 2) == amountToDispense)
                {
                    change.Add(coin);
                    break;
                }
                else if (changeValue + coin.Value < amountToDispense)
                {
                    changeValue += coin.Value;
                    change.Add(coin);
                }
            }
            foreach (Coin coin in change)
            {
                int index = register.FindIndex(x => x.name == coin.name);
                register.RemoveAt(index);
            }
            return change;
        }
        Depcriated/ Dead Code */


        #endregion



    }
}
