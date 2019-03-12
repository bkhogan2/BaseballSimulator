using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ScoreboardTrials
{
    class RunnerLabel : Label
    {
        private int baseLocation;
        private int startingBaseLocation;

        public int BaseLocation
        {
            get
            {
                return baseLocation;
            }
            set
            {
                baseLocation = value;
            }
        }

        public int StartingBaseLocation
        {
            get
            {
                return startingBaseLocation;
            }
            set
            {
                startingBaseLocation = value;
            }
        }

    }
}
