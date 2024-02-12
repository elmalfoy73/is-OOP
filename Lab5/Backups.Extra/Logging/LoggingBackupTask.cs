using System.Drawing;
using Backups.Algorithms;
using Backups.Extra.CleanUpAlgorithms;

namespace Backups.Extra;

public class LoggingBackupTask
{
    private readonly BackupTaskExtra _decoratee;
    private readonly ILogger _logger;
    public LoggingBackupTask(string place, string name, IRepositoryExtra repository, IStorageAlgorithm algorithm, ILogger logger)
    {
        _decoratee = new (place, name, repository, algorithm);
        _logger = logger;
    }

    public void AddBackupObject(BackupObject obj)
    {
        _decoratee.AddBackupObject(obj);
        _logger.Log(string.Concat("Added Backup Object ", obj.FilePatch, "In task", _decoratee.Name));
    }

    public void RemoveBackupObject(BackupObject obj)
    {
        _decoratee.RemoveBackupObject(obj);
        _logger.Log(string.Concat("Removed Backup Object ", obj.FilePatch, "from task ", _decoratee.Name));
    }

    public RestorePoint CreateRestorePoint(string name, DateTime date)
    {
        RestorePoint point = _decoratee.CreateRestorePoint(name, date);
        _logger.Log(string.Concat("Created Restore Point ", point.Name, "in task ", _decoratee.Name));
        return point;
    }

    public void CleanRestorePoints(ICleanUpAlgorithm algorithm, int n, DateTime date)
    {
        _decoratee.CleanRestorePoints(algorithm, n, date);
        _logger.Log(string.Concat("Cleaned Restore Points", "in task ", _decoratee.Name));
    }

    public void MergeRestorePoints(RestorePoint oldPoint, RestorePoint newPoint)
    {
        _decoratee.MergeRestorePoints(oldPoint, newPoint);
        _logger.Log(string.Concat("Merged Restore Point ", oldPoint.Name, "in point ", newPoint.Name));
    }

    public List<BackupObject> Recovery(IRepositoryExtra repository, RestorePoint point)
    {
        List<BackupObject> restoredObj = _decoratee.Recovery(repository, point);
        _logger.Log(string.Concat("Recovered from Restore Point ", point.Name, "to original location ", repository.Name));
        return restoredObj;
    }

    public List<BackupObject> Recovery(IRepositoryExtra repository, RestorePoint point, string path)
    {
        List<BackupObject> restoredObj = _decoratee.Recovery(repository, point, path);
        _logger.Log(string.Concat("Recovered from Restore Point ", point.Name, "to different location ", path));
        return restoredObj;
    }
}