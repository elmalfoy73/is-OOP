using System.ComponentModel.DataAnnotations;

namespace Banks;

public class ClientBuilder
{
    public ClientBuilder(Bank bank)
    {
        Bank = bank;
    }

    public Bank Bank { get; }
    public string? FullName { get; private set; }
    [Phone]
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public Passport? Passport { get; private set; }
    public decimal CreditLimit { get; private set; }

    public ClientBuilder AddFullName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BankException("Client's name is null or white space");
        FullName = fullName;
        return this;
    }

    public ClientBuilder AddPhone(string phone)
    {
        Phone = phone;
        return this;
    }

    public ClientBuilder AddAddress(string? address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BankException("Client's address is null or white space");
        Address = address;
        return this;
    }

    public ClientBuilder AddPassport(int series, int no)
    {
        Passport = new (series, no);
        return this;
    }

    public ClientBuilder AddCreditLimit(decimal limit)
    {
        CreditLimit = limit;
        return this;
    }

    public Client Build()
    {
        Client? client = null;
        if (ValidateClient())
            client = new Client(this);
        else
            throw new BankException("Fullname and phone are required");
        return client;
    }

    public bool ValidateClient()
    {
        return !string.IsNullOrWhiteSpace(FullName) & Phone != null;
    }
}