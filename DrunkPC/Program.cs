using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;

//Application name - DrunkPC
//Description : Application that generates erratic mouse & keyboard movement and input and generates system sounds and fake dialog to confuse user 
//Topics:
//1>Threads
//2>System.Windows.Form namespace & assembly
//3>Hidden apllication


namespace DrunkPC
{ 
    class Program
    {
        public static Random _random = new Random();
        public static int _startUpDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("Drunk Prank Application by imsajid12");

            if(args.Length >= 2)
            {
                _startUpDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }
            //Create all the threads that manipulate all of the inputs and outputs to the system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startUpDelaySeconds);
            Console.WriteLine("Waiting 10 seconds before starting threads");
            while(future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            //Start all the threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating all threads");
            //Abort all the threads and exit the application
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();

        }

        #region Thread Functions
        /// <summary>
        /// This will randomly generate mouse movements
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Drunk Mouse Thread Started");

            int moveX = 0;
            int moveY = 0;
            while (true)
            {
                //Console.WriteLine(Cursor.Position.ToString());

                //Generate random numbers to move the cursor on X and Y
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                //Change mouyse cursor positions to new random coordinates
                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X + moveX, 
                    Cursor.Position.Y + moveY
                );
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// This will randomly generate keyboard movements
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("Drunk Keyboard Thread Started");

            while (true)
            {
                //Generate Random Capital Alphabets
                char key = (char)(_random.Next(25) + 65);

                //50/50 generate lower case
                if(_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }

                SendKeys.SendWait(key.ToString());
                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// This will randomly generate system sound
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("Drunk Sound Thread Started");

            //Determine if we aree going to play sound(40% off)
            while (true)
            {
                if(_random.Next(100) > 60)
                {
                    switch(_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// This will randomly generate notifications to use
        /// </summary>
        public static void DrunkPopupThread()
        {
            Console.WriteLine("Drunk Popup Thread Started");
            while (true)
            {
                //10% of a time show a dialog
                if(_random.Next(100) > 90)
                {
                    //Determine which messagebox should appear
                    switch(_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet Explorer stopped working", "Internet Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Your system is running low on resources", "Microsoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
                Thread.Sleep(10000);
            }
        }
        #endregion
    }
}
