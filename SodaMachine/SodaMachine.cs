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

        public bool Transaction(Customer customer, Can sodaChoice, List<Coin> insertedCoins)
        {
            if(CheckTransAction(sodaChoice, insertedCoins))
            {
                AcceptPayment(insertedCoins);
                Can dispensedSoda = DispenseSodaCan(sodaChoice);

                return true;
            }
            return false;
        }

        public bool CheckTransAction(Can soda, List<Coin> customerCoins)
        {
            if (CheckInventoryForSoda(soda))
            {
                if (CheckIfEnoughMoney(soda, customerCoins))
                {
                    double requiredChange = CaluclateChange(customerCoins, soda);
                    if (CanGiveChange(requiredChange))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckIfEnoughMoney(Can soda, List<Coin> customerCoins)
        {
            if(CoinCalculator.GetValueOfCoins(customerCoins) > soda.Price)
            { 
                return true; 
            }
            return false;

        }
        public double CaluclateChange(List<Coin> customerCoins, Can soda)
        {
            return CoinCalculator.GetValueOfCoins(customerCoins) - soda.Price;
            throw new NotImplementedException();
        }
        public bool CanGiveChange(double requriedChange)
        {
            double changeValue = 0;
            foreach(Coin coin in register)
            {
                if(changeValue + coin.Value == requriedChange)
                {
                    return true;
                }
                else if( changeValue + coin.Value < requriedChange)
                {
                    changeValue += coin.Value;
                }
            }
            return false;
        }
        public bool CheckInventoryForSoda(Can soda)
        {
            // Check if any matching sodas.

            return inventory.Exists(x => x.Name == soda.Name);
        }
        public void AcceptPayment(List<Coin> insertedCoins)
        {
            // Add coins
            register.InsertRange(-1, insertedCoins);
        }
        public List<Can> SodaOfferings()
        {
            List<Can> sodas = inventory;
            return sodas;
        }
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
        private void OrganizeInventory()
        {
            inventory = inventory.OrderBy(x => x.Name).ToList();
        }
        private void OrganizeRegister()
        {
            register = register.OrderBy(x => x.name).ToList();
        }
    }
}
