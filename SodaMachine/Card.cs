using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineSim
{
    public class Card
    {
        private double availableFunds;
        public double AvailableFunds { get 
            { 
                return availableFunds;
            }
            private set
            { 
                availableFunds = value; 
            } 
        }
        /// <summary>
        /// Generic Card constructor with no funds initialized.
        /// </summary>
        public Card()
        {
            AvailableFunds = 0;
        }
        /// <summary>
        /// Card constructor that takes in an amount to put onto the card.
        /// </summary>
        /// <param name="amount">Amount of funds to initialize.</param>
        public Card(double amount)
        {
            AvailableFunds += amount;
        }
        /// <summary>
        /// Method used to add funds to card instance.
        /// </summary>
        /// <param name="amount">Amount to add to card.</param>
        /// <returns></returns>
        public bool Deposit(double amount)
        {
            if (amount > 0)
            {
                AvailableFunds += amount;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Method used to make payments or remove funds from card instance.
        /// </summary>
        /// <param name="amount">Amount to withdraw/remove from card.</param>
        /// <returns></returns>
        public bool Withdraw(double amount)
        {
            if(AvailableFunds >= amount)
            {
                AvailableFunds -= amount;
                return true;
            }
            return false;

        }
    }
}
