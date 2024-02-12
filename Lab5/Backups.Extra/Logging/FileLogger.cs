using System.Text;

namespace Backups.Extra;

public class FileLogger : ILogger
{
    public FileLogger(bool timeMark, string fileName)
    {
        TimeMark = timeMark;
        FileName = fileName;
    }

    public bool TimeMark { get; }
    public string FileName { get; }
    public void Log(string log)
    {
        if (TimeMark)
        {
            log = string.Concat(DateTime.Now, " ",  log);
        }

        using (StreamWriter writer = new StreamWriter(FileName, true))
        {
            writer.WriteLineAsync("Addition");
            writer.WriteAsync("4,5");
        }
    }
}