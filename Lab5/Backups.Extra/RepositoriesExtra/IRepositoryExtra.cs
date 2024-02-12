namespace Backups.Extra;

public interface IRepositoryExtra : IRepository
{
    public void DeleteRestorePoint(Backup backup, RestorePoint restorePoint);
    public List<BackupObject> Recovery(RestorePoint restorePoint);
    public List<BackupObject> Recovery(RestorePoint restorePoint, string path);
    public void AddStorage(RestorePoint restorePoint, Storage storage);
}