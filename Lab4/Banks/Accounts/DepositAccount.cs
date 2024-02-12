namespace Banks.Accounts;

public class DepositAccount : IBankAccount
{
    private List<Transaction> _transactions;

    public DepositAccount(string name, Client client, decimal balance)
    {
        Name = name;
        Client = client;
        Balance = balance;
        InterestOnAccount = 0;
        _transactions = new ();
    }

    public string Name { get; }
    public Client Client { get; }
    public decimal Balance { get; private set; }
    public decimal InterestOnAccount { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public void Recive(decimal sum)
    {
        Balance += sum;
        AddInterestOnAccount();
        _transactions.Add(new Transaction(sum, DateTime.Now));
    }

    public void Withdraw(decimal sum)
    {
        throw new Exception("You can't withdraw money from the deposit account");
    }

    public void Send(string phone, decimal sum)
    {
        throw new Exception("You can't withdraw money from the deposit account");
    }

    public void Send(Bank bank, string phone, decimal sum)
    {
        throw new Exception("You can't withdraw money from the deposit account");
    }

    public void Send(IBankAccount account, decimal sum)
    {
        throw new Exception("You can't withdraw money from the deposit account");
    }

    public void AddInterestOnAccount()
    {
        decimal perDay = Client.Bank.InterestOnAccount / 100 / 365;
        InterestOnAccount += Balance * perDay;
    }

    public void NextMonth()
    {
        Balance += InterestOnAccount;
        InterestOnAccount = 0;
    }

    public void CloseDeposit()
    {
        Client.RemoveAccount(this);
        Client.DefaultAccount.Recive(Balance);
        Balance = 0;
    }
}