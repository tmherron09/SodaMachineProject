using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineSim
{
    public class Backpack
    {
        public List<Can> cans;

        public Backpack()
        {
            cans = new List<Can>();
        }
        public void AddCan(Can soda)
        {
            cans.Add(soda);
            
        }

    }
}
