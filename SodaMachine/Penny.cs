using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Penny : Coin
    {
        public Penny()
        {
            value = .01;
            name = "penny";
        }
    }
}
