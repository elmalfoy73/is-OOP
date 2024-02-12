namespace Backups;

public class BackupObject
{
    public BackupObject(string filePatch)
    {
        FilePatch = filePatch;
        StoragePath = null;
    }

    public string FilePatch { get; }
    public string? StoragePath { get; set; }

    public void AddStoragePath(string storagePath)
    {
        StoragePath = storagePath;
    }
}