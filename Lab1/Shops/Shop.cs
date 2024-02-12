using System.ComponentModel.DataAnnotations;

namespace Shops;

public class Shop
{
    private Dictionary<Product, ProductStats> _products;

    public Shop(string name, string address)
    {
        _products = new Dictionary<Product, ProductStats>();
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        Balance = 0;
    }

    public Guid Id { get; }

    [Required]
    public string Name { get; }

    [Required]
    public string Address { get; }

    public decimal Balance { get; private set; }

    public IReadOnlyDictionary<Product, ProductStats> Products => _products;

    public void AddProductToShop(Product product, ProductStats productStats)
    {
        if (product != null)
            _products.Add(product, productStats);
    }

    public void PriceChange(Product product, decimal newPrice)
    {
        if (_products.ContainsKey(product))
        {
            uint amount = _products[product].Amount;
            _products[product] = new ProductStats(newPrice, amount);
        }
    }

    public void IncreaseAmount(Product product, uint count)
    {
        if (_products.ContainsKey(product))
        {
            decimal price = _products[product].Price;
            _products[product] = new ProductStats(price, count);
        }
    }

    public void Buy(Product product, uint count, decimal price)
    {
        if (_products.ContainsKey(product))
        {
            uint amount = _products[product].Amount - count;
            _products[product] = new ProductStats(price, amount);
            Balance += price * count;
        }
    }
}