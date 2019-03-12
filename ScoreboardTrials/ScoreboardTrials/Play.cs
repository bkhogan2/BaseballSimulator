using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreboardTrials
{
    public class Play
    {
        private static double hasItOdds = .1;
        private static double doesntHaveItOdds;
        private const double ONE = 1;

        public static int numToAdvance;

        //private static bool advanceHalf;
        private static bool playersRunning;
        private static bool playerRemoved = false;

        public static List<Base> Bases = new List<Base>();
        private static Base maxForcedBase;
        private static Base maxApproachedBase;
        private static int baseNumberForced = 0;
        public static Timer tmrRunPlay_Tick = new Timer();

        //private static string infielder;
        //private static string outfielder;
        static Base homePlate = new Base(0, "Home Plate");
        static Base firstBase = new Base(1, "First");
        static Base secondBase = new Base(2, "Second");
        static Base thirdBase = new Base(3, "Third");
        static Base homeBase = new Base(4, "Home Base");

        static Queue<Player> dugOutQueue = new Queue<Player>();
        static List<Player> onBaseList = new List<Player>();
        

        static Player player1 = new Player();
        static Player player2 = new Player();
        static Player player3 = new Player();
        static Player player4 = new Player();
        static Player player5 = new Player();
        static Player player6 = new Player();
        static Player player7 = new Player();
        static Player player8 = new Player();
        static Player currentBatter;

        static Fielder firstBaseman = new Fielder(1, "First");
        static Fielder secondBaseman = new Fielder(2, "Second");
        static Fielder thirdBaseman = new Fielder(3, "Third");
        static Fielder catcher = new Fielder(0, "Catcher");
        static Fielder shortStop = new Fielder(10, "Short Stop");
        static Fielder pitcher = new Fielder(10, "Pitcher");
        static Fielder leftFielder = new Fielder(10, "Left Field");
        static Fielder rightFielder = new Fielder(10, "Right Field");
        static Fielder centerFielder = new Fielder(10, "Center Field");

        static Fielder inFielder = new Fielder();
        static Fielder outFielder = new Fielder();

        static List<Fielder> fielderList = new List<Fielder>();

        static ProportionValue<string> runnerOut = ProportionValue.Create(.2, null, "", "out");
        static ProportionValue<string> runnerSafe = ProportionValue.Create(.8, null, "", "safe");

        static ProportionValue<string>[] outSafe = new[]
        {
            runnerOut,
            runnerSafe
        };

        static ProportionValue<string> run = ProportionValue.Create(.5, null, "The runner ran to ", "run");
        static ProportionValue<string> notRun = ProportionValue.Create(.5, null, "The runner stopped at", "notRun");

        static ProportionValue<string> checkIt = new ProportionValue<string>();

        public static ProportionValue<string>[] runnerDecision = new[] {
                run,
                notRun
        };

        static ProportionValue<string> firstBaseMan = ProportionValue.Create(.225, null, "first baseman ");
        static ProportionValue<string> secondBaseMan = ProportionValue.Create(.225, null, "second baseman ");
        static ProportionValue<string> thirdBaseMan = ProportionValue.Create(.225, null, "third baseman ");
        static ProportionValue<string> shortStop1 = ProportionValue.Create(.225, null, "short stop ");
        static ProportionValue<string> pitcher1 = ProportionValue.Create(.1, null, "pitcher ");

        static ProportionValue<string>[] infieldList = new[] {
            firstBaseMan,
            secondBaseMan,
            thirdBaseMan,
            shortStop1,
            pitcher1
        };

        static ProportionValue<string> leftField = ProportionValue.Create(.34, null, "left fielder ");
        static ProportionValue<string> rightField = ProportionValue.Create(.33, null, "right fielder ");
        static ProportionValue<string> centerField = ProportionValue.Create(.33, null, "center fielder ");

        static ProportionValue<string>[] outfieldList = new[] {
            leftField,
            rightField,
            centerField

        };

        static ProportionValue<string> infield = ProportionValue.Create(.235, null, "", "infield");
        static ProportionValue<string> outfield = ProportionValue.Create(.765, null, "", "outfield");

        static ProportionValue<string>[] infieldOrOutfield = new[]
        {
            infield,
            outfield
        };

        static ProportionValue<string> caught = ProportionValue.Create(.3, null, "caught by ", "caught");
        static ProportionValue<string> notCaught = ProportionValue.Create(.7, null, "fielded by ", "notCaught");


        static ProportionValue<string>[] caughtOrNotCaught = new[] {
            caught,
            notCaught
        };        

        static ProportionValue<string> hasIt = ProportionValue.Create(hasItOdds, null, "The fielder fielded the ball ", "hasIt");
        static ProportionValue<string> doesntHaveIt = ProportionValue.Create(1 - hasItOdds, null, "The fielder doesn't have the ball yet", "notYet");

        public static ProportionValue<string>[] hasItOrNot = new[] {
                hasIt,
                doesntHaveIt
        };

        public static void LoadBases()
        {
            if (Bases.Count == 0)
            {
                Bases.Add(homePlate);
                Bases.Add(firstBase);
                Bases.Add(secondBase);
                Bases.Add(thirdBase);
                Bases.Add(homeBase);
            }   

        }

        public static void resetBases()
        {
            Bases.Clear();
        }

        public static void LoadNewDugout()
        {     
            if(dugOutQueue.Count == 0)
            {
                dugOutQueue.Enqueue(player1);
                dugOutQueue.Enqueue(player2);
                dugOutQueue.Enqueue(player3);
                dugOutQueue.Enqueue(player4);
                dugOutQueue.Enqueue(player5);
                dugOutQueue.Enqueue(player6);
                dugOutQueue.Enqueue(player7);
                dugOutQueue.Enqueue(player8);
            }      
                      
        }
        public static void resetOldDugout()
        {

        }

        public static void LoadFielders()
        {
            fielderList.Add(firstBaseman);
            fielderList.Add(secondBaseman);
            fielderList.Add(thirdBaseman);
            fielderList.Add(catcher);
        }

        public static void LoadAllObjects()
        {
            LoadBases();
            LoadNewDugout();
            LoadFielders();
        }

        

        public static void resetAllObjects()
        {

        }

        public static void putCurrentBatterOnFirst()
        {
            onBaseList.Add(currentBatter);
            onBaseList.ElementAt(onBaseList.Count - 1).BaseLocation = 0;
        }

        public static void DoStuff()
        {
            if (Play.Bases.Count == 0)
            {
                Play.LoadBases();
            }

            if (Play.dugOutQueue.Count == 0)
            {
                LoadNewDugout();
            }

            newInning();                       
        }

        public static void setCurrentBatter()
        {
            currentBatter = dugOutQueue.Dequeue();
            onBaseList.Add(currentBatter);
            currentBatter.BaseLocation = 0;
            Bases.ElementAt(0).isLoaded = true;
            frmBaseballSimulator.addLabelToBases();
        }

        private static void advanceCurrentBatter(Player player)
        {
            Bases.ElementAt(0).isApproached = true;
            player.BaseLocation = -(player.BaseLocation + 1);
        }

        private static void currentBatterMadeIt(Player player)
        {
            if(player.Equals(currentBatter))
            {
                Bases.ElementAt(0).isLoaded = true;
                Bases.ElementAt(0).isApproached = false;
                player.BaseLocation = 0;
                onBaseList.Add(player);
                currentBatter = null;
            }
        }

        private static void removePlayerAfterOut(Player player, bool boolInfieldProgression, bool outfieldProgression)
        {
            
            int playerBaseLocation = player.BaseLocation;

            if(player == currentBatter)
            {
                ScoreBoard.Strikes = 0;
                ScoreBoard.Balls = 0;
            }

            if (player.BaseLocation < 0)
            {
                if (boolInfieldProgression)
                {
                    ScoreBoard.recordOut();
                    frmBaseballSimulator.addPlayDescription();

                    Bases.ElementAt(-player.BaseLocation).isApproached = false;
                    player.BaseLocation = 10;

                    dugOutQueue.Enqueue(player);
                    onBaseList.Remove(player);

                    if(onBaseList.Count > 0)
                    {
                        setRunnersNewBase();
                    }                    

                    frmBaseballSimulator.startAdvanceHalfTimer(numToAdvance, true, -playerBaseLocation - 1);
                }
                else
                {
                    ScoreBoard.recordOut();
                    frmBaseballSimulator.addPlayDescription();

                    Bases.ElementAt(-player.BaseLocation).isApproached = false;
                    player.BaseLocation = 10;

                    dugOutQueue.Enqueue(player);
                    onBaseList.Remove(player);

                    if (onBaseList.Count > 0)
                    {
                        setRunnersNewBase();
                    }

                    frmBaseballSimulator.startAdvanceHalfTimer(numToAdvance, true, -playerBaseLocation - 1);
                }
            }
            else
            {
                frmBaseballSimulator.recordOut(player.BaseLocation);
                frmBaseballSimulator.clearCount();

                ScoreBoard.recordOut();
                
                Bases.ElementAt(player.BaseLocation + 1).isApproached = false;
                player.BaseLocation = 10;

                dugOutQueue.Enqueue(player);
                onBaseList.Remove(player);
            }

            if (player.Equals(currentBatter))
            {
                currentBatter = null;
                
            }
        }

        private static void removePlayerAfterOut(Player player)
        {
            
            if (player.BaseLocation < 0)
            {
                frmBaseballSimulator.recordOut(-player.BaseLocation - 1);
                frmBaseballSimulator.clearCount();

                ScoreBoard.recordOut();

                Bases.ElementAt(-player.BaseLocation).isApproached = false;
                player.BaseLocation = 10;

                dugOutQueue.Enqueue(player);
                onBaseList.Remove(player);

            }
            else
            {
                frmBaseballSimulator.recordOut(player.BaseLocation);
                frmBaseballSimulator.clearCount();

                ScoreBoard.recordOut();

                Bases.ElementAt(player.BaseLocation + 1).isApproached = false;
                player.BaseLocation = 10;

                dugOutQueue.Enqueue(player);
                onBaseList.Remove(player);
            }

            if (player.Equals(currentBatter))
            {
                currentBatter = null;

            }

            Play.endPlay();
        }

        public static void removePlayerAfterScore()
        {
            frmBaseballSimulator.recordScore();            

            for (int i = 0; i < onBaseList.Count; i++)
            {
                if (onBaseList.ElementAt(i).BaseLocation == 4 || onBaseList.ElementAt(i).BaseLocation == -4)
                {
                    dugOutQueue.Enqueue(onBaseList.ElementAt(i));
                    onBaseList.ElementAt(i).BaseLocation = 10;
                    onBaseList.Remove(onBaseList.ElementAt(i));
                    
                }
            }
           
            Bases.ElementAt(4).isLoaded = false;
            Bases.ElementAt(4).isApproached = false;

            
        }

        private static void resetRunners()
        {
            foreach (Player player in onBaseList)
            {
                setRunnerOldBase(player);
            }
        }

        private static Base determineBasePriority()
        {
            determineMaxApproachedBase();

            if (Bases.ElementAt(4).isApproached)
            {
                return Bases.ElementAt(4);
            } else if (maxForcedBase != null)
            {
                return maxForcedBase;
            } else if (maxApproachedBase != null)
            {
                return maxApproachedBase;
            }

            return null;
        }

        private static void determineMaxForcedBase()
        {
            for (int i = 0; i < Bases.Count; i++)
            {
                if (Bases.ElementAt(i).isApproached)
                {
                    maxForcedBase = Bases.ElementAt(i);
                }
            }
        }

        private static void determineMaxApproachedBase()
        {
            maxApproachedBase = null;

            for (int i = 0; i < Bases.Count; i++)
            {
                if (Bases.ElementAt(i).isApproached)
                {
                    maxApproachedBase = Bases.ElementAt(i);
                }
            }            
        }

        private static void infieldChallengeRunner(Fielder fielder)
        {
            frmBaseballSimulator.addToLblTest(" infield challenge");

            Base priority = determineBasePriority();

            if (priority.baseNumber != fielder.BaseLocation)
            {
                frmBaseballSimulator.addToLblTest(" priority.baseNumber != fielder.BaseLocation");
                if (priority.baseNumber == 1)
                {
                    frmBaseballSimulator.addToLblTest(" priority.baseNumber == 1");
                    Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";

                    removePlayerAfterOut(currentBatter, true, false);
                }
                else
                {
                    frmBaseballSimulator.addToLblTest(" else");

                    for (int i = 0; i < onBaseList.Count; i++)
                    {
                        if (-onBaseList.ElementAt(i).BaseLocation == priority.baseNumber)
                        {
                            Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";
                            removePlayerAfterOut(onBaseList.ElementAt(i), true, false);
                            playerRemoved = true;
                        }
                    }

                    if (playerRemoved == false)
                    {
                        priority.baseNumber = onBaseList.ElementAt(0).BaseLocation;

                        Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";

                        removePlayerAfterOut(onBaseList.ElementAt(0), false, true);
                    }

                    playerRemoved = false;
                }
               
            } else
              {
                    frmBaseballSimulator.addToLblTest(" else");
                    if (priority.baseNumber == 1)
                    {

                    frmBaseballSimulator.addToLblTest(" priority.baseNumber == 1");
                    Lists.playDescription += "Out at 1st!\n\n";
                        removePlayerAfterOut(currentBatter, true, false);
                    }
                    else
                    {
                    frmBaseballSimulator.addToLblTest(" else");
                        for (int i = 0; i < onBaseList.Count; i++)
                        {
                            if (-onBaseList.ElementAt(i).BaseLocation == priority.baseNumber)
                            {                                
                                Lists.playDescription += "Out at " + priority.name + "!\n\n";
                                removePlayerAfterOut(onBaseList.ElementAt(i), true, false);
                                playerRemoved = true;
                            }
                        }

                        if (playerRemoved == false)
                        {
                            priority.baseNumber = onBaseList.ElementAt(0).BaseLocation;

                            Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";

                            removePlayerAfterOut(onBaseList.ElementAt(0), true, false);
                        }

                        playerRemoved = false;
                }                              
            }
        }

        private static void outFieldChallengeRunner(Fielder fielder)
        {
            frmBaseballSimulator.addToLblTest(" outfieldchallengerunner");
            Base priority = determineBasePriority();

            ProportionValue<string> outOrSafe = outSafe.ChooseByRandom();

            if (outOrSafe.InsertValue == "out")
            {
                frmBaseballSimulator.addToLblTest(" out");
                if (priority.baseNumber == 1)
                {
                    frmBaseballSimulator.addToLblTest(" priority.baseNumber == 1");
                    addHitPlayDescription();
                    Lists.playDescription += "Out at " + priority.name + "! (" + fielder.Position + ")\n\n";
                    
                    removePlayerAfterOut(currentBatter, false, true);
                }
                else
                {
                    frmBaseballSimulator.addToLblTest(" else");
                    addHitPlayDescription();
                    for (int i = 0; i < onBaseList.Count; i++)
                    {
                        if (-onBaseList.ElementAt(i).BaseLocation == priority.baseNumber)
                        {                                            
                            Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";
                            
                            removePlayerAfterOut(onBaseList.ElementAt(i), false, true);

                            playerRemoved = true;                            
                        }
                    }

                    if (playerRemoved == false)
                    {
                        
                        priority.baseNumber = onBaseList.ElementAt(0).BaseLocation;

                        Lists.playDescription += "Out! (" + fielder.Position + " to " + priority.name + ")\n\n";

                        removePlayerAfterOut(onBaseList.ElementAt(0), false, true);
                    }

                    playerRemoved = false;
                }
            } else if (outOrSafe.InsertValue == "safe") {
                frmBaseballSimulator.addToLblTest(" safe");
                if (onBaseList.Count > 0)
                {
                    setRunnersNewBase();
                }
                numToAdvance += 1;
                playersRunning = false;
                
                Lists.playDescription = "";
                addHitPlayDescription();
                Lists.playDescription = "Safe! (" + fielder.Position + ")";                

                frmBaseballSimulator.startAdvanceHalfTimer(numToAdvance, false, 100);
            }


            
        }

        private static void addHitPlayDescription ()
        {
            switch (numToAdvance)
            {
                case 1:
                    Lists.playDescription = "";
                    Lists.playDescription = "Single!\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    break;
                case 2:
                    Lists.playDescription = "";
                    Lists.playDescription = "Double!\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    break;
                case 3:
                    Lists.playDescription = "";
                    Lists.playDescription = "Triple!\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    break;
                case 4:
                    Lists.playDescription = "";
                    Lists.playDescription = "Home Run!\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    break;

            }
        }
        public void addBasePriorityToPlayDescription()
        {
            if (determineBasePriority() != null)
            {
                Lists.playDescription += determineBasePriority().name;
                addBasePriorityToPlayDescription();
            }
            else
            {
                Lists.playDescription += "The play ended.";
            }            
        }

        public static void noBasesForced ()
        {
            for (int i = 0; i < Bases.Count; i++)
            {
                Bases.ElementAt(i).isForced = false;
            }

            maxForcedBase = null;
        }

        public static void determineLoadedBases()
        {
            foreach (Base base1 in Bases)
            {
                base1.isLoaded = false;     
            }

            for (int i = 0; i < onBaseList.Count; i++)
            {
                foreach (Base base1 in Bases)
                {
                    if (onBaseList.ElementAt(i).BaseLocation == base1.baseNumber)
                    {
                        base1.isLoaded = true;
                    }
                }                
            }
        }            

        public static void determineForcedBases(Base newBase)
        {
             if (newBase.isLoaded)
            {
                baseNumberForced += 1;
                determineForcedBases(Bases.ElementAt(baseNumberForced));          
            }
            else
            {                             
                for (int i = baseNumberForced; i >= 1; i--)
                {
                    Bases.ElementAt(i).isForced = true;
                }
                
                maxForcedBase = Bases.ElementAt(baseNumberForced);

                baseNumberForced = 0;
            }         
        }
        
        private static void advanceRunners()
        {                      
            foreach(Player player in onBaseList)
            {
                advanceRunner(player);
            }

            //=========       T H I S  C O D E  I S  F O R  T E S T I N G  O N L Y         ============//

            //Lists.playDescription = "";
            //Lists.playDescription = "Runner's Advance! Players Running: " + playersRunning.ToString();
            //frmBaseballSimulator.addPlayDescription();


            //========================================================================================//
        }

        private static void advanceRunner(Player player)
        {    
            //player.BaseLocation becomes negative to indicate that he is running
            if(player.BaseLocation < 4 && player.BaseLocation >= 0)
            {
                Bases.ElementAt(player.BaseLocation).isLoaded = false;
                Bases.ElementAt(player.BaseLocation + 1).isApproached = true;
                player.BaseLocation = -(player.BaseLocation + 1);                
            }
            
                          
        }

        private static void setRunnerNewBase(Player player)
        {
            if(player.BaseLocation < 0)
            {
                player.BaseLocation = -player.BaseLocation;
                if (player.BaseLocation != 4)
                {
                    Bases.ElementAt(player.BaseLocation).isLoaded = true;
                }
                
                

                if (player.Equals(currentBatter))
                {
                    currentBatter = null;

                }



            }
        }

        private static void setRunnerOldBase(Player player)
        {
            if (player.BaseLocation < 0 && player != currentBatter)
            {
                player.BaseLocation = -(player.BaseLocation + 1);
                Bases.ElementAt(player.BaseLocation).isLoaded = true;
                Bases.ElementAt(player.BaseLocation).isApproached = false;
            }
        }

        private static void stopRunner(Player player)
        {
            if (player.BaseLocation < 0)
            {
                player.BaseLocation = -player.BaseLocation - 1;
            }

            Bases.ElementAt(player.BaseLocation + 1).isApproached = false;
            Bases.ElementAt(player.BaseLocation).isLoaded = true;         
        }

        private static void setRunnersNewBase()
        {            
                for (int i = 0; i < onBaseList.Count; i++)
                {
                    if (onBaseList.ElementAt(i).BaseLocation < 0)
                    {
                        if (onBaseList.ElementAt(i).BaseLocation == -4)
                        {
                            removePlayerAfterScore();
                        }

                        if (onBaseList.Count > 0)
                        {

                            onBaseList.ElementAt(i).BaseLocation = -onBaseList.ElementAt(i).BaseLocation;

                            if (onBaseList.ElementAt(i).BaseLocation != 4)
                            {
                                Bases.ElementAt(onBaseList.ElementAt(i).BaseLocation).isLoaded = true;
                            }

                            Bases.ElementAt(onBaseList.ElementAt(i).BaseLocation).isApproached = false;
                    }                  

                    if (onBaseList.Count > 0)
                    {
                        if (onBaseList.ElementAt(i) == currentBatter)
                        {
                            currentBatter = null;
                        }

                    }
                }
                    
                }           

                noBasesForced();                       
        }

        private static ProportionValue<string> allChooseToRun()
        {
            ProportionValue<string> runOrNotRun = runnerDecision.ChooseByRandom();

            if(runOrNotRun == run)
            {
                return run;
            }
            else
            {
                return notRun;
            }
        }

        private static ProportionValue<string> checkIfFielded(double odds)
        {
            setHasItOrNotOdds(odds);

            ProportionValue<string> hasIt = ProportionValue.Create(hasItOdds, null, "The fielder fielded the ball ", "hasIt");
            ProportionValue<string> doesntHaveIt = ProportionValue.Create(doesntHaveItOdds, null, "The fielder doesn't have the ball yet", "notYet");

            ProportionValue<string>[] hasItOrNot = new[] {
                hasIt,
                doesntHaveIt
            };

            checkIt = hasItOrNot.ChooseByRandom();                     

            if(checkIt.InsertValue == "hasIt")
            {
                return hasIt;
            }

            if(checkIt.InsertValue == "notYet")
            {            
                return doesntHaveIt;
            }

            return null;
        }

        private static void runnersMadeIt()
        {
            onBaseList.Add(currentBatter);
            currentBatter.BaseLocation = 0;
            Bases.ElementAt(0).isApproached = false;
            Bases.ElementAt(0).isLoaded = true;

            foreach (Player player in onBaseList)
            {
                if(player.BaseLocation < 0)
                {
                    player.BaseLocation = -player.BaseLocation;
                    Bases.ElementAt(player.BaseLocation).isApproached = false;
                    Bases.ElementAt(player.BaseLocation).isLoaded = true;
                }
            }
        }

        private static void ballIsCaught()
        {
            removePlayerAfterOut(currentBatter);
            foreach(Player player in onBaseList)
            {
                if(player.BaseLocation < 0)
                {
                    Bases.ElementAt(-player.BaseLocation).isApproached = false;
                    player.BaseLocation = -(player.BaseLocation + 1);
                    Bases.ElementAt(player.BaseLocation).isLoaded = true;                    
                }
            }
        }

        private static ProportionValue<string> chooseInfieldOrOutfield()
        {
            ProportionValue<string> infieldOutfield = infieldOrOutfield.ChooseByRandom();
            return infieldOutfield;
        }

        //private static void chooseBallTrajectory()
        //{
        //    ProportionValue<string> trajectory = trajectoryList.ChooseByRandom();

        //    string playAddition = trajectory.Value;

        //    Lists.playDescription += playAddition;
        //}

        private static void unloadAllBases()
        {
            foreach (Base base1 in Bases)
            {
                base1.isLoaded = false;
            }
        }

        private static void setHasItOrNotOdds(double odds)
        {
            hasItOdds = odds;
            doesntHaveItOdds = 1 - odds;
        }

        private static void runInfieldProgression(Fielder fielder)
        {
            frmBaseballSimulator.updateLblTest("infield");
            advanceRunners();

            ProportionValue<string> checkIT = checkIfFielded(1);

            if(checkIT.InsertValue == "hasIt")
            {
                frmBaseballSimulator.addToLblTest(" has it");
                infieldChallengeRunner(fielder);
                
            }
            else if (checkIT.InsertValue == "notYet")
            {
                frmBaseballSimulator.addToLblTest(" not yet");
                if (onBaseList.Count > 0)
                {
                    setRunnersNewBase();
                }

                Lists.playDescription = "";
                Lists.playDescription = "Error!";
                frmBaseballSimulator.addPlayDescription();

                frmBaseballSimulator.startAdvanceHalfTimer(1, true, 100);
                
            }
        }

        private static void runOutfieldProgression(Fielder fielder)
        {

            frmBaseballSimulator.updateLblTest("outfield");
            ProportionValue<string> checkIT = checkIfFielded(hasItOdds);

            //determine if players are running
            foreach (Player element in onBaseList)
            {
                if (element.BaseLocation < 0)
                {
                    playersRunning = true;
                    break;
                } else
                {
                    playersRunning = false;
                }
            }            
            
            //this is for a home run
            if (numToAdvance == 4)
            {
                frmBaseballSimulator.addToLblTest(" home run");
                frmBaseballSimulator.startAdvanceHalfTimer(numToAdvance, false, 100);
            }
            else if (playersRunning) {
                frmBaseballSimulator.addToLblTest(" Players Running");

                //fielder doesn't have it
                if (checkIT.InsertValue == "notYet")
                {
                    frmBaseballSimulator.addToLblTest(" not yet");
                    numToAdvance += 1;
                    if(onBaseList.Count > 0)
                    {
                        setRunnersNewBase();
                    }
                    
                    
                    hasItOdds = .5;
                    playersRunning = false;

                    runOutfieldProgression(fielder);
                }

                //player fields it
                if (checkIT.InsertValue == "hasIt")
                {
                    frmBaseballSimulator.addToLblTest(" has it");
                    outFieldChallengeRunner(fielder);
                }
            }
            else if (!playersRunning) {
                frmBaseballSimulator.addToLblTest(" Players Running");
                if (checkIT.InsertValue == "notYet") {
                    frmBaseballSimulator.addToLblTest(" not yet");

                    hasItOdds += .1;                    
                    advanceRunners();                    
                    runOutfieldProgression(fielder);
                                        
                }
                else if (checkIT.InsertValue == "hasIt") {
                    frmBaseballSimulator.addToLblTest(" has it");
                    addHitPlayDescription();
                    frmBaseballSimulator.startAdvanceHalfTimer(numToAdvance, false, 100);
                }
            }    

        }

        public static void runPlay()
        {
               

            ProportionValue<string> fieldedBy = infieldOrOutfield.ChooseByRandom();
            ProportionValue<string> caughtOrNot = caughtOrNotCaught.ChooseByRandom();

            chooseInfielder(infieldList.ChooseByRandom());
            
            ChooseOutFielder(outfieldList.ChooseByRandom());
            

            if(caughtOrNot.InsertValue == "caught")
            {
                advanceRunners();
                //determine the player who caught the ball
                if (fieldedBy.InsertValue == "outfield")
                {
                    Lists.playDescription = "";
                    Lists.playDescription += "Caught by " + outFielder.Position + "\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    removePlayerAfterOut(currentBatter);  
                }

                if (fieldedBy.InsertValue == "infield")
                {
                    Lists.playDescription = "";
                    Lists.playDescription += "Caught by " + inFielder.Position + "\n\n";
                    frmBaseballSimulator.addPlayDescription();
                    removePlayerAfterOut(currentBatter);              
                }                               
                //return players to their original bases
            }

            if (caughtOrNot.InsertValue == "notCaught")
            {
                

                if (fieldedBy.InsertValue == "infield")
                {
                    
                    runInfieldProgression(inFielder);
                    
                }
                else if (fieldedBy.InsertValue == "outfield")
                {
                    advanceRunners();
                    
                    runOutfieldProgression(outFielder);
                }      
                
                
                
            }

            
        }

        public static void removeCurrentBatter()
        {
            removePlayerAfterOut(currentBatter);
        }

        private static void chooseInfielder(ProportionValue<string> fielderProp)
        {           
            if (fielderProp.Value == "first baseman ")
            {
                inFielder = firstBaseman;
            }
            else if (fielderProp.Value == "second baseman ")
            {
                inFielder = secondBaseman;
            }
            else if (fielderProp.Value == "third baseman ")
            {
                inFielder = thirdBaseman;
            }
            else if (fielderProp.Value == "catcher ")
            {
                 inFielder = catcher;
            }
            else if (fielderProp.Value == "short stop ")
            {
                inFielder = shortStop;
            }
            else if (fielderProp.Value == "pitcher ")
            {
                inFielder = pitcher;
            }        
        }

        private static void ChooseOutFielder(ProportionValue<string> fielderProp)
        {           

            if (fielderProp.Value == "center fielder ")
            {
                outFielder = centerFielder;
            }
            else if (fielderProp.Value == "left fielder ")
            {
                outFielder = leftFielder;
            }
            else if (fielderProp.Value == "right fielder ")
            {
                outFielder = rightFielder;
            }           

            
        }

        public static void newInning()
        {
            ScoreBoard.newInning();
            
            frmBaseballSimulator.newInning();
        }

        public static void endInning()
        {
            foreach (Player player in onBaseList)
            {
                dugOutQueue.Enqueue(player);
               
            }

            onBaseList.Clear();

            foreach (Base newBase in Bases)
            {
                newBase.isLoaded = false;
            }

            ScoreBoard.newInning();
            frmBaseballSimulator.endInning();            
        }

        public static void startPlay()
        {
            Lists.playDescription = "--------------------------------------------------------------------------------------\n\n";
            frmBaseballSimulator.addPlayDescription();
            Lists.playDescription = "";

            if(currentBatter == null)
            {
                setCurrentBatter();
            }

            //=========       T H I S  C O D E  I S  F O R  T E S T I N G  O N L Y         ============//

            //Lists.playDescription = "";
            //Lists.playDescription += "OBC: " + onBaseList.Count.ToString() + "\n\n";
            //frmBaseballSimulator.addPlayDescription();

            //========================================================================================//

            noBasesForced();
            determineForcedBases(Bases.ElementAt(0));

            Lists.DoWorkSon(Lists.firstPropValue);
        }
        
        public static void recordStrike()
        {
            ScoreBoard.recordStrike();
            if (ScoreBoard.Strikes == 3)
            {
                frmBaseballSimulator.recordStrike(false, true);
            } else
            {
                frmBaseballSimulator.recordStrike(false, false);
            }
                       
        }

        public static void recordFoul()
        {
            if(ScoreBoard.Strikes < 2)
            {
                ScoreBoard.Strikes += 1;
                frmBaseballSimulator.recordStrike(true, false);
            } else
            {
                frmBaseballSimulator.recordStrike(true, false);
            }
        }

        public static void recordBall()
        {
            ScoreBoard.recordBall();

            if(ScoreBoard.Balls == 4)
            {
                frmBaseballSimulator.recordBall(true);
            } else {
                frmBaseballSimulator.recordBall(false);
            }
            
        }

        private static void recordOut(Player player)
        {
            removePlayerAfterOut(player);
        }

        public static void endPlay()
        {
            Lists.playDescription = "";
            numToAdvance = 0;
            frmBaseballSimulator.PlayTimer.Stop();            

            if (ScoreBoard.Strikes == 3)
            {               
                removePlayerAfterOut(currentBatter);
                
            }
            else if (ScoreBoard.Balls == 4)
            {
                walkCurrentBatter();
            }
            else if (ScoreBoard.Outs == 3)
            {
                endInning();
            }
            else
            {
                frmBaseballSimulator.PlayTimer.Start();                
            }

            //=========       T H I S  C O D E  I S  F O R  T E S T I N G  O N L Y         ============//

            //Lists.playDescription = "";
            //Lists.playDescription += "OBC: " + onBaseList.Count.ToString() + "\n\n"; ;
            //frmBaseballSimulator.addPlayDescription();

            //========================================================================================//
            
        }

        public static void walkCurrentBatter()
        {
            determineLoadedBases();
            determineForcedBases(Bases.ElementAt(0));

            frmBaseballSimulator.addRunnerToWalkList(0);

            Bases.ElementAt(3).isForced = true;

            foreach (Base base1 in Bases)
            {
                if (base1.isForced)
                {
                    foreach (Player player in onBaseList)
                    {
                        if (player.BaseLocation == base1.baseNumber)
                        {
                            advanceRunner(player);
                            setRunnerNewBase(player);                            
                            frmBaseballSimulator.addRunnerToWalkList(base1.baseNumber);
                        }
                    }
                }
            }

            advanceRunner(currentBatter);
            setRunnerNewBase(currentBatter);

            frmBaseballSimulator.PlayTimer.Stop();
            frmBaseballSimulator.startWalkBattersTimer();
        }

        public static void testWalkBases()
        {
            LoadNewDugout();
            LoadBases();

            setCurrentBatter();

            currentBatter.BaseLocation = 3;

            setCurrentBatter();
            currentBatter.BaseLocation = 2;

            setCurrentBatter();
            currentBatter.BaseLocation = 1;

            setCurrentBatter();

            walkCurrentBatter();
        }


    }
}
    

