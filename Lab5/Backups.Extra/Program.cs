using Backups.Algorithms;

namespace Backups.Extra;

public class Program
{
    public static void Main()
    {
        DirectoryInfo directory = new ("/Users/elizabeth/Documents");
        DirectoryInfo mainDirectory = directory.CreateSubdirectory("TestExtra");
        DirectoryInfo repoDirectory = mainDirectory.CreateSubdirectory("Repository");

        FileSystemExtra repository = new (mainDirectory.FullName, repoDirectory.Name);
        BackupObject obj1 = repository.GetBackupObject("obj.txt");
        BackupObject obj2 = repository.GetBackupObject("obj2.txt");
        BackupTaskExtra task = new (mainDirectory.FullName, "task", repository, new SingleStorage());
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);

        RestorePoint point = task.CreateRestorePoint("point1", DateTime.Now);
        task.Recovery(repository, point, Path.Combine(mainDirectory.FullName, "newRepo"));

        ConsoleLogger logger = new (true);
        LoggingBackupTask task2 = new (mainDirectory.FullName, "task2", repository, new SplitStorage(), logger);
        task2.AddBackupObject(obj1);
        task2.AddBackupObject(obj2);
        RestorePoint point1 = task2.CreateRestorePoint("point1", DateTime.Now);
        task2.RemoveBackupObject(obj2);
        RestorePoint point2 = task2.CreateRestorePoint("point2", DateTime.Now);
        task2.MergeRestorePoints(point1, point2);
    }
}