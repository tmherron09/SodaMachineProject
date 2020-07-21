using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Backpack
    {
        public List<Can> cans { get; private set; }

        public Backpack()
        {
            cans = new List<Can>();
        }

        // Backpack can recieve cans.
        // Backpack can report how many cans.

        public void AddCan(Can soda)
        {
            cans.Add(soda);
            throw new NotImplementedException();
        }
        public void ReportContents()
        {
            throw new NotImplementedException();
        }

    }
}
