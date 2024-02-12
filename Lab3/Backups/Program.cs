using Backups.Algorithms;

namespace Backups;

public class Program
{
    public static void Main()
    {
        DirectoryInfo directory = new DirectoryInfo("/Users/elizabeth/Documents");
        DirectoryInfo mainDirectory = directory.CreateSubdirectory("Test2");
        DirectoryInfo repoDirectory = mainDirectory.CreateSubdirectory("Repository");

        FileSystem repository = new FileSystem(mainDirectory.FullName, repoDirectory.Name);
        BackupObject obj1 = repository.GetBackupObject("obj.txt");
        BackupObject obj2 = repository.GetBackupObject("obj2.txt");
        BackupTask task = new (mainDirectory.FullName, "task", repository, new SingleStorage());
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        RestorePoint point1 = task.CreateRestorePoint("point1", DateTime.Now);
    }
}