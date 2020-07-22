using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract public class Coin
    {
        protected double value;
        public string name;
        public double Value { get { return value; } }
        //public string Name { get { return name; } protected set { this.name = value; } }
        

        public static void OrderByValue(ref List<Coin> coins)
        {
            coins = coins.OrderByDescending(x => x.Value).ToList();
        }
        public static void OrderByValue(Wallet wallet)
        {
            wallet.coins = wallet.coins.OrderByDescending(x => x.Value).ToList();
        }
    }
}
