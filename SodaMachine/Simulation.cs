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
        
        /// <summary>
        /// Create new isntance of Simulation with Empty Customer and Soda Machine
        /// </summary>
        public Simulation()
        {
            customer = new Customer();
            sodaMachine = new SodaMachine();
        }
        /// <summary>
        /// Create new instance of Simulation with default user stories values
        /// </summary>
        /// <param name="userstoriesDefaults">True: Set to user stories defaults</param>
        public Simulation(bool userstoriesDefaults)
        {
            if (userstoriesDefaults)
            {
                customer = new Customer(true);
                // Creates a new SodaMachine with the default user stories values
                //sodaMachine = new SodaMachine(20, 10, 20, 50, 5, 5, 5);
                sodaMachine = new SodaMachine(20, 10, 20, 50, 5, 5, 0);
            }
            else
            {
                customer = new Customer();
                sodaMachine = new SodaMachine();
            }
        }



        // RunSimulation()
        // Initialize Simulation
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


        public void RunSimulation()
        {
            //Call Initialization
            //InitializeSimuation();

            bool isBuyingSoda = true;

            while(isBuyingSoda)
            {
                // Display a welcome message.
                bool hasPurchasedSoda = customer.UseSodaMachine(sodaMachine);
                if (!hasPurchasedSoda && !UserInterface.GetUserInputYesNo("We're sorry, would you like to try again?", true))
                {
                    break;
                }

                isBuyingSoda = UserInterface.GetUserInputYesNo("Would you like to buy another soda?", true); // While customer still using soda machine.

            }
            // Display Thank You
        }






        #region Initialization Methods for Non-User stories
        /* Initialization Methods */
        private void InitializeSimuation()
        {
            // initialize a new SodaMachine
            InitializeNewSodaMachine();
            // Call method to initialize new Customer.
            InitializeNewCustomer();
        }

        private void InitializeNewSodaMachine()
        {
            throw new NotImplementedException();
        }

        private void InitializeNewCustomer()
        {
            throw new NotImplementedException();
        } 
        #endregion













    }
}
