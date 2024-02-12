namespace Backups;

public interface IArchiver
{
    public void Archive(FileSystem repository, DirectoryInfo repoDirectory, DirectoryInfo restorePointDirectory, Storage storage);
    public void Unarchive(FileSystem repository, DirectoryInfo directory, Storage storage);
}