namespace Backups.Algorithms;

public class SingleStorage : IStorageAlgorithm
{
    public void Save(IRepository repository, List<BackupObject> backupObjects, RestorePoint restorePoint)
    {
        List<Storage> storages = new ();
        Storage storage = new (Path.Combine(restorePoint.Place, restorePoint.Name), "storage");
        foreach (var obj in backupObjects)
        {
            storage.Add(obj);
        }

        storages.Add(storage);
        repository.Save(restorePoint, storages);
    }
}