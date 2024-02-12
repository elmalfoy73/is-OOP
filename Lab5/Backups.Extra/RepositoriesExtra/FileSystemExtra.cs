namespace Backups.Extra;

public class FileSystemExtra : IRepositoryExtra
{
    private readonly FileSystem _decoratee;
    public FileSystemExtra(string place, string name)
    {
        Place = place;
        Name = name;
        _decoratee = new FileSystem(place, name);
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
        DirectoryInfo restorePointDirectory = new (Path.Combine(restorePoint.Place, restorePoint.Name));
        restorePointDirectory.Delete(true);
    }

    public List<BackupObject> Recovery(RestorePoint restorePoint)
    {
        ZipArchiver zip = new ();
        DirectoryInfo repoDirectory = new (Path.Combine(Place, Name));
        foreach (var storage in restorePoint.Storages)
        {
            foreach (var obj in storage.BackupObjects)
            {
                FileInfo file = new (obj.FilePatch);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            zip.Unarchive(_decoratee, repoDirectory, storage);
        }

        List<BackupObject> recoveredObj = new ();
        foreach (var storage in restorePoint.Storages)
        {
            recoveredObj.AddRange(storage.BackupObjects);
        }

        return recoveredObj;
    }

    public List<BackupObject> Recovery(RestorePoint restorePoint, string path)
    {
        ZipArchiver zip = new ();
        DirectoryInfo directory = new (path);
        foreach (var storage in restorePoint.Storages)
        {
            zip.Unarchive(_decoratee, directory, storage);
        }

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

        DirectoryInfo repoDirectory = new (Path.Combine(Place, Name));
        DirectoryInfo restorePointDirectory = new (Path.Combine(restorePoint.Place, restorePoint.Name));

        ZipArchiver archiver = new ();
        archiver.Archive(_decoratee, repoDirectory, restorePointDirectory, storage);
    }
}