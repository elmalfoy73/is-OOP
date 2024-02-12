namespace Banks.Accounts;

public class DebitAccount : IBankAccount
{
    private List<Transaction> _transactions;
    public DebitAccount(string name, Client client, decimal balance)
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
        if (Client.IsSuspicious & sum > Client.Bank.Limit)
            throw new BankException("Limit exceeded");
        if (Balance - sum < 0)
            throw new BankException("insufficient funds");
        Balance -= sum;
        AddInterestOnAccount();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(string phone, decimal sum)
    {
        if (Client.IsSuspicious & Balance - sum < 0)
            throw new BankException("insufficient funds");
        Client.Bank.Send(phone, sum);
        Balance -= sum;
        AddInterestOnAccount();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(Bank bank, string phone, decimal sum)
    {
        if (Client.IsSuspicious & Balance - sum < 0)
            throw new BankException("insufficient funds");
        bank.CentralBank.SBP(bank, phone, sum);
        Balance -= sum;
        AddInterestOnAccount();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(IBankAccount account, decimal sum)
    {
        if (Client.IsSuspicious & Balance - sum < 0)
            throw new BankException("insufficient funds");
        account.Recive(sum);
        Balance -= sum;
        AddInterestOnAccount();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
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
}