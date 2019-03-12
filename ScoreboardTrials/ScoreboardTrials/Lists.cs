using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardTrials
{
    class Lists
    {
        public static string playDescription { get; set; }
        //static ProportionValue<string> ground = ProportionValue.Create(.34, GfieldedList, "a ground ball headed for the ");
        //static ProportionValue<string> lineDrive = ProportionValue.Create(.33, LDcaughtOrFielded, "a line drive headed for the ");
        //static ProportionValue<string> poppedUp = ProportionValue.Create(.33, PUcaughtOrFielded, "popped up and headed for the ");

        //public static ProportionValue<string>[] trajectoryList = new[]
        //{
        //    poppedUp,
        //    lineDrive,
        //    ground
        //};

        static ProportionValue<string> wildStay = ProportionValue.Create(.99, null, "The batter decided to stay.", "balls");
        static ProportionValue<string> wildRun = ProportionValue.Create(.01, null, "The batter runs for it!", null);

        public static ProportionValue<string>[] wildPitchList = new[] {
                wildStay,
                wildRun
        };

        static ProportionValue<string> rightSideline = ProportionValue.Create(.5, null, "hit foul down the right sideline. ", "fouls");
        static ProportionValue<string> leftSideLine = ProportionValue.Create(.5, null, "hit foul down the left sideline.", "fouls");

        public static ProportionValue<string>[] foulList = new[] {
                rightSideline,
                leftSideLine
        };
        
        static ProportionValue<string> fair = ProportionValue.Create(.7, null, "", "hits"); //and the ball is hit!
        static ProportionValue<string> foul = ProportionValue.Create(.3, foulList);

        public static ProportionValue<string>[] contactList = new[] {
               fair,
               foul
        };

        static ProportionValue<string> ball = ProportionValue.Create(.7, null, "and takes a ball.", "balls");
        static ProportionValue<string> strike = ProportionValue.Create(.3, null, "and takes a strike.", "strikes");

        public static ProportionValue<string>[] noSwingList = new[] {
               ball,
               strike
        };

        static ProportionValue<string> contact = ProportionValue.Create(.7, contactList, "");
        static ProportionValue<string> noContact = ProportionValue.Create(.3, null, "and misses for a strike!", "strikes");

        public static ProportionValue<string>[] swingList = new[] {
               contact,
               noContact
        };   

        static ProportionValue<string> wildPitch = ProportionValue.Create(0, wildPitchList, "It was a Wild Pitch! ");
        static ProportionValue<string> swing = ProportionValue.Create(.635, swingList, ""); //The batter swings
        static ProportionValue<string> noSwing = ProportionValue.Create(.365, noSwingList, "The batter stays ");

        public static ProportionValue<string>[] pitchList = new[] {
               swing,
               noSwing,
               wildPitch
        };

        static ProportionValue<string> noThrow = ProportionValue.Create(.7, null, "He did not attempt to throw the ball.");
        static ProportionValue<string> throws = ProportionValue.Create(.3, null, "He tried to throw the runner out.");

        public static ProportionValue<string>[] checkList = new[] {
               throws,
               noThrow
        };
        static ProportionValue<string> pitch = ProportionValue.Create(.999, pitchList, "");
        static ProportionValue<string> check = ProportionValue.Create(.001, checkList, "The pitcher checks the bases. ");

        public static ProportionValue<string>[] pitcherChoiceList = new[] {
                pitch,
                check                
        };

        public static ProportionValue<string> firstPropValue = ProportionValue.Create(1, pitcherChoiceList, string.Empty);

        //this method runs through an outcome of lists and returns their outcomes.
        public static void DoWorkSon(ProportionValue<string> propValue)
        {
            var newPropValueList = propValue.PropValueList;

            if (newPropValueList != null)
            {
                var newDescription = propValue.Value;
                playDescription += newDescription;

                var newChosenList = newPropValueList.ChooseByRandom();
                DoWorkSon(newChosenList);

                if (propValue.InsertValue != null)
                {
                    string switchInsertValue = propValue.InsertValue;

                    //receives the inserted values that will update scoreboard information and trigger methods
                    switch (switchInsertValue)
                    {
                        case "balls":
                            Play.recordBall();                          
                            break;

                        case "strikes":
                            Play.recordStrike();
                            break;

                        case "fouls":
                            Play.recordFoul();
                            break;

                        case "hits":
                            frmBaseballSimulator.startRunPlayTimer();
                            break;
                    }
                }
            }
            else
            {
                var newDescription = propValue.Value;
                playDescription += newDescription;

                Console.WriteLine(playDescription);

                if (propValue.InsertValue != null)
                {
                   
                    string switchInsertValue = propValue.InsertValue;

                    //receives the inserted values that will update scoreboard information and trigger methods
                    switch (switchInsertValue)
                    {
                        case "balls":
                            Play.recordBall();
                            break;

                        case "strikes":
                            Play.recordStrike();
                            break;

                        case "fouls":
                            Play.recordFoul();                            
                            break;

                        case "hits":
                            frmBaseballSimulator.startRunPlayTimer();
                            break;
                    }
                }

                if (propValue.InsertValue != "hits")
                {
                    playDescription = string.Empty;
                }
               
            }
        }

        public static void ResetPlayDescription()
        {
            playDescription = string.Empty;
        }

    }
}
