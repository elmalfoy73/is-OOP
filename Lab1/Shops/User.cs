namespace Shops;

public class User
{
    public User(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
        Spent = 0;
    }

    public string Name { get; }

    public decimal Balance { get; private set; }

    public decimal Spent { get; private set; }

    public void ReduceBalance(decimal cost)
    {
        Balance -= cost;
        Spent += cost;
    }
}