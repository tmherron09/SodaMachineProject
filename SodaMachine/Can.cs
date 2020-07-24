using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineSim
{
    /// <summary>
    /// Abstract class definining cans of Soda. Cans have a price per can and a name.
    /// </summary>
    abstract public class Can
    {
        protected double price;
        public string name;
        public double Price { get { return price; } protected set { this.price = value; } }

    }
}
