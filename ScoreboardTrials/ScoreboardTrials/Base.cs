using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardTrials
{
    public class Base
    {
       
        public bool isLoaded { get; set; }
        public bool isForced { get; set; }
        public bool isApproached { get; set; }
    
        public int baseNumber { get; set; }
        public string name { get; set; }

        public Base(int baseNum, string Name)
        {
            isLoaded = false;
            isForced = false;
            isApproached = false;            
            baseNumber = baseNum;
            name = Name;
        }
    }
}
