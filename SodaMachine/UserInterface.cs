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

        public static void DisplayListOfNames(List<Coin> coins)
        {
            List<string> names = new List<string>();
            int counter = 0;
            Console.WriteLine("This list contains:");
            for (int i = 0; i < coins.Count; i++)
            {
                if (!names.Contains(coins[i].Name))
                {
                    counter++;
                    names.Add(coins[i].Name);
                    Console.WriteLine($"{counter}) {coins[i].Name}");
                }
            }
        }
        public static void DisplayListOfNames(List<Can> cans)
        {
            List<string> names = new List<string>();
            int counter = 0;
            Console.WriteLine("This list contains:");
            for (int i = 0; i < cans.Count; i++)
            {
                if (!names.Contains(cans[i].Name))
                {
                    counter++;
                    names.Add(cans[i].Name);
                    Console.WriteLine($"{counter}) {cans[i].Name}");
                }
            }
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
                switch (coin.Name)
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


    






        // Get user input methods, YES/NO, Multiple Choice





        // Get user Coin Selection.




    }
}
