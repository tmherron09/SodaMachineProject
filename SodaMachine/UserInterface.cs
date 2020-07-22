using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public static class UserInterface
    {


        // Display Messages, Text or List methods.

        public static void DisplayList(List<Coin> coins)
        {
            List<string> names = new List<string>();
            int counter = 0;
            Console.WriteLine("This list contains:");
            for (int i = 0; i < coins.Count; i++)
            {
                if (!names.Contains(coins[i].name))
                {
                    counter++;
                    names.Add(coins[i].name);
                    Console.WriteLine($"{counter}) {coins[i].name}");
                }
            }
        }
        public static void DisplayList(List<Can> cans)
        {
            List<string> lineText = new List<string>();
            int counter = 0;
            Console.WriteLine("This Soda Machine offers: ");
            for (int i = 0; i < cans.Count; i++)
            {
                if (!lineText.Contains(cans[i].Name))
                {
                    counter++;
                    lineText.Add(cans[i].Name);
                    Console.WriteLine($"{counter + 1}) {cans[i].Name}");
                }
            }
        }
        public static void DisplayList(string[] textLines, bool hasNewLineEscapeCharacter)
        {
            if (hasNewLineEscapeCharacter)
            {
                foreach (string text in textLines)
                {
                    Console.Write(text);
                }
            }
            else
            {
                foreach (string text in textLines)
                {
                    Console.WriteLine(text);
                }
            }
        }
        public static void DisplayList(string text)
        {
            Console.WriteLine(text);
        }
        public static void DisplayList(string text, bool clearScreen)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
        }
        public static void DisplayList(string text, bool clearScreen, bool showPressAnyKey)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
            if (showPressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
            }
        }
        public static void DisplayList(string text, bool clearScreen, bool showPressAnyKey, bool clearScreenAfterKeyPress)
        {
            if(clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
            if(showPressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
            }
            if(clearScreen)
            {
                Console.Clear();
            }
        }

        public static int AskForCoinAmount(string coinName, int amountInWallet)
        {
            bool valid = false;
            int userInput;
            do
            {
                Console.WriteLine($"You have {amountInWallet} {(amountInWallet == 1 ? coinName : coinName + "s")} in your wallet.\nHow many would you like to insert into the soda machine?\n");
                Console.WriteLine("Amount: ");
                valid = Int32.TryParse(Console.ReadLine(), out userInput);
                if(valid)
                {
                    if(userInput < 0 || userInput > amountInWallet)
                    {
                        DisplayList("You do not have that many coins in your wallet.", false, true, true);
                        valid = false;
                        Console.Clear();
                    }
                }
            } while (!valid);
            return userInput;
        }

        public static void DisplayListOfCoins(List<Coin> coins)
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
            // Display contents
            string[] coinList = new string[] { $"Quarters: {numberOfQuarters}\n", $"Dimes: {numberOfDimes}\n", $"Nickels: {numberOfNickels}\n", $"Pennies: {numberOfPennies}\n" };
            DisplayList(coinList, true);
        }



        public static void DisplayListOfCoins(List<Coin> coins, string firstLineText, string lineTextModifier)
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
            // Display contents
            // Change from singular to plural. Zero will use the plural form given English something rule. 0 coins, not 0 coin.
            string[] coinList = new string[] 
            { 
                $"{firstLineText}\n",
                $"{lineTextModifier} {numberOfQuarters} {(numberOfQuarters == 1 ? "quarter": "quarters" )}.\n",
                $"{lineTextModifier} {numberOfDimes} {(numberOfDimes == 1 ? "dime": "dimes" )}.\n",
                $"{lineTextModifier} {numberOfNickels} {(numberOfNickels == 1 ? "nickel": "nickels" )}.\n",
                $"{lineTextModifier} {numberOfPennies} {(numberOfPennies == 1 ? "penny": "pennies" )}.\n",
            };
            DisplayList(coinList, true);
        }





        // Display for specific Objects
        public static void DisplayContentsOfCustomerWallet(Customer customer)
        {
            int numberOfQuarters = 0;
            int numberOfDimes = 0;
            int numberOfNickels = 0;
            int numberOfPennies = 0;
            List<Coin> coins = customer.CheckCustomerWalletContents();
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
            // Display contents
        }
        
        /// <summary>
        /// Asks the user for their selection. Once selected it displays the cost and asks for verification. If the change their mind the loop will start over.
        /// </summary>
        /// <returns>The name of the soda.</returns>
        public static Can AskForSodaSelection(List<Can> sodaSelection)
        {
            string sodaChoiceInput;
            Can sodaChoice = null;
            bool decided = false; // Runs the loops.
            Console.WriteLine("The Soda Machine glows in front of you, before a cold beverage awaits!");
            do
            {
                
                Console.WriteLine("Which soda would you like to purchase today?");
                
                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    Console.WriteLine($"{i+ 1}) {sodaSelection[i].Name}");
                }
                bool valid = false;
                sodaChoiceInput = Console.ReadLine();
                for(int i = 0; i < sodaSelection.Count; i++)
                {
                    if (sodaChoiceInput == $"{i + 1}" || sodaChoiceInput.ToLower() == sodaSelection[i].Name.ToLower())
                    {
                        valid = true;
                        sodaChoice = sodaSelection[i];
                        if (GetUserInputYesNo($"{sodaSelection[i].Name} is ${sodaSelection[i].Price:#.00}. Are you sure? (Y/N)", false))
                        {
                            decided = true;
                        }
                    }
                }
                if(!valid)
                {
                    Console.WriteLine("Invalid selection, try again. Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            } while (!decided || sodaChoice == null);
            return sodaChoice;
        }










        // Get user input methods, YES/NO, Multiple Choice


        public static bool GetUserInputYesNo(string message, bool clearScreen)
        {
            string userChoice;
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }
                Console.Write(message);
                userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y" || userChoice == "yes")
                {
                    return true;
                }
                else if (userChoice == "n" || userChoice == "no")
                {
                    return false;
                }
            }
        }





        // Get user Coin Selection.




    }
}
