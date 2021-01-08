using System;
using System.Collections.Generic;
using System.Text;

namespace ProgressBars.src
{
    /// <summary>
    /// Indicates doing proccess by index like this. Done 4/17
    /// </summary>
    public class Count : IProgress<int>
    {
        /// <summary>
        /// Set word that would indicate kind of process. If keyword equals "Installing": Installing 3/21
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// Position of count (X)
        /// </summary>
        public int XPosOfCount { get; set; }
        /// <summary>
        /// Position of count (Y)
        /// </summary>
        public int YPosOfCount { get; set; }
        /// <summary>
        /// Index of proccess
        /// </summary>
        public int Index { get; set; } = 0;
        /// <summary>
        /// Count of procceses
        /// </summary>
        public int ThingsCount { get; set; } = 0;

        /// <summary>
        /// Initilizes new <see cref="Count"/>
        /// </summary>
        /// <param name="keyword">Set word that would indicate kind of process. If keyword equals "Installing": Installing 3/21</param>
        /// <param name="count">Count of procceses</param>
        public Count(string keyword, int count)
        {
            Keyword = keyword;
            ThingsCount = count;
            XPosOfCount = Console.CursorLeft;
            YPosOfCount = Console.CursorTop;
            Report(Index);
        }

        /// <summary>
        /// Report of changing value of progress
        /// </summary>
        /// <param name="value">New value</param>
        public void Report(int value)
        {
            Console.SetCursorPosition(XPosOfCount, YPosOfCount);
            Console.WriteLine($"{Keyword}: {Index}/{ThingsCount}");
            Index++;
        }

        /// <summary>
        /// Set completed string like this. Completed
        /// </summary>
        /// <param name="CompletedString">Set key word that will appear when proccess end.</param>
        public void SetCompleted(string CompletedString)
        {
            Console.SetCursorPosition(XPosOfCount, YPosOfCount);
            string easierString = null;
            for (int i = 0; i < $"{Keyword}: {Index}/{ThingsCount}".Length; i++)
                easierString += " ";
            Console.WriteLine(easierString);
            Console.SetCursorPosition(XPosOfCount, YPosOfCount);
            Console.WriteLine(CompletedString);
        }
    }
}
