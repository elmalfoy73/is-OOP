namespace Banks.Accounts;

public interface IBankAccount
{
    string Name { get;  }
    Client Client { get;  }
    decimal Balance { get;  }
    public IReadOnlyCollection<Transaction> Transactions { get; }

    public void Recive(decimal sum);
    public void Withdraw(decimal sum);
    public void Send(string phone, decimal sum);
    public void Send(Bank bank, string phone, decimal sum);
    public void Send(IBankAccount account, decimal sum);
    public void NextMonth();
}