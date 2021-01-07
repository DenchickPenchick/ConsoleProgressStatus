# ConsoleProgressStatus
There is console progress bars. They can help you show progress of your proccess. For example, downloading files from the Internet. 
# Instruction
You can use this stuff like in example:
```csharp
using System.Threading;
using ProgressBars;

namespace TestOfProgressBar
{
    class Program
    {
        static void Main(string[] args)
        {
            var bar = new ProgressBar(true, true);
            for (int i = 0; i<=100; i+=10)
            {
                bar.Report(i);
                Thread.Sleep(1000);
            }
            bar.Stop();
            
            var status = new Status("Installing", true);                       
            status.Report("MyApp.exe");
            Thread.Sleep(1000);
            status.Report("Text.txt");
            Thread.Sleep(1000);
            status.Report("Image.jpeg");
            Thread.Sleep(1000);
            status.Stop();
        }
    }
}

```
