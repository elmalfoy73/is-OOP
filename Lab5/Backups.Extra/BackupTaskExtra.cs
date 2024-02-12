using Backups.Algorithms;
using Backups.Extra.CleanUpAlgorithms;

namespace Backups.Extra;

public class BackupTaskExtra : BackupTask
{
    public BackupTaskExtra(string place, string name, IRepositoryExtra repository, IStorageAlgorithm algorithm)
        : base(place, name, repository, algorithm)
    {
        Repository = repository;
    }

    public new IRepositoryExtra Repository { get; }

    public void CleanRestorePoints(ICleanUpAlgorithm algorithm, int n, DateTime date)
    {
        IEnumerable<RestorePoint> points = algorithm.SelectPointsToClear(Backup, n, date);
        foreach (var point in points)
        {
            Repository.DeleteRestorePoint(Backup, point);
        }
    }

    public void MergeRestorePoints(RestorePoint oldPoint, RestorePoint newPoint)
    {
        foreach (var oldStorage in oldPoint.Storages)
        {
            if (!newPoint.Storages.Contains(oldStorage))
                Repository.AddStorage(newPoint, oldStorage);
        }

        Repository.DeleteRestorePoint(Backup, oldPoint);
    }

    public List<BackupObject> Recovery(IRepositoryExtra repository, RestorePoint point)
    {
        return repository.Recovery(point);
    }

    public List<BackupObject> Recovery(IRepositoryExtra repository, RestorePoint point, string path)
    {
        return repository.Recovery(point, path);
    }
}