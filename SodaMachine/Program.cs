using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set window to largest height.
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.CursorVisible = false;


            Simulation simulation = new Simulation(true);
            simulation.RunSimulation();
            Console.ReadLine();
        }
    }
}
