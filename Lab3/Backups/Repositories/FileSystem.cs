namespace Backups;

public class FileSystem : IRepository
{
    public FileSystem(string place, string name)
    {
        Place = place;
        Name = name;
    }

    public string Place { get;  }
    public string Name { get; }

    public BackupObject GetBackupObject(string fileName)
    {
        FileInfo file = new (Path.Combine(Place, Name, fileName));
        if (!file.Exists)
        {
            file.Create();
        }

        return new BackupObject(Path.Combine(Place, Name, fileName));
    }

    public void Save(RestorePoint restorePoint, List<Storage> storages)
    {
        restorePoint.AddRange(storages);

        DirectoryInfo repoDirectory = new (Path.Combine(Place, Name));
        DirectoryInfo restorePointDirectory = new (Path.Combine(restorePoint.Place, restorePoint.Name));
        restorePointDirectory.Create();

        foreach (var storage in storages)
        {
            ZipArchiver archiver = new ();
            archiver.Archive(this, repoDirectory, restorePointDirectory, storage);
        }
    }

    public void CreateDirectory(DirectoryInfo directory)
    {
        directory.Create();
    }

    public DirectoryInfo CreateSubdirectory(DirectoryInfo directory, string subDirectory)
    {
        return directory.CreateSubdirectory(subDirectory);
    }

    public void DeleteDirectory(DirectoryInfo directory)
    {
        directory.Delete(true);
    }
}