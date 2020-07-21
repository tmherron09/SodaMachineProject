using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Simulation
    {
        SodaMachine sodaMachine;
        Customer customer;

        public Simulation()
        {
            customer = new Customer();
            sodaMachine = new SodaMachine();
        }

        // RunSimulation()
        // Call the user interface for Customer and Soda Machine Interaction.
        // Go between for Customer and SodaMachine class
        // Call to ask for which soda the user wants.
        // Ask soda machine if soda is left. if true continue, if not go back to start.
        // Call to ask user to choose how much change they want to insert.
        // Ask soda machine if enough change is inserted. if false, ask for coin input again.
        // Ask soda machine if enough coin is available for giving change. If false, inform customer. Ask if new soda or new insert change?
        // If enough change and coin return, Get coin from customer and give coin to soda machine.
        // Recieve change from soda machine and give to Customer.
        // Recieve soda and give soda to customer to put in backpack.
        // Start over.

    }
}
