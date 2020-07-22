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
        
        private static int heightOfSodaMachineOutline = 20;
        public static int topPadding = (Console.LargestWindowHeight - heightOfSodaMachineOutline) / 2;
        private static int leftPadding = 10;

        // Display Messages, Text or List methods.


        /// <summary>
        /// Default Write string to screen. Best for single line strings. No new line at end of string.
        /// </summary>
        /// <param name="text">A single line string</param>
        public static void DisplayList(string text)
        {
            Console.WriteLine(text);
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
                Console.ReadKey();
            }
        }
        public static void DisplayList(string text, bool clearScreen, bool showPressAnyKey, bool clearScreenAfterKeyPress)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
            if (showPressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            if (clearScreen)
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
                if (valid)
                {
                    if (userInput < 0 || userInput > amountInWallet)
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


        public static void PrintList(List<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                Console.WriteLine(coin.name);
            }
        }


        // Display for specific Objects
        /// <summary>
        /// Asks the user for their selection. Once selected it displays the cost and asks for verification. If the change their mind the loop will start over.
        /// </summary>
        /// <returns>The name of the soda.</returns>
        public static Can AskForSodaSelection(List<Can> sodaSelection)
        {
            string sodaChoiceInput;
            Can sodaChoice = null;
            bool decided = false; // Runs the loops.
            Console.SetCursorPosition(leftPadding + 25, topPadding - 1);
            TextAndBackgroundColor(ConsoleColor.Green, ConsoleColor.White);
            Console.WriteLine("The Soda Machine glows in front of you, before a cold beverage awaits!");
            do
            {
                DrawSodaMachineOutLine();
                TextAndBackgroundColor(ConsoleColor.White, ConsoleColor.Black);
                string message = "";
                message += "Which soda would you like to purchase today?\n";

                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    message += $"{i + 1}) {sodaSelection[i].name}\n";
                }
                WriteLiteral(message, leftPadding + 35, topPadding + 4);
                bool valid = false;
                sodaChoiceInput = Console.ReadLine();
                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    if (sodaChoiceInput == $"{i + 1}" || sodaChoiceInput.ToLower() == sodaSelection[i].name.ToLower())
                    {
                        valid = true;
                        sodaChoice = sodaSelection[i];
                        if (GetUserInputYesNo($"{sodaSelection[i].name} is ${sodaSelection[i].Price:#.00}. Are you sure? (Y/N)", false, leftPadding + 35, topPadding + 10))
                        {
                            decided = true;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }
                if (!valid)
                {
                    Console.SetCursorPosition(leftPadding, topPadding + 11);
                    Console.WriteLine("Invalid selection, try again. Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            } while (!decided || sodaChoice == null);
            return sodaChoice;
        }

        // Positional Based UI Methods

        public static void WriteLiteral(string msg, int left, int top)
        {
            int pos = 0;
            msg = msg.Replace("\n", "¶");

            Console.SetCursorPosition(left, top);
            foreach (char letter in msg)
            {
                if (letter == '¶')
                {
                    top += 1;
                    Console.SetCursorPosition(left, top);
                }
                else
                {
                    Console.Write(msg[pos]);
                }
                pos++;
            }
        }

        /*
         * Display a soda machine outline- OutLine X Graphics O
         * Display welcome message either to the right or bottom.
         * 
         * 
         * 
         * Display Thank you and Good Bye
         */




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


        public static bool GetUserInputYesNo(string message, bool clearScreen, int leftPad, int topPad)
        {
            string userChoice;
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }
                WriteLiteral(message, leftPad, topPad);
                Console.SetCursorPosition(leftPad, topPad + 1);
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


        // Decoration


        private static void TextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private static void TextAndBackgroundColor(ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
        }



        /// <summary>
        /// Draw a
        /// </summary>
        public static void DrawScreenDivider()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(new string('█', Console.WindowWidth));
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void DrawSodaMachineOutLine()
        {
            // Write to one left of leftPadding. Left padding is inside the soda machine outline.
            WriteLiteral(sodaMachineOutline, leftPadding - 1, topPadding);

        }

        // Drawings

        public static string sodaMachineOutline = @"██████████████████████
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
█                    █
██████████████████████";















        #region Unused/ Depreciated


        //public static void DisplayListOfCoins(List<Coin> coins, string firstLineText, string lineTextModifier)
        //{
        //    int numberOfQuarters = 0;
        //    int numberOfDimes = 0;
        //    int numberOfNickels = 0;
        //    int numberOfPennies = 0;

        //    foreach (Coin coin in coins)
        //    {
        //        switch (coin.name)
        //        {
        //            case "quarter":
        //                numberOfQuarters++;
        //                break;
        //            case "dime":
        //                numberOfDimes++;
        //                break;

        //            case "nickel":
        //                numberOfNickels++;
        //                break;
        //            case "penny":
        //                numberOfPennies++;
        //                break;
        //        }
        //    }
        //    // Display contents
        //    // Change from singular to plural. Zero will use the plural form given English something rule. 0 coins, not 0 coin.
        //    string[] coinList = new string[]
        //    {
        //        $"{firstLineText}\n",
        //        $"{lineTextModifier} {numberOfQuarters} {(numberOfQuarters == 1 ? "quarter": "quarters" )}.\n",
        //        $"{lineTextModifier} {numberOfDimes} {(numberOfDimes == 1 ? "dime": "dimes" )}.\n",
        //        $"{lineTextModifier} {numberOfNickels} {(numberOfNickels == 1 ? "nickel": "nickels" )}.\n",
        //        $"{lineTextModifier} {numberOfPennies} {(numberOfPennies == 1 ? "penny": "pennies" )}.\n",
        //    };
        //    DisplayList(coinList, true);
        //}

        //public static void DisplayContentsOfCustomerWallet(Customer customer)
        //{
        //    int numberOfQuarters = 0;
        //    int numberOfDimes = 0;
        //    int numberOfNickels = 0;
        //    int numberOfPennies = 0;
        //    List<Coin> coins = customer.CheckCustomerWalletContents();
        //    foreach (Coin coin in coins)
        //    {
        //        switch (coin.name)
        //        {
        //            case "quarter":
        //                numberOfQuarters++;
        //                break;
        //            case "dime":
        //                numberOfDimes++;
        //                break;

        //            case "nickel":
        //                numberOfNickels++;
        //                break;
        //            case "penny":
        //                numberOfPennies++;
        //                break;
        //        }
        //    }
        //    // Display contents
        //}

        //public static void DisplayList(List<Coin> coins)
        //{
        //    List<string> names = new List<string>();
        //    int counter = 0;
        //    Console.WriteLine("This list contains:");
        //    for (int i = 0; i < coins.Count; i++)
        //    {
        //        if (!names.Contains(coins[i].name))
        //        {
        //            counter++;
        //            names.Add(coins[i].name);
        //            Console.WriteLine($"{counter}) {coins[i].name}");
        //        }
        //    }
        //}
        //public static void DisplayList(List<Can> cans)
        //{
        //    List<string> lineText = new List<string>();
        //    int counter = 0;
        //    Console.WriteLine("This Soda Machine offers: ");
        //    for (int i = 0; i < cans.Count; i++)
        //    {
        //        if (!lineText.Contains(cans[i].name))
        //        {
        //            counter++;
        //            lineText.Add(cans[i].name);
        //            Console.WriteLine($"{counter + 1}) {cans[i].name}");
        //        }
        //    }
        //}


        #endregion
    }
}
