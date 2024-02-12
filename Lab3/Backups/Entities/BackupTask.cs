using Backups.Algorithms;

namespace Backups;

public class BackupTask
{
    private List<BackupObject> _backupObjects;

    public BackupTask(string place, string name, IRepository repository, IStorageAlgorithm algorithm)
    {
        Place = place;
        Name = name;
        _backupObjects = new ();
        Repository = repository;
        Algorithm = algorithm;
        Backup = new ();
    }

    public string Place { get; }
    public string Name { get; }
    public IRepository Repository { get;  }
    public IStorageAlgorithm Algorithm { get; }
    public Backup Backup { get; }

    public void AddBackupObject(BackupObject obj)
    {
        _backupObjects.Add(obj);
    }

    public void RemoveBackupObject(BackupObject backupObjects)
    {
        _backupObjects.Remove(backupObjects);
    }

    public RestorePoint CreateRestorePoint(string name, DateTime date)
    {
        RestorePoint restorePoint = new (Path.Combine(Place, Name), name, date);
        Backup.AddRestorePoint(restorePoint);
        Algorithm.Save(Repository, _backupObjects, restorePoint);
        return restorePoint;
    }
}