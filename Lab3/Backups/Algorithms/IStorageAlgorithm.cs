namespace Backups.Algorithms;

public interface IStorageAlgorithm
{
    public void Save(IRepository repository, List<BackupObject> backupObjects, RestorePoint restorePoint);
}