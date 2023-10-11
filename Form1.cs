using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Projekt
{
    public partial class Form1 : Form
    {
        private static Semaphore semaphore_BR = new Semaphore(1, 1); 
        private static Semaphore semaphore_BY = new Semaphore(1, 1); 
        private static Semaphore semaphore_RY = new Semaphore(1, 1); 

        private static Semaphore semaphore_Y_long = new Semaphore(1, 1); 

        bool flagStart; 
        bool step; 
        bool stepInit;

        int timeR;
        int timeB; 
        int timeY;

        public Form1()
        {
            InitializeComponent(); 
            Init(); 
        }

        public void Init() 
        {

            Thread threadY = new Thread(SnakeY_Function); 
            threadY.IsBackground = true; 
            Thread threadB = new Thread(SnakeB_Function);
            threadB.IsBackground = true;
            Thread threadR = new Thread(SnakeR_Function);
            threadR.IsBackground = true;

            timeB = 50;
            timeR = 50;
            timeY = 50;

            step = true; 
            stepInit = false; 
            flagStart = false; 

            SpeedBlue.Value = timeB / 10;
            SpeedRed.Value = timeR / 10;
            SpeedYellow.Value = timeY / 10;

            threadY.Start();
            threadR.Start();
            threadB.Start();
        }

        public void SnakeY_Function()
        {
            int lenght = 5;
            int tail = 0; 
            int head = tail + lenght;

            PictureBox[] Tab = new PictureBox[] {Y1, Y2, Y3, Y4, Y5, Y6, Y7, Shared1, Shared2, Shared3,
            Shared4, Shared5, Shared6, Shared7, Y8, Y9, Y10, Y11, Y12, Y13, Y14, Y15, Y16, Y17, Y18, Y19};

        while (true) 
        {
            if (flagStart)
            {
                if (step)
                {
                    Thread.Sleep(timeY);

                    if (Tab[head] == Shared1)
                    {
                        semaphore_Y_long.WaitOne();
                        semaphore_BY.WaitOne();
                    }

                    if (Tab[head] == Shared4)
                    {
                        semaphore_RY.WaitOne();
                    }

                    Tab[tail].BackColor = Color.White; 
                    Tab[head].BackColor = Color.Yellow; 
                    tail++; 
                    head++; 

                    if (head == Tab.Length) head = 0; 
                    if (tail == Tab.Length) tail = 0; 

                    if (stepInit) step = false;

                    if (Tab[tail] == Shared5)
                    {
                        semaphore_BY.Release(); 
                    }

                    if (Tab[tail] == Y8)
                    {
                        semaphore_Y_long.Release();
                        semaphore_RY.Release();
                    }

                }

            }
        }
        }

        public void SnakeB_Function()
        {
            int lenght = 7;
            int tail = 0;
            int head = tail + lenght;
            PictureBox[] Tab = new PictureBox[] {B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, 
            Shared8, Shared9, Shared10, Shared11, Shared12, Shared13, Shared14, Shared15, Shared16, Shared4,
            Shared3, Shared2, Shared1, B14, B15};

            while (true)
            {
                if (flagStart)
                {
                    if (step)
                    {
                        Thread.Sleep(timeB);
                        if (Tab[head] == Shared8)
                        {
                            semaphore_BR.WaitOne();
                        }

                        if (Tab[head] == Shared4)
                        {
                            semaphore_BY.WaitOne();
                        }

                        Tab[tail].BackColor = Color.White;
                        Tab[head].BackColor = Color.Blue;
                        tail++;
                        head++;
                        if (head == Tab.Length) head = 0;
                        if (tail == Tab.Length) tail = 0;
                        if (stepInit) step = false;

                        if (Tab[tail] == Shared3)
                        {
                            semaphore_BR.Release();
                        }

                        if (Tab[tail] == B14)
                        {
                            semaphore_BY.Release();
                        }
                    }

                }
            }
        } 

        public void SnakeR_Function()
        {
            int lenght = 9;
            int tail = 0;
            int head = tail + lenght;
            PictureBox[] Tab = new PictureBox[] {R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, 
            Shared7, Shared6, Shared5, Shared4, Shared16, Shared15, Shared14, Shared13, Shared12, Shared11,
            Shared10, Shared9, Shared8, R13, R14, R15, R16, R17};

            while (true)
            {
                if (flagStart)
                {
                    if (step)
                    {
                        Thread.Sleep(timeR);
                        if (Tab[head] == Shared4)
                        {
                            semaphore_BR.WaitOne();
                        }

                        if (Tab[head] == Shared7)
                        {
                            semaphore_Y_long.WaitOne();
                            semaphore_RY.WaitOne();
                        }

                        if (Tab[head] == Shared16)
                        {
                            semaphore_Y_long.Release();
                        }

                        Tab[tail].BackColor = Color.White;
                        Tab[head].BackColor = Color.Red;
                        tail++;
                        head++;
                        if (head == Tab.Length) head = 0;
                        if (tail == Tab.Length) tail = 0;
                        if (stepInit) step = false;

                        if (Tab[tail] == R13)
                        {
                            semaphore_BR.Release();
                        }

                        if (Tab[tail] == Shared16)
                        {
                            semaphore_RY.Release();
                        }
                    }

                }
            }
        } 

        public void button1_Click(object sender, EventArgs e)
        {
            stepInit = false; 
            step = true; 
            flagStart = true; 
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            flagStart= false; 
        } 

        private void button3_Click(object sender, EventArgs e)
        {
            stepInit = true; 
            step = true; 
            flagStart = true;
        } 

        private void SpeedYellow_Scroll(object sender, EventArgs e)
        {
            int max = 120; 
            timeY = max - SpeedYellow.Value * 11; 
            if(timeY <= 0) timeY = 10; 
        } 

        private void SpeedRed_Scroll(object sender, EventArgs e)
        {
            int max = 120;
            timeR = max - SpeedRed.Value * 11;
            if (timeR <= 0) timeR = 10;
        } 

        private void SpeedBlue_Scroll(object sender, EventArgs e)
        {
            int max = 120;
            timeB = max - SpeedBlue.Value * 11;
            if (timeB <= 0) timeB = 10;
        } 
    }
}
