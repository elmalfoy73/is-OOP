using System.ComponentModel.DataAnnotations;

namespace Shops;

public class ProductStats
{
    public ProductStats(decimal price, uint amount)
    {
        if (price > 0)
            Price = price;
        Amount = amount;
    }

    [Required]
    public decimal Price { get; }

    [Required]
    public uint Amount { get; }
}