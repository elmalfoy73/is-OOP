namespace Banks;

public class Bank
{
    private List<Client> _clients;

    public Bank(string name, decimal fees, decimal interestOnAccount, decimal limit, CentralBank centralBank)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BankException("Bank's name is null or white space");
        Name = name;
        Fees = fees;
        InterestOnAccount = interestOnAccount;
        Limit = limit;
        CentralBank = centralBank;
        _clients = new ();
    }

    public string Name { get; }
    public decimal Fees { get; private set; }
    public decimal InterestOnAccount { get; private set; }
    public decimal Limit { get; private set; }
    public CentralBank CentralBank { get; }
    public IReadOnlyCollection<Client> Clients => _clients.AsReadOnly();

    public Client CreateClient(ClientBuilder builder)
    {
        if (_clients.FirstOrDefault(c => c.Phone == builder.Phone) != null)
            throw new Exception("Client's phone isn't unique");
        Client client = builder.Build();
        _clients.Add(client);
        return client;
    }

    public void Send(string phone, decimal sum)
    {
        Client client = _clients.FirstOrDefault(c => c.Phone == phone) ?? throw new BankException("client is null");
        client.DefaultAccount.Recive(sum);
    }

    public void ChangeLimit(decimal limit)
    {
        Limit = limit;
    }

    public void ChangeInterestOnAccount(decimal interestOnAccount)
    {
        InterestOnAccount = interestOnAccount;
    }

    public void ChangeFees(decimal fees)
    {
        Fees = fees;
    }

    public void NextMonth()
    {
        foreach (var client in _clients)
        {
            foreach (var account in client.Accounts)
            {
                account.NextMonth();
            }
        }
    }
}