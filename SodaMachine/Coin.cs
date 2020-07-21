using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract class Coin
    {
        private double value;
        private string name;
        public double Value { get { return value; } protected set { this.value = value; } }
        public string Name { get { return name; } protected set { this.name = value; } }
        

        public static int Alphabetize(string x, string y)
        {
            if(x == null)
            {
                if(y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            } 
            else if (y == null)
            {
                return 1;
            }
            char a = x[0];
            char b = y[0];
            if( a == b)
            {
                return 0;
            }
            if(a < b)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }
    }
}
