namespace Backups.Extra.CleanUpAlgorithms;

public interface ICleanUpAlgorithm
{
    public IEnumerable<RestorePoint> SelectPointsToClear(Backup backup, int n, DateTime pointDate);
}