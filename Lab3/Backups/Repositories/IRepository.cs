namespace Backups;

public interface IRepository
{
    string Place { get; }
    string Name { get; }

    public BackupObject GetBackupObject(string fileName);

    public void Save(RestorePoint restorePoint, List<Storage> storages);
}