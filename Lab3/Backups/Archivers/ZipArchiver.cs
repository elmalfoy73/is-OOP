using System.IO.Compression;

namespace Backups;

public class ZipArchiver : IArchiver
{
    public void Archive(FileSystem repository, DirectoryInfo repoDirectory, DirectoryInfo restorePointDirectory, Storage storage)
    {
         DirectoryInfo storageDirectory = repository.CreateSubdirectory(repoDirectory, storage.Name);

         foreach (var backupObj in storage.BackupObjects)
         {
             FileInfo file = new (backupObj.FilePatch);
             file.CopyTo(Path.Combine(storageDirectory.FullName, file.Name));
         }

         FileInfo zipFile = new (Path.Combine(storage.Place, string.Concat(storage.Name, ".zip")));
         if (zipFile.Exists)
         {
             ZipFile.CreateFromDirectory(
                storageDirectory.FullName,
                Path.Combine(restorePointDirectory.FullName, string.Concat(storageDirectory.Name, "(2)", ".zip")));
         }
         else
         {
            ZipFile.CreateFromDirectory(
                storageDirectory.FullName,
                Path.Combine(restorePointDirectory.FullName, string.Concat(storageDirectory.Name, ".zip")));
         }

         repository.DeleteDirectory(storageDirectory);
    }

    public void Unarchive(FileSystem repository, DirectoryInfo directory, Storage storage)
    {
        ZipFile.ExtractToDirectory(Path.Combine(storage.Place, string.Concat(storage.Name, ".zip")), directory.FullName);
    }
}