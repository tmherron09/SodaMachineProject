using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public abstract class Can
    {
        private double price;
        private string name;
        public double Price { get { return price; } protected set { this.price = value; } }
        public string Name { get { return name; } protected set { this.name = value; } }



    }
}
