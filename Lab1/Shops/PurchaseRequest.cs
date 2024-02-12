namespace Shops;

public class PurchaseRequest
{
    public PurchaseRequest(Product product, uint count)
    {
        Product = product;
        Count = count;
    }

    public Product Product { get;  }
    public uint Count { get;  }
}