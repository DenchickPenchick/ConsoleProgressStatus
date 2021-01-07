using System;
using System.Threading;

namespace ProgressBars
{
    public class Status : IProgress<string>
    {
        public string Keyword { get; set; }
        public string CurrentThing { get; private set; } = "NONE";
        public int XPosOfStatus { get; private set; }
        public int YPosOfStatus { get; private set; }
        private readonly bool WithAnimation = false;
        private int StepOfAnim = 1;
        private bool AnimStop = false;

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
