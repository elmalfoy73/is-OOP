using System.ComponentModel;
using Shops;
using Xunit;

namespace Shops.Test;

public class MarketPlaceTests
{
    private Marketplace _marketplace = new ();

    [Fact]
    public void ProductsDelivery()
    {
        _marketplace = new Marketplace();

        Shop shop = _marketplace.AddShop("shop1", "x");
        Product product1 = new Product("1");
        Product product2 = new Product("2");
        Product product3 = new Product("3");
        var deliveryList = new List<SupplyRequest>()
        {
            new SupplyRequest(product1, new ProductStats(1000, 10)),
            new SupplyRequest(product2, new ProductStats(1000, 10)),
            new SupplyRequest(product3, new ProductStats(1000, 10)),
        };
        _marketplace.DeliveryToShop(shop, deliveryList);

        Assert.True(shop.Products.ContainsKey(product1));
        Assert.True(shop.Products.ContainsKey(product2));
        Assert.True(shop.Products.ContainsKey(product3));
    }

    [Fact]
    public void PriceChange()
    {
        _marketplace = new Marketplace();

        Shop shop = _marketplace.AddShop("shop1", "x");
        Product product = new Product("3");
        var deliveryList = new List<SupplyRequest>()
        {
            new SupplyRequest(product, new ProductStats(1000, 10)),
        };
        _marketplace.DeliveryToShop(shop, deliveryList);
        decimal newPrice = 10000;
        _marketplace.PriceChange(shop, product, newPrice);

        Assert.Equal(newPrice, shop.Products[product].Price);
    }

    [Fact]
    public void FindBestShop()
    {
        _marketplace = new Marketplace();

        Shop shop1 = _marketplace.AddShop("shop1", "Rublyovka 1");
        Shop shop2 = _marketplace.AddShop("shop2", "Rublyovka 2");

        Product goldenToilet = new Product("goldenToilet");
        Product goldenBath = new Product("goldenBath");
        Product goldenSink = new Product("goldenSink");

        var deliveryList1 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(1000000, 100)),
            new SupplyRequest(goldenBath, new ProductStats(1000000, 100)),
            new SupplyRequest(goldenSink, new ProductStats(1000000, 100)),
        };

        var deliveryList2 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(2000000, 100)),
            new SupplyRequest(goldenBath, new ProductStats(2000000, 100)),
            new SupplyRequest(goldenSink, new ProductStats(500000, 100)),
        };

        _marketplace.DeliveryToShop(shop1, deliveryList1);
        _marketplace.DeliveryToShop(shop1, deliveryList2);

        var shoppingList = new List<PurchaseRequest>()
        {
            new PurchaseRequest(goldenToilet, 2),
            new PurchaseRequest(goldenBath, 1),
            new PurchaseRequest(goldenSink, 2),
        };
        Shop? bestShop = _marketplace.FindBestShop(shoppingList);

        Assert.Equal(shop1, bestShop);
    }

    [Fact]
    public void FindBestShopEveryWhereIsNotEnough()
    {
        _marketplace = new Marketplace();
        Shop shop1 = _marketplace.AddShop("shop1", "Rublyovka 1");
        Shop shop2 = _marketplace.AddShop("shop2", "Rublyovka 2");

        Product goldenToilet = new Product("goldenToilet");
        Product goldenBath = new Product("goldenBath");
        Product goldenSink = new Product("goldenSink");

        var deliveryList1 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(1000000, 1)),
            new SupplyRequest(goldenBath, new ProductStats(1000000, 1)),
            new SupplyRequest(goldenSink, new ProductStats(1000000, 1)),
        };

        var deliveryList2 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(2000000, 1)),
            new SupplyRequest(goldenBath, new ProductStats(2000000, 1)),
            new SupplyRequest(goldenSink, new ProductStats(500000, 1)),
        };

        _marketplace.DeliveryToShop(shop1, deliveryList1);
        _marketplace.DeliveryToShop(shop1, deliveryList2);

        var shoppingList = new List<PurchaseRequest>()
        {
            new PurchaseRequest(goldenToilet, 2),
            new PurchaseRequest(goldenBath, 2),
            new PurchaseRequest(goldenSink, 2),
        };
        Shop? bestShop = _marketplace.FindBestShop(shoppingList);

        Assert.Null(bestShop);
    }

    [Fact]
    public void FindBestShopProductDoesNotExist()
    {
        _marketplace = new Marketplace();
        Shop shop1 = _marketplace.AddShop("shop1", "Rublyovka 1");
        Shop shop2 = _marketplace.AddShop("shop2", "Rublyovka 2");

        Product goldenToilet = new Product("goldenToilet");
        Product goldenBath = new Product("goldenBath");
        Product goldenSink = new Product("goldenSink");

        var deliveryList1 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(1000000, 1)),
        };

        var deliveryList2 = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(2000000, 1)),
        };

        _marketplace.DeliveryToShop(shop1, deliveryList1);
        _marketplace.DeliveryToShop(shop1, deliveryList2);

        var shoppingList = new List<PurchaseRequest>()
        {
            new PurchaseRequest(goldenToilet, 2),
            new PurchaseRequest(goldenBath, 2),
            new PurchaseRequest(goldenSink, 2),
        };
        Shop? bestShop = _marketplace.FindBestShop(shoppingList);

        Assert.Null(bestShop);
    }

    [Fact]
    public void WholesalePurchase()
    {
        _marketplace = new Marketplace();

        Shop shop = _marketplace.AddShop("shop1", "x");
        Product goldenToilet = new Product("goldenToilet");
        Product goldenBath = new Product("goldenBath");
        Product goldenSink = new Product("goldenSink");

        uint amount = 100;
        var deliveryList = new List<SupplyRequest>()
        {
            new SupplyRequest(goldenToilet, new ProductStats(1, amount)),
            new SupplyRequest(goldenBath, new ProductStats(10, amount)),
            new SupplyRequest(goldenSink, new ProductStats(100, amount)),
        };
        _marketplace.DeliveryToShop(shop, deliveryList);

        var shoppingList = new List<PurchaseRequest>()
        {
            new PurchaseRequest(goldenToilet, 2),
            new PurchaseRequest(goldenBath, 2),
            new PurchaseRequest(goldenSink, 2),
        };

        User user = new ("Edgar", 10000000);
        foreach (var product in shoppingList)
        {
            _marketplace.Buy(user, shop, product.Product, product.Count);
        }

        Assert.Equal(user.Spent, shop.Balance);
        Assert.Equal(amount - shoppingList[0].Count, shop.Products[goldenToilet].Amount);
    }
}