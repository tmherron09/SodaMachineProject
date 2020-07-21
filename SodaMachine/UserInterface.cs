﻿using System;
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
            List<string> names = new List<string>();
            int counter = 0;
            Console.WriteLine("This Soda Machine offers: ");
            for (int i = 0; i < cans.Count; i++)
            {
                if (!names.Contains(cans[i].Name))
                {
                    counter++;
                    names.Add($"{cans[i].Name,10}${cans[i].Price:#0.00}");
                    Console.WriteLine($"{counter}) {cans[i].Name}");
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
        public static string AskForSodaSelection()
        {

            throw new NotImplementedException();
        }










        // Get user input methods, YES/NO, Multiple Choice


        public static bool GetUserInputYesNo(string message)
        {
            Console.WriteLine(message);

            string userChoice;
            while (true)
            {
                Console.Clear();
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
