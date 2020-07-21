using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        List<Coin> register;
        List<Can> inventory;

        // On construct define starting coins
        public SodaMachine()
        {

        }

        // Methods

        // Check if can is available.
        // Return the price of a can.
        // Calculate if enough coins inserted.
        // Calculate Change value
        // Calculate if enough coins can be returned.
        // If enough coins inserted and enough change, dispense soda and subtract from inventory
        // Give change and subtract from register.

        public List<Coin> AcceptPayment(List<Coin> payment, Can soda)
        {
            // Add coins
            throw new NotImplementedException();
        }
        public Can DispenseSodaCan(Can soda)
        {
            throw new NotImplementedException();
        }
    }
}
