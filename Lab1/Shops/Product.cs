namespace Shops;

public class Product
{
    public Product(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get; }

    private Guid Id { get; }
}