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

        // Simulation variables
        

        public Simulation()
        {
            customer = new Customer();
            sodaMachine = new SodaMachine();
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
            InitializeSimuation();

            do
            {
                // Ask user if they would like a soda.
                if(!UserInterface.GetUserInputYesNo())
                {
                    break;
                }

                // Display Sodas and Prices
                // DisplaySodaOfferings();
                // Ask for user input. 
                UserInterface.AskForSodaSelection();

                // Display selection choice: Name/Price
                // Ask for what coins they would like to input.
                // Compare list of coins to wallet contents.
                // If match, send to machine with soda choice
                if(sodaMachine.CheckTransAction(soda, customerCoins))
                {

                }



            } while (true); // While customer still using soda machine.




        }






        #region Initialization Methods
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
