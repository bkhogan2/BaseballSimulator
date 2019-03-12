using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardTrials
{
    class ScoreBoard
    {
        private static int outs = 0;
        private static int strikes = 0;
        private static int balls = 0;
        private static int fouls = 0;

        private static int homeHits { get; set; }
        private static int awayHits { get; set; }

        private static int inningScore = 0;
        private static int innings = 0;         

        public static bool inningsChanged = false;
        public static bool runnerIsOut = false;
        public static bool runScored = false;

        public static void resetScoreboard ()
        {
            outs = 0;
            strikes = 0;
            balls = 0;
            fouls = 0;

       
            inningScore = 0;
            innings = 0;

            inningsChanged = false;
            runnerIsOut = false;
            runScored = false;
    }

        public static int Balls
        {
            get
            {
                return balls;
            }

            set
            {
                balls = value;

                if (balls == 4)
                {
                    ScoreBoard.inningScore = 1;
                    balls = 0;
                }
            }
        }


        public static int Strikes
        {
            get
            {
                return strikes;
            }

            set
            {
                strikes = value;

                if (strikes == 3)
                {
                    ScoreBoard.Outs += 1;
                }
            }
        }

        public static int Outs
        {
            get
            {
                return outs;
            }

            set
            {
                outs = value;
            }
        }

        public static int Innings
        {
            get
            {
                return innings;
            }

            set
            {
                innings = value;
            }
        }

        public static int InningScore
        {
            get
            {
                return inningScore;
            }

            set
            {
                inningScore = value;

                if (value > 0)
                {
                    runScored = true;
                }
            }
        }

        public static int Fouls
        {
            get
            {
                return fouls;
            }
            set
            {
                if (strikes < 2)
                {
                    strikes = value;
                }
            }
        }

        public static void newInning()
        {
            ScoreBoard.innings += 1;
            ScoreBoard.Strikes = 0;
            ScoreBoard.Balls = 0;
            ScoreBoard.Outs = 0;
        }

        public static void recordOut()
        {
            outs += 1;
            Strikes = 0;
            Balls = 0;
        }

        public static void recordStrike()
        {
            strikes += 1;
        }

        public static void recordBall()
        {
            balls += 1;
        }

        public static void recordFoul()
        {
            if(strikes < 2)
            {
                strikes += 1;
            }
        }

        public static void recordScore()
        {

        }

        public static void newCount()
        {
            strikes = 0;
            balls = 0;
        }
    }
}


