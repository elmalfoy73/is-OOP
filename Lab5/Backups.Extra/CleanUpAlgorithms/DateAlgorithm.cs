namespace Backups.Extra.CleanUpAlgorithms;

public class DateAlgorithm : ICleanUpAlgorithm
{
    public IEnumerable<RestorePoint> SelectPointsToClear(Backup backup, int n, DateTime pointDate)
    {
        return backup.RestorePoints.Where(p => p.Date > pointDate);
    }
}