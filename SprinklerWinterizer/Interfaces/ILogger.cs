namespace SprinklerWinterizer.Interfaces
{
    public interface ILogger
    {
        void Log(string status);
        void Log(string format, params object[] status);
        void LogSeparator();
    }
}
