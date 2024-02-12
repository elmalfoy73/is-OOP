namespace Backups;

public class RestorePoint
{
    private List<Storage> _storages;

    public RestorePoint(string place, string name, DateTime date)
    {
        _storages = new List<Storage>();
        Date = date;
        Place = place;
        Name = name;
    }

    public DateTime Date { get; }
    public string Place { get; }
    public string Name { get;  }
    public IReadOnlyCollection<Storage> Storages => _storages.AsReadOnly();

    public void Add(Storage storage)
    {
        _storages.Add(storage);
    }

    public void AddRange(List<Storage> storages)
    {
        _storages.AddRange(storages);
    }

    public void Remove(Storage storage)
    {
        _storages.Remove(storage);
    }
}