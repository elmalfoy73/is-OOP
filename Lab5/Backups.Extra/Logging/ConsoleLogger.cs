namespace Backups.Extra;

public class ConsoleLogger : ILogger
{
    public ConsoleLogger(bool timeMark)
    {
        TimeMark = timeMark;
    }

    public bool TimeMark { get; }
    public void Log(string log)
    {
        if (TimeMark)
        {
            log = string.Concat(DateTime.Now, " ",  log);
        }

        System.Console.WriteLine(log);
    }
}