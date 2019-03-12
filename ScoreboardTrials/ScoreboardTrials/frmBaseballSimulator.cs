using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
 

namespace ScoreboardTrials
{
    public partial class frmBaseballSimulator : Form
    {
        public static Timer PlayTimer = new Timer();
        public static Timer NewInningTimer = new Timer();
        public static Timer walkBattersTimer = new Timer();
        public static Timer runPlayTimer = new Timer();
        public static Timer advanceHalfTimer = new Timer();

        private static List<RunnerLabel> setRunnersList = new List<RunnerLabel>();

        public static int intRunnersCount = 0;
        public static int basesCrossed = 0;
        private static int NumToAdvance;
        //private static int startingBaseLocation;
        private static int onBaseCount;
        private static bool boolAdvanceHalf;
        private static int playerOutBaseLocation;
        private static int totalNumToAdvance = 0;

        private int index = ScoreBoard.Innings;
        private int visitorSum = 0;
        private int homeSum = 0;
        //private static int intMoveOneBase;

        private static int counter = 0;

        private bool GameStart = true;

        private static ListBox lstPlayDescription = new ListBox();
        private static TextBox txtPlayDescription = new TextBox();
        private static RichTextBox rtxtPlayDescriptions = new RichTextBox();

        private static List<Control> VisitorControls = new List<Control>();
        private static List<Control> HomeControls = new List<Control>();

        private static List<RunnerLabel> labelDugout = new List<RunnerLabel>();
        private static List<RunnerLabel> onBaseLabels = new List<RunnerLabel>();
        private static List<RunnerLabel> walkRunnersList = new List<RunnerLabel>();

        private static RunnerLabel nextLabelInLine;

        private static RunnerLabel labl1 = new RunnerLabel();
        private static RunnerLabel labl2 = new RunnerLabel();
        private static RunnerLabel labl3 = new RunnerLabel();
        private static RunnerLabel labl4 = new RunnerLabel();
        private static RunnerLabel labl5 = new RunnerLabel();

        private static Label lblVstr1 = new Label();
        private static Label lblVstr2 = new Label();
        private static Label lblVstr3 = new Label();
        private static Label lblVstr4 = new Label();
        private static Label lblVstr5 = new Label();
        private static Label lblVstr6 = new Label();
        private static Label lblVstr7 = new Label();
        private static Label lblVstr8 = new Label();
        private static Label lblVstr9 = new Label();
        private static Label lblVstrRuns = new Label();

        private static Label lblHome1 = new Label();
        private static Label lblHome2 = new Label();
        private static Label lblHome3 = new Label();
        private static Label lblHome4 = new Label();
        private static Label lblHome5 = new Label();
        private static Label lblHome6 = new Label();
        private static Label lblHome7 = new Label();
        private static Label lblHome8 = new Label();
        private static Label lblHome9 = new Label();
        private static Label lblHomeRuns = new Label();

        private static Label lblStrks = new Label();
        private static Label lblBlls = new Label();        
        private static Label lblOts = new Label();

        private static Label lblTst = new Label();
        public frmBaseballSimulator()
        {
            InitializeComponent();            
        }
        
        public List<Control> VisitorScores()
        {
            VisitorControls = new List<Control>();

            lblVstr1 = lblV1;
            lblVstr2 = lblV2;
            lblVstr3 = lblV3;
            lblVstr4 = lblV4;
            lblVstr5 = lblV5;
            lblVstr6 = lblV6;
            lblVstr7 = lblV7;
            lblVstr8 = lblV8;
            lblVstr9 = lblV9;
            lblVstrRuns = lblVRuns;

            VisitorControls.Add(lblVstr1);
            VisitorControls.Add(lblVstr2);
            VisitorControls.Add(lblVstr3);
            VisitorControls.Add(lblVstr4);
            VisitorControls.Add(lblVstr5);
            VisitorControls.Add(lblVstr6);
            VisitorControls.Add(lblVstr7);
            VisitorControls.Add(lblVstr8);
            VisitorControls.Add(lblVstr9);
            VisitorControls.Add(lblVstrRuns);

            return VisitorControls;
        }

        public List<Control> HomeScores()
        {

            HomeControls = new List<Control>();

            lblHome1 = lblH1;
            lblHome2 = lblH2;
            lblHome3 = lblH3;
            lblHome4 = lblH4;
            lblHome5 = lblH5;
            lblHome6 = lblH6;
            lblHome7 = lblH7;
            lblHome8 = lblH8;
            lblHome9 = lblH9;
            lblHomeRuns = lblHRuns;            

            HomeControls.Add(lblHome1);
            HomeControls.Add(lblHome2);
            HomeControls.Add(lblHome3);
            HomeControls.Add(lblHome4);
            HomeControls.Add(lblHome5);
            HomeControls.Add(lblHome6);
            HomeControls.Add(lblHome7);
            HomeControls.Add(lblHome8);
            HomeControls.Add(lblHome9);
            HomeControls.Add(lblHomeRuns);

            return HomeControls;
        }

        public void PopulateScoreboard ()
        {
            //reset the visitor and home scores. These scores are displayed as the total runs scored on the scoreboard.
            visitorSum = 0; 
            homeSum = 0;

            lblStrikes.Text = ScoreBoard.Strikes.ToString();
            lblBalls.Text = ScoreBoard.Balls.ToString();
            lblOuts.Text = ScoreBoard.Outs.ToString();

            if (ScoreBoard.Innings % 2 == 0 && ScoreBoard.Innings < 17)
            {
                VisitorControls.ElementAt(ScoreBoard.Innings / 2).Text = ScoreBoard.InningScore.ToString();
            }

            else if (ScoreBoard.Innings % 2 != 0 && ScoreBoard.Innings < 18)
            {
                HomeControls.ElementAt((ScoreBoard.Innings - 1) / 2).Text = ScoreBoard.InningScore.ToString();
            }            

            for (int i = 0; i < 9; i++)
            {
                if (VisitorControls.ElementAt(i).Text != string.Empty)
                {
                    int currentControl = int.Parse(VisitorControls.ElementAt(i).Text);
                    visitorSum += currentControl;
                }
            }

            VisitorControls.ElementAt(9).Text = visitorSum.ToString();

            for (int i = 0; i < 9; i++)
            {
                if (HomeScores().ElementAt(i).Text != string.Empty)
                {
                    int currentControl = int.Parse(HomeScores().ElementAt(i).Text);
                   homeSum += currentControl;
                }
            }

            HomeControls.ElementAt(9).Text = homeSum.ToString();
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            Play.resetAllObjects();
            Lists.playDescription = "";
            ScoreBoard.resetScoreboard();
            resetAllObjects();
            btnReset.Enabled = false;
        }

        public static void resetAllObjects ()
        {
            ResetScoreboard();
            onBaseLabels.Clear();
            HomeControls.Clear();
            VisitorControls.Clear();
        }

        public static void ResetScoreboard()
        {
          if (ScoreBoard.Outs == 3)
          {
              ScoreBoard.Outs = 0;
              ScoreBoard.Strikes = 0;
              ScoreBoard.Balls = 0;
          }

          if (ScoreBoard.inningsChanged == true)
          {
              ScoreBoard.InningScore = 0;
              ScoreBoard.Strikes = 0;
              ScoreBoard.Balls = 0;
              ScoreBoard.Outs = 0;
              ScoreBoard.inningsChanged = false;
              ScoreBoard.runnerIsOut = false;
          }

          if (ScoreBoard.runnerIsOut == true)
          {
              ScoreBoard.Strikes = 0;
              ScoreBoard.Balls = 0;
              ScoreBoard.runnerIsOut = false;
          }

          if (ScoreBoard.runScored == true)
          {
              ScoreBoard.Strikes = 0;
              ScoreBoard.Balls = 0;
              ScoreBoard.runScored = false;
          }          
        }

        private void UpdateScoreboard()
        {
            lblStrikes.Text = ScoreBoard.Strikes.ToString();
            lblBalls.Text = ScoreBoard.Balls.ToString();
            lblOuts.Text = ScoreBoard.Outs.ToString();
        }

        private void recordRunScored()
        {
            if (ScoreBoard.Innings % 2 == 0 && ScoreBoard.Innings < 17)
            {
                //VisitorScores().ElementAt(ScoreBoard.Innings / 2).Text = ScoreBoard.InningScore.ToString();
                VisitorControls.ElementAt(ScoreBoard.Innings / 2).Text = (int.Parse(VisitorControls.ElementAt(ScoreBoard.Innings / 2).Text) + 1).ToString(); 
            }

            else if (ScoreBoard.Innings % 2 != 0 && ScoreBoard.Innings < 18)
            {
                //HomeScores().ElementAt((ScoreBoard.Innings - 1) / 2).Text = ScoreBoard.InningScore.ToString();
                HomeControls.ElementAt((ScoreBoard.Innings - 1) / 2).Text = ScoreBoard.InningScore.ToString();
            }
        }

        public static void changeInningScoreToZero()
        {
            if (ScoreBoard.Innings % 2 == 0 && ScoreBoard.Innings < 17)
            {
                //VisitorScores().ElementAt(ScoreBoard.Innings / 2).Text = ScoreBoard.InningScore.ToString();
                VisitorControls.ElementAt(ScoreBoard.Innings / 2).Text = "0";
            }

            else if (ScoreBoard.Innings % 2 != 0 && ScoreBoard.Innings < 18)
            {
                //HomeScores().ElementAt((ScoreBoard.Innings - 1) / 2).Text = ScoreBoard.InningScore.ToString();
                HomeControls.ElementAt((ScoreBoard.Innings - 1) / 2).Text = "0";
            }
        }

        public void updateScoreboard()
        {
            UpdateScoreboard();
        }

        public void NewInning()
        {
            PlayTimer.Interval = 1000;
            PlayTimer.Tick += new EventHandler(PlayTimer_Tick);
            PlayTimer.Start();
        }

        public static void newInning()
        {
            NewInningTimer.Interval = 1000;
            NewInningTimer.Tick += new EventHandler(NewInningTimer_Tick);
            NewInningTimer.Start();
        }

        public static void stopAllTimers ()
        {
            runPlayTimer.Stop();

        }

        public static void endInning()
        {
            returnLabelsAfterInning();

            //This code below will finish the game and give the user a chance to 
            //start a new game. It is not complete. Medium risk for deletion
            //if (ScoreBoard.Innings == 17)
            //{
            //    if (int.Parse(lblHomeRuns.Text) < int.Parse(lblVstrRuns.Text))
            //    {
            //        MessageBox.Show("Game Over! Visiting Team Wins!");
            //    } else
            //    {
            //        PlayTimer.Stop();
            //        startNewInningTimer();
            //    }
            //} else if (ScoreBoard.Innings == 18)
            //{

            //}
            //ScoreBoard.newInning();
            PlayTimer.Stop();
            startNewInningTimer();
        }

        public static void newCount()
        {
            lblStrks.Text = "0";
            lblBlls.Text = "0";
        }

        public static void recordStrike(bool foul, bool strikeout)
        {
            if(foul == true)
            {
                Lists.ResetPlayDescription();
                Lists.playDescription += "Foul! \n\n";
                addPlayDescription();
                if (ScoreBoard.Strikes <= 2 && int.Parse(lblStrks.Text) < 2)
                {
                    addOneToLabel(lblStrks);
                }
            } else if (strikeout)
            {
                Lists.ResetPlayDescription();
                Lists.playDescription += "Strikeout! \n\n";
                addPlayDescription();
                addOneToLabel(lblStrks);
            } else
            {
                Lists.ResetPlayDescription();
                Lists.playDescription += "Strike! \n\n";
                addPlayDescription();
                addOneToLabel(lblStrks);
            }
            
            totalNumToAdvance = 0;
            basesCrossed = 0;
            Play.endPlay();
        }

        public static void recordBall(bool walk)
        {
            if (walk) {
                Lists.ResetPlayDescription();
                Lists.playDescription += "Walk! \n\n";
                addPlayDescription();
            } else
            {
                Lists.ResetPlayDescription();
                Lists.playDescription += "Ball! \n\n";
                addPlayDescription();
            }
            
            addOneToLabel(lblBlls);
            totalNumToAdvance = 0;
            basesCrossed = 0;
            Play.endPlay();           
        }

        public static void recordOut(int playerBaseLocation)
        {
            for(int i = 0; i < onBaseLabels.Count; i++)
            {
                  if (onBaseLabels.ElementAt(i).BaseLocation == playerBaseLocation)
                  {                        
                       addOneToLabel(lblOts);
                       addLabelToDugout(onBaseLabels.ElementAt(i));
                  }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Play.LoadAllObjects();
            loadAllObjects();
            btnStart.Enabled = false;
            btnReset.Enabled = false;
            startNewInningTimer(); 
        }

        private void loadLabelDugout()
        {
            this.Controls.Add(labl1);
            this.Controls.Add(labl2);
            this.Controls.Add(labl3);
            this.Controls.Add(labl4);
            
            if (labelDugout.Count == 0)
            {
                labelDugout.Add(labl1);
                labelDugout.Add(labl2);
                labelDugout.Add(labl3);
                labelDugout.Add(labl4);
            }

            foreach(RunnerLabel label in labelDugout)
            {
                formatRunnerLabels(label);
            }

            foreach (RunnerLabel label in labelDugout)
            {
                label.Visible = false;
                label.Top = 306;
                label.Left = 514;
                label.BaseLocation = 10;
            }     
        } 

        private void createScoreboardLabels()
        {
            this.Controls.Add(lblStrks);
            this.Controls.Add(lblBlls);
            this.Controls.Add(lblOts);

            lblStrks = lblStrikes;
            lblBlls = lblBalls;
            lblOts = lblOuts;       

            this.Controls.Add(lblVstr1);
            this.Controls.Add(lblVstr2);
            this.Controls.Add(lblVstr3);
            this.Controls.Add(lblVstr4);
            this.Controls.Add(lblVstr5);
            this.Controls.Add(lblVstr6);
            this.Controls.Add(lblVstr7);
            this.Controls.Add(lblVstr8);
            this.Controls.Add(lblVstr9);
            this.Controls.Add(lblVstrRuns);

            lblVstr1 = lblV1;
            lblVstr2 = lblV2;
            lblVstr3 = lblV3;
            lblVstr4 = lblV4;
            lblVstr5 = lblV5;
            lblVstr6 = lblV6;
            lblVstr7 = lblV7;
            lblVstr8 = lblV8;
            lblVstr9 = lblV9;
            lblVstrRuns = lblVRuns;

            VisitorControls.Add(lblVstr1);
            VisitorControls.Add(lblVstr2);
            VisitorControls.Add(lblVstr3);
            VisitorControls.Add(lblVstr4);
            VisitorControls.Add(lblVstr5);
            VisitorControls.Add(lblVstr6);
            VisitorControls.Add(lblVstr7);
            VisitorControls.Add(lblVstr8);
            VisitorControls.Add(lblVstr9);
            VisitorControls.Add(lblVstrRuns);

            this.Controls.Add(lblHome1);
            this.Controls.Add(lblHome2);
            this.Controls.Add(lblHome3);
            this.Controls.Add(lblHome4);
            this.Controls.Add(lblHome5);
            this.Controls.Add(lblHome6);
            this.Controls.Add(lblHome7);
            this.Controls.Add(lblHome8);
            this.Controls.Add(lblHome9);
            this.Controls.Add(lblHomeRuns);

            lblHome1 = lblH1;
            lblHome2 = lblH2;
            lblHome3 = lblH3;
            lblHome4 = lblH4;
            lblHome5 = lblH5;
            lblHome6 = lblH6;
            lblHome7 = lblH7;
            lblHome8 = lblH8;
            lblHome9 = lblH9;
            lblHomeRuns = lblHRuns;

            HomeControls.Add(lblHome1);
            HomeControls.Add(lblHome2);
            HomeControls.Add(lblHome3);
            HomeControls.Add(lblHome4);
            HomeControls.Add(lblHome5);
            HomeControls.Add(lblHome6);
            HomeControls.Add(lblHome7);
            HomeControls.Add(lblHome8);
            HomeControls.Add(lblHome9);
            HomeControls.Add(lblHomeRuns);
        }

        private void loadTimers()
        {
            frmBaseballSimulator.walkBattersTimer.Interval = 10;
            frmBaseballSimulator.walkBattersTimer.Tick += new EventHandler(walkBattersTimer_Tick);
            
            frmBaseballSimulator.runPlayTimer.Interval = 1000;
            frmBaseballSimulator.runPlayTimer.Tick += new EventHandler(runPlayTimer_Tick);

            frmBaseballSimulator.advanceHalfTimer.Interval = 10;
            frmBaseballSimulator.advanceHalfTimer.Tick += new EventHandler(advanceHalfTimer_Tick);

            frmBaseballSimulator.NewInningTimer.Interval = 1000;
            frmBaseballSimulator.NewInningTimer.Tick += new EventHandler(NewInningTimer_Tick);

            frmBaseballSimulator.PlayTimer.Interval = 1000;
            frmBaseballSimulator.PlayTimer.Tick += new EventHandler(PlayTimer_Tick);
        }     

        private void loadTextBox()
        {
            rtxtPlayDescriptions.Visible = false;
            this.Controls.Add(rtxtPlayDescriptions);
            rtxtPlayDescriptions = playDescTextBox;
        }

        private void loadAllObjects()
        {
            loadLabelDugout();      
            createScoreboardLabels();
            loadTimers();
            loadTextBox();
        }        

        public static void clearCount()
        {
            lblBlls.Text = "0";
            lblStrks.Text = "0";
        }

        private static void formatRunnerLabels(RunnerLabel lbl)
        {
            lbl.BringToFront();
            lbl.BackColor = Color.Blue;
            lbl.Height = 16;
            lbl.Width = 17;
            lbl.AutoSize = false;
            lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void createRunnnerLabels()
        {
            this.Controls.Add(labl1);
            this.Controls.Add(labl2);
            this.Controls.Add(labl3);
            this.Controls.Add(labl4);
        }

        public static void addLabelToBases()
        {
            nextLabelInLine = labelDugout.ElementAt(0);
            nextLabelInLine.Visible = true;
            onBaseLabels.Add(nextLabelInLine);
            labelDugout.Remove(labelDugout.ElementAt(0));
            nextLabelInLine.BaseLocation = 0;                
        }

        public static void returnLabelsAfterInning()
        {
            foreach (RunnerLabel lbl in onBaseLabels)
            {
                if (onBaseLabels.Contains(lbl))
                {
                    lbl.Visible = false;
                    lbl.Top = 306;
                    lbl.Left = 514;
                    lbl.BaseLocation = 10;
                    labelDugout.Add(lbl);                   
                }
            }
            onBaseLabels.Clear();
        }

        private static void addLabelToDugout(RunnerLabel lbl)
        {
            if (onBaseLabels.Contains(lbl))
            {
                lbl.Visible = false;
                lbl.Top = 306;
                lbl.Left = 514;
                lbl.BaseLocation = 10;
                labelDugout.Add(lbl);
                onBaseLabels.Remove(lbl);
            }
        }

        public static void updateLblTest(string outOrIn)
        {
            lblTst.Text = outOrIn;
        }

        public static void addToLblTest (string methodName)
        {
            lblTst.Text += methodName;
        }
        
        public static void setCurrentBatter()
        {
            addLabelToBases();
        }

        private static void resetRunnerLabels()
        {
            for(int i = 0; i < onBaseLabels.Count; i++)
            {
               addLabelToDugout(onBaseLabels.ElementAt(0));
            }                      
        }

        private static void moveToHalf(RunnerLabel lbl)
        {
            if (lbl.Top >= 273)
            {
                counter += 1;
                lbl.Top -= 1;
                lbl.Left += 1;
                                      
            }
            else if (lbl.BaseLocation != 1)
            {
                if (lbl.BaseLocation == playerOutBaseLocation)
                {
                    recordOut(lbl.BaseLocation);
                }
                else
                {
                    moveToFirst(lbl);
                }
            }

            if (lbl.BaseLocation == 1)
            {
                if (!setRunnersList.Contains(lbl))
                {
                    setRunnersList.Add(lbl);
                }

                if (setRunnersList.Count == onBaseLabels.Count)
                {
                    advanceHalfTimer.Stop();
                    clearCount();
                    setRunnersList.Clear();
                    totalNumToAdvance = 0;
                    basesCrossed = 0;
                    Play.endPlay();
                }
            }
            }

        private static void moveToFirst(RunnerLabel lbl)
        {
            if(lbl.Top >= 239)
            {
                counter += 1;
                lbl.Top -= 1;
                lbl.Left += 1;                                       
            }
            else
            {               
                lbl.BaseLocation = 1;

                
                basesCrossed += 1;

                if (walkRunnersList.Contains(lbl))
                {
                    walkRunnersList.Remove(lbl);                   
                }
            }            
        }

        private static void moveToFirstHalf(RunnerLabel lbl)
        {
            if (lbl.Left >= 556)
            {
                counter += 1;
                lbl.Top -= 1;
                lbl.Left -= 1;                      
            }
            else if (lbl.BaseLocation != 2)
            {

                if (lbl.BaseLocation == playerOutBaseLocation)
                {
                    recordOut(lbl.BaseLocation);
                }
                else
                {
                    moveToSecond(lbl);
                }
            }

            if (lbl.BaseLocation == 2)
            {
                if(!setRunnersList.Contains(lbl))
                {
                    setRunnersList.Add(lbl);
                }

                if (setRunnersList.Count == onBaseLabels.Count)
                {
                    advanceHalfTimer.Stop();
                    clearCount();
                    setRunnersList.Clear();
                    totalNumToAdvance = 0;
                    basesCrossed = 0;
                    Play.endPlay();
                }             
            }
        }

        private static void moveToSecond(RunnerLabel lbl)
        {
            if (lbl.Left > 514)
            {
                lbl.Top -= 1;
                lbl.Left -= 1;

            }
            else
            {
                

                lbl.BaseLocation = 2;
                    basesCrossed += 1;

                if (walkRunnersList.Contains(lbl))
                {
                    walkRunnersList.Remove(lbl);                    
                }                
            }
        }

        private static void moveToSecondHalf(RunnerLabel lbl)
        {
            if (lbl.Top < 205)
            {
                counter += 1;
                lbl.Top += 1;
                lbl.Left -= 1;                       
            }
            else if (lbl.BaseLocation != 3)
            {
                if (lbl.BaseLocation == playerOutBaseLocation)
                {
                    recordOut(lbl.BaseLocation);
                }
                else
                {
                    moveToThird(lbl);
                }
            }

            if (lbl.BaseLocation == 3)
            {
                if (!setRunnersList.Contains(lbl))
                {
                    setRunnersList.Add(lbl);
                }

                if (setRunnersList.Count == onBaseLabels.Count)
                {
                    advanceHalfTimer.Stop();
                    clearCount();
                    setRunnersList.Clear();
                    totalNumToAdvance = 0;
                    basesCrossed = 0;
                    Play.endPlay();
                }
            }
        }

        private static void moveToThird(RunnerLabel lbl)
        {
            if (lbl.Top < 235)
            {
                lbl.Top += 1;
                lbl.Left -= 1;                
            }
            else
            {
                lbl.BaseLocation = 3;
                basesCrossed += 1;

                if (walkRunnersList.Contains(lbl))
                {
                    walkRunnersList.Remove(lbl);                    
                }
            }
        }

        private static void moveToThirdHalf(RunnerLabel lbl)
        {
            if (lbl.Left <= 482)
            {
                counter += 1;
                lbl.Top += 1;
                lbl.Left += 1;                      
            }
            else if (lbl.BaseLocation != 10)
            {
                if (lbl.BaseLocation == playerOutBaseLocation)
                {
                    recordOut(lbl.BaseLocation);
                }
                else
                {
                    moveToHome(lbl);
                }
            }
        }

        private static void moveToHome(RunnerLabel lbl)
        {
            if (lbl.Left < 512)
            {
                lbl.Top += 1;
                lbl.Left += 1;
            }
            else
            {
                lbl.BaseLocation = 4;
                basesCrossed += 1;
                addLabelToDugout(lbl);

                if (walkRunnersList.Contains(lbl))
                {
                    walkRunnersList.Remove(lbl);                    
                }

                if (onBaseLabels.Contains(lbl))
                {
                    labelDugout.Add(lbl);
                    onBaseLabels.Remove(lbl);
                }          

            }
        }

        private void tmrMoveToFirst_Tick(object sender, EventArgs e)
        {
            moveAllBases();
        }

        private static void moveAllBases()
        {
            for (int i = 0; i < onBaseLabels.Count; i++)
            {
                //if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left >= 514 && onBaseLabels.ElementAt(i).Visible)
                if (onBaseLabels.ElementAt(i).BaseLocation == 0)
                {
                    moveToFirst(onBaseLabels.ElementAt(i));
                }
                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left > 514 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 1)
                {
                    moveToSecond(onBaseLabels.ElementAt(i));
                }

                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left <= 514 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 2)
                {
                    moveToThird(onBaseLabels.ElementAt(i));
                }

                //else if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left <= 512 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 3)
                {
                    moveToHome(onBaseLabels.ElementAt(i));
                }
            }
        }

        private static void advanceRunner(RunnerLabel runner)
        {
                //if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left >= 514 && onBaseLabels.ElementAt(i).Visible)
                if (runner.BaseLocation == 0)
                {
                    moveToFirst(runner);
                }
                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left > 514 && onBaseLabels.ElementAt(i).Visible)
                else if (runner.BaseLocation == 1)
                {
                    moveToSecond(runner);
                }

                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left <= 514 && onBaseLabels.ElementAt(i).Visible)
                else if (runner.BaseLocation == 2)
                {
                    moveToThird(runner);
                }

                //else if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left <= 512 && onBaseLabels.ElementAt(i).Visible)
                else if (runner.BaseLocation == 3)
                {
                    moveToHome(runner);
                }
        }
        private static void walkForcedRunners(RunnerLabel lbl)
        {
            //if(intMoveOneBase > 0)
            //{
            if(walkRunnersList.Contains(lbl))
            {

                for (int i = 0; i < walkRunnersList.Count; i++)
                {
                    //if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left >= 514 && onBaseLabels.ElementAt(i).Visible)
                    if (walkRunnersList.ElementAt(i).BaseLocation == 0)
                    {
                        moveToFirst(walkRunnersList.ElementAt(i));
                    }
                    //else if (walkRunnersList.ElementAt(i).Top <= 240 && walkRunnersList.ElementAt(i).Left > 514 && walkRunnersList.ElementAt(i).Visible)
                    else if (walkRunnersList.ElementAt(i).BaseLocation == 1)
                    {
                        moveToSecond(walkRunnersList.ElementAt(i));
                    }

                    //else if (walkRunnersList.ElementAt(i).Top <= 240 && walkRunnersList.ElementAt(i).Left <= 514 && walkRunnersList.ElementAt(i).Visible)
                    else if (walkRunnersList.ElementAt(i).BaseLocation == 2)
                    {
                        moveToThird(walkRunnersList.ElementAt(i));
                    }

                    //else if (walkRunnersList.ElementAt(i).Top >= 240 && walkRunnersList.ElementAt(i).Left <= 512 && walkRunnersList.ElementAt(i).Visible)
                    else if (walkRunnersList.ElementAt(i).BaseLocation == 3)
                    {
                        moveToHome(walkRunnersList.ElementAt(i));
                    }
                }
            }
            else
            {
                walkBattersTimer.Stop();
            }            
        }

        private static void advanceHalf()
        {
                for (int i = 0; i < onBaseLabels.Count; i++)
                {
                    //if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left >= 514 && onBaseLabels.ElementAt(i).Visible)
                    if (onBaseLabels.ElementAt(i).BaseLocation == 0)
                    {
                        moveToHalf(onBaseLabels.ElementAt(i));
                    }
                    //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left > 514 && onBaseLabels.ElementAt(i).Visible)
                    else if (onBaseLabels.ElementAt(i).BaseLocation == 1)
                    {
                        moveToFirstHalf(onBaseLabels.ElementAt(i));
                    }

                    //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left <= 514 && onBaseLabels.ElementAt(i).Visible)
                    else if (onBaseLabels.ElementAt(i).BaseLocation == 2)
                    {
                        moveToSecondHalf(onBaseLabels.ElementAt(i));
                    }

                    //else if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left <= 512 && onBaseLabels.ElementAt(i).Visible)
                    else if (onBaseLabels.ElementAt(i).BaseLocation == 3)
                    {
                        moveToThirdHalf(onBaseLabels.ElementAt(i));
                    }
                }
        }

        private void tmrNewInning_Tick(object sender, EventArgs e)
        {
            frmBaseballSimulator.newInning();
            ScoreBoard.newInning();
            tmrNewInning.Stop();
            tmrPlay.Start();
        }

        private void tmrPlay_Tick(object sender, EventArgs e)
        {
            if (GameStart)
            {
                createRunnnerLabels();
                createScoreboardLabels();
                Play.LoadNewDugout();
                Play.newInning();
                GameStart = false;
            }

            //Play.startPlay();    
            PlayTimer.Start();        
        }

        public static void PlayTimer_Tick(object sender, EventArgs e)
        {
            //if (frmBaseballSimulator.GameStart)
            //{
            //    createRunnnerLabels();
            //    createScoreboardLabels();
            //    Play.LoadNewDugout();
            //    Play.newInning();
            //    GameStart = false;
            //}

            Play.startPlay();        
        }        

        public static void NewInningTimer_Tick(object sender, EventArgs e)
        {
            //newInning();
            //ScoreBoard.newInning();
            lblOts.Text = "0";
            lblStrks.Text = "0";
            lblBlls.Text = "0";

            changeInningScoreToZero();

            changeRunnerLabelColor();

            Lists.playDescription = "";
            Lists.playDescription = "New Inning\n===============================================\n\n";
            addPlayDescription();
            Lists.playDescription = "";

            NewInningTimer.Stop();

            PlayTimer.Start();         
        }

        public static void changeRunnerLabelColor()
        {
            foreach (RunnerLabel lbl in labelDugout)
            {
                if (lbl.BackColor == Color.Blue)
                {
                    lbl.BackColor = Color.Red;
                }
                else if (lbl.BackColor == Color.Red)
                {
                    lbl.BackColor = Color.Blue;
                }
            }
        }

        public static void walkBattersTimer_Tick(object sender, EventArgs e)
        {
            walkRunners();
        }

        public static void advanceHalfTimer_Tick(object sender, EventArgs e)
        {
            if (onBaseLabels.Count > 0)
            {
                if (!boolAdvanceHalf)
                {
                    for (int i = 0; i < onBaseLabels.Count; i++)
                    {
                        //if (basesCrossed < onBaseLabels.Count + (NumToAdvance -1))
                        if (basesCrossed < totalNumToAdvance)
                        {
                            //if (onBaseLabels.ElementAt(i).BaseLocation < onBaseLabels.ElementAt(i).StartingBaseLocation + NumToAdvance)
                            //{
                                advanceRunner(onBaseLabels.ElementAt(i));
                            //}
                        }
                        else
                        {
                            basesCrossed = 0;
                            totalNumToAdvance = 0;
                            clearCount();
                            advanceHalfTimer.Stop();
                            Play.endPlay();
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < onBaseLabels.Count; i++)
                    {
                        //if (basesCrossed < onBaseLabels.Count + (NumToAdvance -1))
                        if (basesCrossed < totalNumToAdvance)
                        {
                            //if (onBaseLabels.ElementAt(i).BaseLocation < onBaseLabels.ElementAt(i).StartingBaseLocation + NumToAdvance)
                            //{
                                advanceRunner(onBaseLabels.ElementAt(i));
                            //}
                        } else if (basesCrossed < totalNumToAdvance + onBaseLabels.Count)
                        {
                            advanceHalf();
                        }
                        else
                        {
                            basesCrossed = 0;
                            totalNumToAdvance = 0;
                            clearCount();
                            advanceHalfTimer.Stop();
                            Play.endPlay();
                        }
                    }
                }
            }
            else
            {
                basesCrossed = 0;
                totalNumToAdvance = 0;
                clearCount();
                advanceHalfTimer.Stop();
                Play.endPlay();
            }                
        }         
        

        private static void walkRunners()
        {
            if (walkRunnersList.Count > 0)
            {
                for(int i = 0; i < walkRunnersList.Count; i++)
                {
                    walkForcedRunners(walkRunnersList.ElementAt(i));
                }
            }
            else
            {
                clearCount();
                ScoreBoard.newCount();
                walkBattersTimer.Stop();
                startPlayTimer();
            }
                
        }

        public static void startWalkBattersTimer()
        {
            walkBattersTimer.Start();   
        }

        public static void startNewInningTimer()
        {           
            NewInningTimer.Start();
        }

        public static void startPlayTimer()
        {           
            PlayTimer.Start();
        }

        public static void startRunPlayTimer()
        {
            ScoreBoard.Balls = 0;
            ScoreBoard.Strikes = 0;
            PlayTimer.Stop();
            //PlayTimer.Enabled = false;
            runPlayTimer.Start();
        }      

        public static void addRunnerToWalkList(int baseLocation)
        {           
            foreach (RunnerLabel lbl in onBaseLabels)
            {
                if (lbl.BaseLocation == baseLocation)
                {
                    walkRunnersList.Add(lbl);
                }
            }
        }

        public static void startAdvanceHalfTimer(int numToAdvance, bool BoolAdvanceHalf, int PlayerOutBaseLocation)
        {
            for (int i = 0; i < onBaseLabels.Count; i++)
            {
                onBaseLabels.ElementAt(i).StartingBaseLocation = onBaseLabels.ElementAt(i).BaseLocation;
            }

            boolAdvanceHalf = BoolAdvanceHalf;
            NumToAdvance = numToAdvance;
            onBaseCount = onBaseLabels.Count;
            playerOutBaseLocation = PlayerOutBaseLocation;
            for (int i = 0; i < onBaseLabels.Count; i++)
            {
                if (numToAdvance + onBaseLabels.ElementAt(i).BaseLocation > 4)
                {
                    totalNumToAdvance += (4 - onBaseLabels.ElementAt(i).BaseLocation);
                }
                else
                {
                    totalNumToAdvance += numToAdvance;
                }
            }
            advanceHalfTimer.Start();
        }

        public static void recordScore()
        {
            Lists.playDescription = "";
            Lists.playDescription = "Run Scored! \n\n";
            frmBaseballSimulator.addPlayDescription();
            Lists.playDescription = "";

            if (ScoreBoard.Innings % 2 == 0 && ScoreBoard.Innings < 17)
            {
                addOneToLabel(VisitorControls.ElementAt(ScoreBoard.Innings / 2));
                addOneToLabel(lblVstrRuns);
            }

            else if (ScoreBoard.Innings % 2 != 0 && ScoreBoard.Innings < 18)
            {
                addOneToLabel(HomeControls.ElementAt(ScoreBoard.Innings / 2));
                addOneToLabel(lblHomeRuns);
            }
        }

        private static void addOneToLabel(Label lbl)
        {
            if(lbl.Text == "")
            {
                lbl.Text = "0";
            }

            lbl.Text = (int.Parse(lbl.Text) + 1).ToString();
        }

        private static void addOneToLabel(Control lbl)
        {
            if (lbl.Text == "")
            {
                lbl.Text = "0";
            }

            lbl.Text = (int.Parse(lbl.Text) + 1).ToString();
        }

        public static void addPlayDescription()
        {
            rtxtPlayDescriptions.SelectionStart = 0;
            rtxtPlayDescriptions.SelectedText = Lists.playDescription;
            Lists.playDescription = "";
        }

        public static void moveAllRunners()
        {
            for (int i = 0; i < onBaseLabels.Count; i++)
            {
                //if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left >= 514 && onBaseLabels.ElementAt(i).Visible)
                if (onBaseLabels.ElementAt(i).BaseLocation == 0)
                {
                    moveToFirst(onBaseLabels.ElementAt(i));
                }
                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left > 514 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 1)
                {
                    moveToSecond(onBaseLabels.ElementAt(i));
                }

                //else if (onBaseLabels.ElementAt(i).Top <= 240 && onBaseLabels.ElementAt(i).Left <= 514 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 2)
                {
                    moveToThird(onBaseLabels.ElementAt(i));
                }

                //else if (onBaseLabels.ElementAt(i).Top >= 240 && onBaseLabels.ElementAt(i).Left <= 512 && onBaseLabels.ElementAt(i).Visible)
                else if (onBaseLabels.ElementAt(i).BaseLocation == 3)
                {
                    moveToHome(onBaseLabels.ElementAt(i));
                }
            }
        }

        public static void advanceOne()
        {
            if (intRunnersCount < onBaseCount)
            {
                moveAllRunners();
            }
            else
            {
                advanceHalfTimer.Stop();
                intRunnersCount = 0;
                totalNumToAdvance = 0;
                basesCrossed = 0;
                Play.endPlay();
            }
        }

        public static void advanceOneHalf()
        {           

            if(intRunnersCount < onBaseCount)
            {
                moveAllRunners();
            }

            if(intRunnersCount == onBaseCount)
            {
                advanceHalf();
            }
        }

        public static void advanceTwo()
        {
            if (intRunnersCount < 2 * onBaseCount)
            {
                moveAllRunners();
            }
            else
            {
                advanceHalfTimer.Stop();
                intRunnersCount = 0;
                totalNumToAdvance = 0;
                basesCrossed = 0;
                Play.endPlay();
            }
           
        }

        public static void advanceTwoHalf()
        {
            if (intRunnersCount < 2*onBaseCount)
            {
                moveAllRunners();
            }

            if (intRunnersCount / onBaseCount == 2*onBaseCount)
            {
                advanceHalf();
            }
        }

        public static void advanceThree()
        {           
            if (intRunnersCount < 3 * onBaseCount)
            {
                moveAllRunners();
            }
            else
            {
                advanceHalfTimer.Stop();
                intRunnersCount = 0;
                totalNumToAdvance = 0;
                basesCrossed = 0;
                Play.endPlay();
            }
        }

        public static void advanceThreeHalf()
        {
            if(onBaseCount > 0)
            {
                if (intRunnersCount / onBaseCount < 3 * onBaseCount)
                {
                    moveAllRunners();
                }
                else if (intRunnersCount / onBaseCount == 3 * onBaseCount)
                {
                    advanceHalf();
                }
            }
           
        }

        public static void advanceFour()
        {         
            if (intRunnersCount / onBaseCount < 4 * onBaseCount)
            {
                moveAllRunners();
            }
            else
            {
                advanceHalfTimer.Stop();
                intRunnersCount = 0;
                totalNumToAdvance = 0;
                basesCrossed = 0;
                Play.endPlay();
            }
        }

        public static void resetLabels()
        {
            for (int i = 0; i < onBaseLabels.Count; i++)
            {
                if(onBaseLabels.ElementAt(i).BaseLocation == 1)
                {
                    onBaseLabels.ElementAt(i).Top = 239;
                    onBaseLabels.ElementAt(i).Left = 581;
                }
                else if (onBaseLabels.ElementAt(i).BaseLocation == 2)
                {
                    onBaseLabels.ElementAt(i).Left = 514;
                    onBaseLabels.ElementAt(i).Top = 172;
                }
                else if (onBaseLabels.ElementAt(i).BaseLocation == 3)
                {
                    onBaseLabels.ElementAt(i).Top = 235;
                    onBaseLabels.ElementAt(i).Left = 451;
                }                
            }
        }

        private void runPlayTimer_Tick(object sender, EventArgs e)
        {
            runPlayTimer.Stop();
            Play.runPlay();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmBaseballSimulator_Load(object sender, EventArgs e)
        {

        }
    }
    }

