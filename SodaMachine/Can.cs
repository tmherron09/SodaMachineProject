using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract public class Can
    {
        protected double price;
        public string name;
        public double Price { get { return price; } protected set { this.price = value; } }



    }
}
