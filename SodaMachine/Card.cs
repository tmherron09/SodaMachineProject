using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Card
    {
        private double availableFunds;

        public double AvailableFunds { get; private set; }

        public Card()
        {
            AvailableFunds = 0;
        }
        public Card(double amount)
        {
            AvailableFunds += amount;
        }
        public bool Deposit(double amount)
        {
            if (amount > 0)
            {
                AvailableFunds += amount;
                return true;
            }
            return false;
        }
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
