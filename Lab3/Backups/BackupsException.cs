namespace Backups;

public class BackupsException : Exception
{
    public BackupsException(string message)
        : base(message)
    {
    }
}