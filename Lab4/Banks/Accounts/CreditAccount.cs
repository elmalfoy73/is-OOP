namespace Banks.Accounts;

public class CreditAccount : IBankAccount
{
    private List<Transaction> _transactions;
    public CreditAccount(string name, Client client, decimal balance)
    {
        Name = name;
        Client = client;
        Balance = balance;
        Fees = 0;
        _transactions = new ();
    }

    public string Name { get; }
    public Client Client { get; }
    public decimal Balance { get; private set; }
    public decimal Fees { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public void Recive(decimal sum)
    {
        Balance += sum;
        CheckFees();
        _transactions.Add(new Transaction(sum, DateTime.Now));
    }

    public void Withdraw(decimal sum)
    {
        if (Client.IsSuspicious & sum > Client.Bank.Limit)
            throw new BankException("Limit exceeded");
        if (Balance - sum < -Client.CreditLimit)
            throw new BankException("insufficient funds");
        Balance -= sum;
        CheckFees();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(string phone, decimal sum)
    {
        if (Client.IsSuspicious & sum > Client.Bank.Limit)
            throw new BankException("Limit exceeded");
        if (Balance - sum < -Client.CreditLimit)
            throw new BankException("insufficient funds");
        Client.Bank.Send(phone, sum);
        Balance -= sum;
        CheckFees();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(Bank bank, string phone, decimal sum)
    {
        if (Client.IsSuspicious & sum > Client.Bank.Limit)
            throw new BankException("Limit exceeded");
        if (Balance - sum < -Client.CreditLimit)
            throw new BankException("insufficient funds");
        bank.CentralBank.SBP(bank, phone, sum);
        Balance -= sum;
        CheckFees();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void Send(IBankAccount account, decimal sum)
    {
        if (Client.IsSuspicious & sum > Client.Bank.Limit)
            throw new BankException("Limit exceeded");
        if (Balance - sum < -Client.CreditLimit)
            throw new BankException("insufficient funds");
        account.Recive(sum);
        Balance -= sum;
        CheckFees();
        _transactions.Add(new Transaction(-sum, DateTime.Now));
    }

    public void CheckFees()
    {
        if (Balance < 0)
            Fees += Client.Bank.Fees;
    }

    public void NextMonth()
    {
        Balance -= Fees;
        Fees = 0;
    }
}