using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// Represents a physical soda machine. It contains a register with money in the form of Coins. Its inventory contains Cans of soda. It also has a set soda selection to display even when inventory is empty. It can accept coin and credit cards. It will Validate Transactions before processing and return necessary change if successful or failed.
    /// </summary>
    public class SodaMachine
    {
        public List<Coin> register;
        public List<Can> inventory;
        public List<Can> sodaSelection; // All the sodas displayed on the machine for sale.

        /// <summary>
        /// Default Soda Machine constructor. Creates instance of Soda Machine with no money or inventory. Initializes "available" soda via InitializeSodaSelection() method.
        /// </summary>
        public SodaMachine()
        {
            register = new List<Coin>();
            inventory = new List<Can>();
            InitializeSodaSelection();
        }
        /// <summary>
        /// Soda Machine constructor that creates a new instance of Soda Machine with given amounts of each coin. Will create empty inventory and initialize "available" soda via InitializeSodaSelection() method.
        /// </summary>
        /// <param name="numberOfQuarters">Number of Quarters for Register.</param>
        /// <param name="numberOfDimes">Number of Dimes for Register.</param>
        /// <param name="numberOfNickels">Number of Nickels for Register.</param>
        /// <param name="numberOfPennies">Number of Pennies for Register.</param>
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
            // No need to organize by value at initialization as we insert coins highest value first.
        }
        /// <summary>
        /// Soda Machine constructor that creates a new instance of Soda Machine with given inventory of each soda. Will create empty register and initialize "available" soda via InitializeSodaSelection() method.
        /// </summary>
        /// <param name="numberOfRootBeet">Number of Root Beer cans for Inventory.</param>
        /// <param name="numberOfCola">Number of Cola cans for Inventory.</param>
        /// <param name="numberOfOrangeSoda">Number of Orange Soda cans for Inventory.</param>
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
        /// <summary>
        /// Soda Machine constructor that creates a new instance of Soda Machine with given amounts of each coin and given inventory of each soda. Initialize "available" soda via InitializeSodaSelection() method. *It is recognized how unnecessarily large this looks.
        /// </summary>
        /// <param name="numberOfQuarters">Number of Quarters for Register.</param>
        /// <param name="numberOfDimes">Number of Dimes for Register.</param>
        /// <param name="numberOfNickels">Number of Nickels for Register.</param>
        /// <param name="numberOfPennies">Number of Pennies for Register.</param>
        /// <param name="numberOfRootBeet">Number of Root Beer cans for Inventory.</param>
        /// <param name="numberOfCola">Number of Cola cans for Inventory.</param>
        /// <param name="numberOfOrangeSoda">Number of Orange Soda cans for Inventory.</param>
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
            // No need to organize by value at initialization as we insert coins highest value first.

        }
        /// <summary>
        /// Soda Machine construct that takes a list of coins and cans. Stores them in Soda Machine register and inventory respectively. Constructor calls to Order the coins by descending value. Initialize "available" soda via InitializeSodaSelection() method.
        /// </summary>
        /// <param name="coins">List of coins to start in Register.</param>
        /// <param name="cans">List of cans of soda to start in Inventory.</param>
        public SodaMachine(List<Coin> coins, List<Can> cans)
        {
            register = coins;
            inventory = cans;
            InitializeSodaSelection();
            // Anytime an unknown amount of coins are inserted, the machine should auto order/sort the coins into their respective slots, first slot holding highest value of coin.
            CoinCalculator.OrderByValue(ref register);
        }
        /// <summary>
        /// Initializes Soda Machine selection. This represents all soda sold by machine, whether it is in stock or not. Soda selection is currently hard coded. **TODO: Refactor as string and double, removing addition instances of unnecessary objects.
        /// </summary>
        public void InitializeSodaSelection()
        {
            sodaSelection = new List<Can>()
            {
                new RootBeer(),
                new Cola(),
                new OrangeSoda()
            };
        }
        /// <summary>
        /// Main method for processing a transaction on the Soda Machine. It takes a customer, their soda choice and their insert coins. It first validats if the machine can process the transactions. If valid, it will accept and add the coins. Dispense a soda to the customer class and return change if required.
        /// </summary>
        /// <param name="customer">Customer using the Soda Machine</param>
        /// <param name="sodaChoice">A reference to the chosen soda.</param>
        /// <param name="insertedCoins">The list of coins inserted by the customer."</param>
        /// <returns></returns>
        public bool Transaction(Customer customer, string sodaChoiceName, List<Coin> insertedCoins)
        {
            
            // Machine calculates how much money is inserted. Machine/method holds inserted coins during validation before depositing in Register or returning to customer.
            double changeAmount = CoinCalculator.GetValueOfCoins(insertedCoins);
            if (ValidateTransaction(sodaChoiceName, ref changeAmount))
            {
                // If machine validates coin amount, coins immediatly drop into the register.
                AcceptPayment(insertedCoins);
                customer.backpack.AddCan(DispenseSodaCan(sodaChoiceName)); //TODO: Refactor so Customer *Takes soda, and puts in backpack.
                UserInterface.DisplayCan(sodaChoiceName);
                UserInterface.ClearGreenScreen();
                UserInterface.DisplayMainScreen();
                UserInterface.WriteLiteralColor($"ͰGEBLPlease remember to\ntake your change.\nPress Any Key...", 74, 16);
                Console.ReadKey(true);
                DispenseCoins(changeAmount, customer); // TODO: Refactor so Customer *Recieves dispensed Change.
                UserInterface.ClearGreenScreen();
                UserInterface.WriteLiteralColor($"ͰGEBLEnjoy your {sodaChoiceName}!\nHave a Great Day!\nPress Any Key...", 74, 16);
                Console.ReadKey(true);
                return true;
            }
            // If machine fails to validate, coins inserted are immediately rerouted back to the customer.
            UserInterface.ClearGreenScreen();
            UserInterface.WriteLiteralColor("ͰGEBLPlease remember to\ntake your change.\nPress Any Key...", 74, 16);
            DispenseCoins(insertedCoins, customer); // TODO: Refactor so Customer *Recieves dispensed Change.
            Console.ReadKey();
            return false;
        }
        public bool Transaction(Customer customer, string sodaChoiceName)
        {
            if (ValidateTransaction(sodaChoiceName, customer.card))
            {
                customer.backpack.AddCan(DispenseSodaCan(sodaChoiceName)); //TODO: Refactor so Customer *Takes soda, and puts in backpack.
                UserInterface.DisplayCan(sodaChoiceName);
                UserInterface.ClearGreenScreen();
                UserInterface.DisplayMainScreen();
                UserInterface.ClearGreenScreen();
                UserInterface.WriteLiteralColor($"ͰGEBLEnjoy your {sodaChoiceName}!\nHave a Great Day!\nPress Any Key...", 74, 16);
                Console.ReadKey(true);
                return true;
            }
            // If machine fails to validate, coins inserted are immediately rerouted back to the customer.
            UserInterface.ClearGreenScreen();
            UserInterface.WriteLiteralColor("ͰGEBLPlease remember to\nHave a good day!.\nPress Any Key...", 74, 16);
            Console.ReadKey();
            return false;
        }
        /// <summary>
        /// Method holding all the validation checks for the soda machine. Validates soda inventory and validates money transfer can occur.
        /// </summary>
        /// <param name="sodaChoiceName">Name of soda chosen.</param>
        /// <param name="changeAmount">Amount of change inserted into machine. Returns as amount of change to return.</param>
        /// <returns>Returns True is valid transaction. If true changeAmount becomes amount of change to return to customer.</returns>
        public bool ValidateTransaction(string sodaChoiceName, ref double changeAmount)
        {
            if (ValidateInStock(sodaChoiceName))
            {
                UserInterface.MachineValidating("Validating Soda", $"{sodaChoiceName} Is In-Stock", 0);
                if (ValidateMoneyTransfer(sodaChoiceName, ref changeAmount))
                {
                    UserInterface.MachineValidating("Validating Coins", "Cash Accepted", 1);
                    return true;
                }
                UserInterface.MachineValidating("Validating Coins", "Sorry, transaction cannot\nbe completed.", 1);
                return false;
            }
            UserInterface.MachineValidating("Validating Soda Choice", $"{sodaChoiceName}\nis OUT-OF-STOCK", 0);
            return false;
        }
        public bool ValidateTransaction(string sodaChoiceName, Card customerCard)
        {
            if (ValidateInStock(sodaChoiceName))
            {
                UserInterface.MachineValidating("Validating Soda", $"{sodaChoiceName} Is In-Stock", 0);
                if (ValidateMoneyTransfer(sodaChoiceName, customerCard))
                {
                    UserInterface.MachineValidating("Validating Payment", "Payment Accepted", 1);
                    return true;
                }
                UserInterface.MachineValidating("Validating Payment", "Sorry, insufficient funds.", 1);
                return false;
            }
            UserInterface.MachineValidating("Validating Soda Choice", $"{sodaChoiceName} is\nOUT-OF-STOCK", 0);
            return false;
        }
        /// <summary>
        /// Validates that requested soda is in stock in the soda machine inventory.
        /// </summary>
        /// <param name="soda">Soda to check in stock.</param>
        /// <returns></returns>
        public bool ValidateInStock(string sodaName)
        {
            return inventory.Exists(x => x.name == sodaName);
        }
        /// <summary>
        /// Validates the amount of money inserted against inventory according to soda name.
        /// </summary>
        /// <param name="sodaChoiceName">Name of chosen soda.</param>
        /// <param name="changeAmount"> Amount of change inserted into vending machine.</param>
        /// <returns></returns>
        public bool ValidateMoneyTransfer(string sodaChoiceName, ref double changeAmount)
        {
            // First check price against amount of change inserted.
            double price = sodaSelection.Find(x => x.name == sodaChoiceName).Price;
            if (changeAmount >= price)
            {
                // Round to account for Double errors.
                changeAmount = Math.Round(changeAmount - price, 2);
                if (ValidateAvailableChange(changeAmount))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool ValidateMoneyTransfer(string sodaChoiceName, Card customerCard)
        {
            // First check price against amount of change inserted.
            double price = sodaSelection.Find(x => x.name == sodaChoiceName).Price;
            if (customerCard.Withdraw(price))
            {
                AcceptPayment(price);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Validates the Soda Machine's capacity to return the correct amount of change to the customer based on coins available in the register.
        /// </summary>
        /// <param name="changeAmount"></param>
        /// <returns></returns>
        public bool ValidateAvailableChange(double changeAmount)
        {
            if (changeAmount == 0)
            {
                return true;
            }
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
        /// <summary>
        /// Accepts coins into the register, should be called after validation processes.
        /// </summary>
        /// <param name="insertedCoins">Coins inserted by customer.</param>
        public void AcceptPayment(List<Coin> insertedCoins)
        {
            register.InsertRange(0, insertedCoins); 
            CoinCalculator.OrderByValue(ref register); // Reorder register with highest value coins first
        }
        public void AcceptPayment(double amount)
        {
            string magic = "Payment details confirmed by card company. Money is automatically deposited.";
        }
        /// <summary>
        /// Dispenses coints directly to customer and removes from register.
        /// </summary>
        /// <param name="amountToDispense">Value of change to dispense.</param>
        /// <param name="customer">Customer refernce to dispense coins unto.</param>
        public void DispenseCoins(double amountToDispense, Customer customer)
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
       /// <summary>
       /// Dispenses coins inserted by customer and held in change compartment during validation. Upon failure to validate transaction, coins are released back to customer.
       /// </summary>
       /// <param name="insertedCoins">Original coins inserted to customer to be dispensed back.</param>
       /// <param name="customer">Customer who inserted coins.</param>
        public void DispenseCoins(List<Coin> insertedCoins, Customer customer)
        {
            customer.RecieveChange(ref insertedCoins);
        }
        /// <summary>
        /// Dispenses a can of soda matching the soda name input by customer.
        /// </summary>
        /// <param name="sodaChoiceName">Name of chosen soda by a customer.</param>
        /// <returns>Selected can of soda matching input soda name.</returns>
        public Can DispenseSodaCan(string sodaChoiceName)
        {
            int index = inventory.FindIndex(x => x.name == sodaChoiceName);
            inventory.RemoveAt(index);
            switch (sodaChoiceName)
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




    }
}
