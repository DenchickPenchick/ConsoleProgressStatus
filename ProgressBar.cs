using System;
using System.Threading;

namespace ProgressBars
{
    /// <summary>
    /// Indicates doing process by line like this [####------]
    /// </summary>
    public class ProgressBar : IProgress<int>
    {
        /// <summary>
        /// Current percent of procces.
        /// </summary>
        public int CurrentValue = 0;
        /// <summary>
        /// Position of progress bar (X)
        /// </summary>
        public int XPosOfProgressBar { get; private set; }
        /// <summary>
        /// Position of progress bar (Y)
        /// </summary>
        public int YPosOfProgressBar { get; private set; }
        private readonly bool WithPercent = false;
        private readonly bool WithAnimation = false;
        private int StepOfAnim = 1;        
        private bool AnimStop = false;

        /// <summary>
        /// Initializes new <see cref="ProgressBar"/>
        /// </summary>
        /// <param name="percent">If true progress will be write percent like this. Done 20% [##------]</param>
        /// <param name="animation">If true progress will be write animation like this. Done 20% [##------] // Done 30% [##------] ==</param>
        public ProgressBar(bool percent = false, bool animation = false)
        {
            WithPercent = percent;
            WithAnimation = animation;
            XPosOfProgressBar = Console.CursorLeft;
            YPosOfProgressBar = Console.CursorTop;
            Report(0);
            if (animation)
            {
                new Thread(x =>
                {
                    while (!AnimStop)
                    {
                        Report(CurrentValue);
                        Thread.Sleep(100);
                    }                    
                }).Start();                
            }            
        }

        /// <summary>
        /// Report of changing value of progress
        /// </summary>
        /// <param name="value">New value</param>
        public void Report(int value)
        {
            CurrentValue = value;
            value /= 10;            
            Console.SetCursorPosition(XPosOfProgressBar, YPosOfProgressBar);
            Console.WriteLine($"{(WithPercent == true ? $"Done {CurrentValue}% " : null)}[{(value>=1 ? "#" : "-")}{(value >= 2 ? "#" : "-")}{(value >= 3 ? "#" : "-")}{(value >= 4 ? "#" : "-")}{(value >= 5 ? "#" : "-")}" +
                $"{(value >= 6 ? "#" : "-")}{(value >= 7 ? "#" : "-")}{(value >= 8 ? "#" : "-")}{(value >= 9 ? "#" : "-")}{(value >= 10 ? "#" : "-")}] {(WithAnimation == true ? AnimImg() : null)}");           
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public void Stop()
        {
            AnimStop = true;
        }

        private string AnimImg()
        {
            switch (StepOfAnim)
            {
                case 1:
                    StepOfAnim++;
                    return "||";
                case 2:
                    StepOfAnim++;
                    return "//";
                case 3:
                    StepOfAnim++;
                    return "==";
                case 4:
                    StepOfAnim++;
                    return @"\\";
                case 5:
                    StepOfAnim++;
                    return "||";
                case 6:
                    StepOfAnim++;
                    return "//";
                case 7:
                    StepOfAnim++;
                    return "==";
                case 8:
                    StepOfAnim = 1;
                    return @"\\";
                default:
                    return "||";
            }
        }
    }
}
