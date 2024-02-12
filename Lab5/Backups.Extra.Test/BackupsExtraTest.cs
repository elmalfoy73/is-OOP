using Backups.Algorithms;
using Xunit;

namespace Backups.Extra.Test;

public class BackupsExtraTest
{
    [Fact]
    public void MergeTest()
    {
        InMemoryExtra repository = new ("User", "Repository");
        BackupObject obj1 = repository.GetBackupObject("obj.txt");
        BackupObject obj2 = repository.GetBackupObject("obj2.txt");
        BackupTaskExtra task = new (repository.Place, "task", repository, new SplitStorage());
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        RestorePoint point1 = task.CreateRestorePoint("point1", DateTime.Now);
        task.RemoveBackupObject(obj2);
        RestorePoint point2 = task.CreateRestorePoint("point2", DateTime.Now);
        task.MergeRestorePoints(point1, point2);

        foreach (var storage in point1.Storages)
        {
            Assert.Contains(storage, point2.Storages);
        }
    }

    [Fact]
    public void RecoveryTest()
    {
        InMemoryExtra repository = new ("User", "Repository");
        BackupObject obj1 = repository.GetBackupObject("obj.txt");
        BackupObject obj2 = repository.GetBackupObject("obj2.txt");
        BackupTaskExtra task = new (repository.Place, "task", repository, new SplitStorage());
        task.AddBackupObject(obj1);
        task.AddBackupObject(obj2);
        RestorePoint point = task.CreateRestorePoint("point1", DateTime.Now);
        List<BackupObject> restoredObj = task.Recovery(repository, point);

        Assert.Contains(obj1, restoredObj);
        Assert.Contains(obj2, restoredObj);
    }
}