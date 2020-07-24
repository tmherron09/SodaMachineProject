using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineSim
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * 
             * 
             * If you have issues with size. Right click the cmd frame 
             * and select "Properties" "Font" a
             * select the Rasterized Font 8x8.
             * You may also do this for a different appearence 
             * than standard console.
             * Graphics Are designed for a 120,35
             *
             *
             */
            
            
            Console.SetWindowSize(120, 35);
            Console.CursorVisible = false;

            
            Simulation simulation = new Simulation(true);
            simulation.RunSimulation();
            Console.ReadKey(true);
        }
    }
}
