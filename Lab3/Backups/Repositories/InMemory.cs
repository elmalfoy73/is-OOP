namespace Backups;

public class InMemory : IRepository
{
    public InMemory(string place, string name)
    {
        Place = place;
        Name = name;
    }

    public string Place { get; }
    public string Name { get; }

    public BackupObject GetBackupObject(string fileName)
    {
        return new BackupObject(Path.Combine(Place, Name, fileName));
    }

    public void Save(RestorePoint restorePoint, List<Storage> storages)
    {
        restorePoint.AddRange(storages);
    }
}