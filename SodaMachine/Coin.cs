using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract class Coin
    {
        protected double value;
        private string name;
        public double Value { get { return value; } }
        public string Name { get { return name; } protected set { this.name = Name; } }
        public Coin()
        {

        }
    }
}
