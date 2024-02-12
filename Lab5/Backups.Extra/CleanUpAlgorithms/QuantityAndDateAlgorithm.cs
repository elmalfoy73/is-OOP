namespace Backups.Extra.CleanUpAlgorithms;

public class QuantityAndDateAlgorithm : ICleanUpAlgorithm
{
    public IEnumerable<RestorePoint> SelectPointsToClear(Backup backup, int n, DateTime pointDate)
    {
        List<RestorePoint> restorePoints = backup.GetRange(3);
        return restorePoints.Where(p => p.Date > pointDate);
    }
}