using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            for(int i = 0; i < coins.Count; i++)
            {
                if(!names.Contains(coins[i].Name))
                {
                    counter++;
                    names.Add(coins[i].Name);
                    Console.WriteLine($"{counter}) {coins[i].Name}");
                }
            }
    
        }




        // Get user input methods, YES/NO, Multiple Choice





        // Get user Coin Selection.




    }
}
