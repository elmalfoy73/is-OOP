namespace Shops;

public class SupplyRequest
{
    public SupplyRequest(Product product, ProductStats productStats)
    {
        Product = product;
        ProductStats = productStats;
    }

    public Product Product { get;  }
    public ProductStats ProductStats { get;  }
}