using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineSim
{
    /// <summary>
    /// Abstract class defining a Coin. Coins have a monetary value and name.
    /// </summary>
    abstract public class Coin
    {
        protected double value;
        public string name;
        public double Value { get { return value; } }

        
    }
}
