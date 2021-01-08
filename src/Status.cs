using System;
using System.Threading;

namespace ProgressBars
{
    /// <summary>
    /// Indicates doing process by name like this. Installing Image.img...
    /// </summary>
    public class Status : IProgress<string>
    {
        /// <summary>
        /// Set word that would indicate kind of process. If keyword equals "Installing": Installing MyApp.exe...
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// Current thing in progress
        /// </summary>
        public string CurrentThing { get; private set; } = "NONE";
        /// <summary>
        /// Position of status (X)
        /// </summary>
        public int XPosOfStatus { get; private set; }
        /// <summary>
        /// Position of status (Y)
        /// </summary>
        public int YPosOfStatus { get; private set; }
        private readonly bool WithAnimation = false;
        private int StepOfAnim = 1;
        private bool AnimStop = false;

        /// <summary>
        /// Initializes new <see cref="Status"/>
        /// </summary>
        /// <param name="keyword">Set word that would indicate kind of process. If keyword equals "Installing": Installing MyApp.exe...</param>
        /// <param name="animation">If true progress will be write animation like this. Installing Image.img. Installing Image.img.. Installing Image.img...</param>
        public Status(string keyword, bool animation)
        {
            XPosOfStatus = Console.CursorLeft;
            YPosOfStatus = Console.CursorTop;
            Keyword = keyword;
            WithAnimation = animation;
            Report(CurrentThing);
            if (animation)            
                new Thread(x => 
                {
                    while (!AnimStop)
                    {
                        Report(CurrentThing);
                        Thread.Sleep(300);
                    }                    
                }).Start();            
        }

        /// <summary>
        /// Report of changing value of progress
        /// </summary>
        /// <param name="value">New value</param>
        public void Report(string value)
        {            
            CurrentThing = value;
            Console.SetCursorPosition(XPosOfStatus, YPosOfStatus);
            Console.WriteLine($"{Keyword} {CurrentThing}");
            Console.SetCursorPosition(XPosOfStatus + $"{Keyword} {CurrentThing}".Length, YPosOfStatus);
            if (WithAnimation)
            {
                Console.Write("   ");
                Console.SetCursorPosition(XPosOfStatus + $"{Keyword} {CurrentThing}".Length, YPosOfStatus);
                Console.Write(AnimImg());
            }
            else
                Console.Write("...");
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public void Stop(bool WithComplete = false, string CompletedString = "Completed")
        {
            AnimStop = true;
            if (WithComplete)
            {
                Console.SetCursorPosition(XPosOfStatus, YPosOfStatus);
                string easierString = null;
                for (int i = 0; i < $"{Keyword} {CurrentThing}...".Length; i++)
                    easierString += " ";
                Console.WriteLine(easierString);
                Console.SetCursorPosition(XPosOfStatus, YPosOfStatus);
                Console.WriteLine(CompletedString);
            }
        }

        private string AnimImg()
        {
            switch (StepOfAnim)
            {
                case 1:
                    StepOfAnim++;
                    return ".";
                case 2:
                    StepOfAnim++;
                    return "..";
                case 3:
                    StepOfAnim = 1;
                    return "...";
                default:
                    StepOfAnim = 1;
                    return ".";
            }
        }
    }
}
