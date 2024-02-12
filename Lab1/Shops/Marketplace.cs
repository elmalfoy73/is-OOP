using System.Security.AccessControl;

namespace Shops;

public class Marketplace
{
    private List<Shop> _shops = new List<Shop>();

    public Shop AddShop(string name, string address)
    {
        Shop shop = new (name, address);
        _shops.Add(shop);
        return shop;
    }

    public User AddUser(string name, int balance)
    {
        User user = new (name, balance);
        return user;
    }

    public void PriceChange(Shop shop, Product product, decimal newPrice)
    {
        if (shop.Products.ContainsKey(product))
        {
            shop.PriceChange(product, newPrice);
        }
    }

    public void DeliveryToShop(Shop shop, List<SupplyRequest> deliveryList)
    {
        foreach (var product in deliveryList)
        {
            if (shop.Products.ContainsKey(product.Product))
            {
                shop.IncreaseAmount(product.Product, product.ProductStats.Amount);
            }
            else
            {
                shop.AddProductToShop(product.Product, product.ProductStats);
            }
        }
    }

    public void Buy(User user, Shop shop, Product product, uint count)
    {
        decimal price = shop.Products[product].Price;
        decimal cost = price * count;
        user.ReduceBalance(cost);
        shop.Buy(product, count, price);
    }

    public decimal Total(Shop shop, List<PurchaseRequest> shoppingList)
    {
        decimal sum = 0;
        foreach (var product in shoppingList)
        {
            try
            {
                if (shop.Products.ContainsKey(product.Product) & (shop.Products[product.Product].Amount >= product.Count))
                {
                    sum += shop.Products[product.Product].Price * product.Count;
                }
            }
            catch
            {
                return 0;
            }
        }

        return sum;
    }

    public Shop FindBestShop(List<PurchaseRequest> shoppingList)
    {
        Shop bestShop = null;
        decimal minSum = decimal.MaxValue;

        foreach (var shop in _shops)
        {
            decimal sum = Total(shop, shoppingList);
            if ((sum != 0) & (sum < minSum))
            {
                minSum = sum;
                bestShop = shop;
            }
        }

        return bestShop;
    }
}