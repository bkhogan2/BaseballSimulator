using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardTrials
{
    public class Player
    {
        private string firstName { get; set; }
        private string lastName { get; set; }

        private int baseLocation { get; set; }

        public Player()
        {
            BaseLocation = 10;
        }
        
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
    }

    class Fielder
    {
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string position { get; set; }
        private int baseLocation { get; set; }

        public Fielder()
        {

        }
        public Fielder(int baseNumber, string pPosition)
        {
            baseLocation = baseNumber;
            position = pPosition;
        }

        public Fielder(string pPosition)
        {

            position = pPosition;
        }

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

        public string Position
        {
            get
            {
                return position;
            }
        }
    }
}
