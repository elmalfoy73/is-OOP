namespace Backups.Extra.CleanUpAlgorithms;

public class QuantityAlgorithm : ICleanUpAlgorithm
{
    public IEnumerable<RestorePoint> SelectPointsToClear(Backup backup, int n, DateTime pointDate)
    {
        return backup.GetRange(3);
    }
}