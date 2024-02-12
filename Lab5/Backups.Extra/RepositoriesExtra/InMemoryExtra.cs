namespace Backups.Extra;

public class InMemoryExtra : IRepositoryExtra
{
    private readonly InMemory _decoratee;
    public InMemoryExtra(string place, string name)
    {
        Place = place;
        Name = name;
        _decoratee = new InMemory(place, name);
    }

    public string Place { get; }
    public string Name { get; }
    public BackupObject GetBackupObject(string fileName)
    {
        return _decoratee.GetBackupObject(fileName);
    }

    public void Save(RestorePoint restorePoint, List<Storage> storages)
    {
        _decoratee.Save(restorePoint, storages);
    }

    public void DeleteRestorePoint(Backup backup, RestorePoint restorePoint)
    {
        backup.RemoveRestorePoint(restorePoint);
    }

    public List<BackupObject> Recovery(RestorePoint restorePoint)
    {
        List<BackupObject> recoveredObj = new ();
        foreach (var storage in restorePoint.Storages)
        {
            recoveredObj.AddRange(storage.BackupObjects);
        }

        return recoveredObj;
    }

    public List<BackupObject> Recovery(RestorePoint restorePoint, string path)
    {
        List<BackupObject> recoveredObj = new ();
        foreach (var storage in restorePoint.Storages)
        {
            recoveredObj.AddRange(storage.BackupObjects);
        }

        return recoveredObj;
    }

    public void AddStorage(RestorePoint restorePoint, Storage storage)
    {
        restorePoint.Add(storage);
    }
}