using Backups.Algorithms;
using Xunit;

namespace Backups.Test;
public class BackupsTest
{
    [Fact]
    public void СreateBackupSplitStorage()
    {
        string path = Path.Combine("Users", "elizabeth", "Documents", "Test2");
        InMemory repository = new (path, "Repository");
        BackupObject obj1 = repository.GetBackupObject(Path.Combine(path, repository.Name, "obj.txt"));
        BackupObject obj2 = repository.GetBackupObject(Path.Combine(path, repository.Name, "obj2.txt"));
        BackupTask task = new (path, "task1", repository, new SplitStorage());
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        RestorePoint point1 = task.CreateRestorePoint("point1", DateTime.Now);

        task.RemoveBackupObject(obj2);
        RestorePoint point2 = task.CreateRestorePoint("point2", DateTime.Now);
        Assert.Contains(point1, task.Backup.RestorePoints);
        Assert.Contains(point2, task.Backup.RestorePoints);
        Assert.Equal(2, point1.Storages.Count);
        Assert.Equal(1, point2.Storages.Count);
    }
}