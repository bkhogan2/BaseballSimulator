using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DrawingCircles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }        

        private bool atHome = true;
        private bool atFirst = false;
        private bool atSecond = false;
        private bool atThird = false;

        private bool twoToRun = false;        
        private bool atFirst1 = false;
        private bool atSecond1 = false;
        private bool atThird1 = false;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {           
            
        }

        //Thread th;
        //Thread th1;
        System.Windows.Forms.Label label2;

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            moveToSecond(label1);
           
            moveToFirst(label2);
        }

        //public void Thread()
        //{
        //    if (atHome == true)
        //    {
        //        label1.Left += 1;
        //        label1.Top -= 1;

        //        if (label1.Left >= 245)
        //        {
        //            atHome = false;
        //            atFirst = true;

        //        }
        //    }
        //    else if (atFirst)
        //    {
        //        label1.Left -= 1;
        //        label1.Top -= 1;

        //        if (label1.Top >= 147)
        //        {
        //            label1.Left -= 7;
        //            atFirst = false;
        //            atSecond = true;
        //        }
        //    }
        //    else if (atSecond)
        //    {
        //        label1.Left -= 1;
        //        label1.Top += 1;

        //        if (label1.Left <= 50)
        //        {
        //            atSecond = false;
        //            atThird = true;
        //        }
        //    }
        //    else if (atThird)
        //    {
        //        label1.Left += 1;
        //        label1.Top += 1;

        //        if (label1.Left <= 100)
        //        {
        //            atThird = false;
        //            atHome = true;
        //        }
        //    }

        //    Invalidate();
        //}

        public void moveToSecond(System.Windows.Forms.Label label1)
        {
            if (atHome)
            {
                label1.Left += 1;
                label1.Top -= 1;

                if (label1.Left >= 245)
                {
                    atHome = false;
                    atFirst = true;
                    createNewLabel();
                    twoToRun = true;
                }
            }
            else if (atFirst)
            {
                label1.Left -= 1;
                label1.Top -= 1;

                if (label1.Top <= 147)
                {
                    label1.Left -= 7;
                    atFirst = false;
                    atSecond = true;
                    
                    timer1.Stop();
                    timer1.Dispose();
                }
            }
            else if (atSecond)
            {
                label1.Left -= 1;
                label1.Top += 1;

                if (label1.Left <= 108)
                {
                    atSecond = false;
                    atThird = true;
                }
            }
            else if (atThird)
            {
                label1.Left += 1;
                label1.Top += 1;

                if (label1.Top >= 280)
                {
                    atThird = false;
                    atHome = true;
                }
            }

            Invalidate();
        }

        public void moveToFirst(System.Windows.Forms.Label label1)
        {
            if (twoToRun)
            {
                label1.Left += 1;
                label1.Top -= 1;

                if (label1.Left >= 245)
                {
                    atHome = false;
                    atFirst = false;
                }
            }
            else if (atFirst1)
            {
                label1.Left -= 1;
                label1.Top -= 1;

                if (label1.Top <= 147)
                {
                    label1.Left -= 7;
                    atFirst1 = false;
                    atSecond1 = true;
                    timer1.Stop();
                    timer1.Dispose();
                }
            }
            else if (atSecond1)
            {
                label1.Left -= 1;
                label1.Top += 1;

                if (label1.Left <= 108)
                {
                    atSecond1 = false;
                    atThird1 = true;
                }
            }
            else if (atThird1)
            {
                label1.Left += 1;
                label1.Top += 1;

                if (label1.Top >= 280)
                {
                    atThird1 = false;
                    twoToRun = true;
                }
            }

            Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            //Label label2 = new Label();
            //label2.Left = 200;
            //label2.Top = 300;
            //label2.AutoSize = false;
            //label2.Width = 10;
            //label2.Height = 10;
            //label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //label2.BackColor = Color.Blue;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
            //this.Controls.Add(label2);
            //label2.BringToFront();
            //label2.Visible = true;
            //label2.Left = 800;
            //label2.Top = 300;
            //label2.AutoSize = false;
            //label2.Width = 10;
            //label2.Height = 10;
            //label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //label2.BackColor = Color.Blue;

            createNewLabel();
        }

        private void createNewLabel()
        {
            label2 = new System.Windows.Forms.Label();
            this.Controls.Add(label2);
            label2.BringToFront();

            
            label2.Left = 174;
            label2.Top = 280;
            label2.AutoSize = false;
            label2.Width = 20;
            label2.Height = 20;
            label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            label2.BackColor = Color.Blue;
        }
    }
}
