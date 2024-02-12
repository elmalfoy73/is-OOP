namespace Backups;

public class Storage
{
    private List<BackupObject> _backupObjects;

    public Storage(string path, string name)
    {
        _backupObjects = new ();
        Place = path;
        Name = name;
    }

    public string Place { get;  }
    public string Name { get;  }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public void Add(BackupObject obj)
    {
        _backupObjects.Add(obj);
        obj.AddStoragePath(Place);
    }
}