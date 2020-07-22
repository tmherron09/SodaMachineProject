﻿using System;
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
        public SodaMachine(int numberOfCola, int numberOfOrangeSoda, int numberOfRootBeet)
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
        public SodaMachine(int numberOfQuarters, int numberOfDimes, int numberOfNickels, int numberOfPennies, int numberOfCola, int numberOfOrangeSoda, int numberOfRootBeet)
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

        // Methods

        // Check if can is available.
        // Return the price of a can.
        // Calculate if enough coins inserted.
        // Calculate Change value
        // Calculate if enough coins can be returned.
        // If enough coins inserted and enough change, dispense soda and subtract from inventory
        // Give change and subtract from register.
        
        /// <summary>
        /// Main method for processing a transaction on the Soda Machine. It takes a customer, their soda choice and their insert coins. It first validats if the machine can process the transactions. If valid, it will accept and add the coins. Dispense a soda to the customer class and return change if required.
        /// </summary>
        /// <param name="customer">Customer using the Soda Machine</param>
        /// <param name="sodaChoice">A reference to the chosen soda.</param>
        /// <param name="insertedCoins">The list of coins inserted by the customer."</param>
        /// <returns></returns>
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
            return false;
        }


        // Validation stage
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
        public bool CheckInventoryForSoda(Can soda)
        {
            // Check if any matching sodas.

            return inventory.Exists(x => x.Name == soda.Name);
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
                UserInterface.DisplayList("Sorry, this machine does not have the required amount of change.");
                return false;
            }
            Console.WriteLine("We're sorry, but the amount you entered is not enough.");
            return false;

        }
        
        // Payment/Change Methods
        public double CaluclateChange(List<Coin> customerCoins, Can soda)
        {
            return Math.Round(CoinCalculator.GetValueOfCoins(customerCoins) - soda.Price, 2);
        }
        public bool CanGiveChange(double requiredChange)
        {
            if (requiredChange == 0)
            {
                return true;
            }
            // Set highest coins first in register
            Coin.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value, 2) == requiredChange)
                {
                    return true;
                }
                else if (changeValue + coin.Value < requiredChange)
                {
                    changeValue += coin.Value;
                }
            }
            return false;
        }
        public void AcceptPayment(List<Coin> insertedCoins)
        {
            // Add coins to register
            register.InsertRange(0, insertedCoins);
            Coin.OrderByValue(ref register); // Reorder register with highest value coins first
        }
        public List<Coin> DispenseChange(double requiredChange)
        {
            List<Coin> change = new List<Coin>();

            if (requiredChange == 0)
            {
                return change;
            }
            // Set highest coins first in register
            Coin.OrderByValue(ref register);
            double changeValue = 0;
            foreach (Coin coin in register)
            {
                if (Math.Round(changeValue + coin.Value,2) == requiredChange)
                {
                    change.Add(coin);
                    break;
                }
                else if (changeValue + coin.Value < requiredChange)
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


        // Soda Can Methods
        public Can DispenseSodaCan(Can soda)
        {
            int index = inventory.FindIndex(x => x.name == soda.name);
            inventory.RemoveAt(index);
            switch (soda.Name)
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
        
        
        // Utility Methods
        
    }
}
