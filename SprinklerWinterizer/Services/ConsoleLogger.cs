using SprinklerWinterizer.Interfaces;
using System;

namespace SprinklerWinterizer.Services
{
    public class ConsoleLogger : ILogger
    {

        public void Log(string status)
        {
            Console.WriteLine("{0}|{1}", DateTime.Now, status);
        }

        public void Log(string format, params object[] status)
        {
            Console.WriteLine("{0}|{1}", DateTime.Now, string.Format(format, status));
        }

        public void LogSeparator()
        {
            Log("=============================");
        }

    }
}
