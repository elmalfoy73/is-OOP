namespace Backups.Extra.CleanUpAlgorithms;

public class QuantityOrDateAlgorithm : ICleanUpAlgorithm
{
    public IEnumerable<RestorePoint> SelectPointsToClear(Backup backup, int n, DateTime pointDate)
    {
        List<RestorePoint> restorePointsOutOfRange = backup.GetRange(3);
        IEnumerable<RestorePoint> restorePointsOutOfDate = backup.RestorePoints.Where(p => p.Date > pointDate);
        return restorePointsOutOfRange.Union(restorePointsOutOfDate);
    }
}