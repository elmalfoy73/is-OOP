using System.Collections;
using Banks.Accounts;

namespace Banks;

public class Client
{
    private List<IBankAccount> _accounts;

    public Client(ClientBuilder clientBuilder)
    {
        Bank = clientBuilder.Bank;
        FullName = clientBuilder.FullName ?? throw new BankException("Fullname are required");
        Phone = clientBuilder.Phone ?? throw new BankException("Phone are required");
        Address = clientBuilder.Address;
        Passport = clientBuilder.Passport;
        CreditLimit = clientBuilder.CreditLimit;
        IsSuspicious = true;
        if (Address != null & Passport != null)
            IsSuspicious = false;
        DefaultAccount = new DebitAccount("default", this, 0);
        _accounts = new List<IBankAccount> { DefaultAccount };
    }

    public Bank Bank { get; }
    public string FullName { get; }
    public string Phone { get; }
    public string? Address { get; private set; }
    public Passport? Passport { get; private set; }
    public decimal CreditLimit { get; private set; }
    public bool IsSuspicious { get; private set; }
    public DebitAccount DefaultAccount { get; }
    public IReadOnlyCollection<IBankAccount> Accounts => _accounts.AsReadOnly();

    public void AddAddress(string? address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BankException("Client's address is null or white space");
        Address = address;
        if (Passport != null)
            IsSuspicious = false;
    }

    public void AddPassport(int series, int no)
    {
        Passport = new (series, no);
        if (Address != null)
            IsSuspicious = false;
    }

    public void AddCreditLimit(decimal limit)
    {
        CreditLimit = limit;
    }

    public IBankAccount CreateAccount(string type, string name, decimal startBalance)
    {
        IBankAccount account;
        switch (type)
        {
            case "Debit":
                account = new DebitAccount(name, this, startBalance);
                break;
            case "Credit":
                account = new CreditAccount(name, this, startBalance);
                break;
            case "Deposit":
                account = new CreditAccount(name, this, startBalance);
                break;
            default:
                throw new BankException("This type of account isn't exist");
        }

        _accounts.Add(account);
        return account;
    }

    public void RemoveAccount(IBankAccount account)
    {
        _accounts.Remove(account);
    }
}