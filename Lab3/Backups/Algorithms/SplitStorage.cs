namespace Backups.Algorithms;

public class SplitStorage : IStorageAlgorithm
{
    public void Save(IRepository repository, List<BackupObject> backupObjects, RestorePoint restorePoint)
    {
        List<Storage> storages = new ();
        foreach (var obj in backupObjects)
        {
            Storage storage = new (Path.Combine(restorePoint.Place, restorePoint.Name), string.Concat("storage", backupObjects.IndexOf(obj).ToString()));
            storage.Add(obj);
            storages.Add(storage);
        }

        repository.Save(restorePoint, storages);
    }
}