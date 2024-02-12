namespace Banks;

public class CentralBank
{
    private List<Bank> _banks;

    public CentralBank()
    {
        _banks = new ();
    }

    public IReadOnlyCollection<Bank> Banks => _banks.AsReadOnly();
    public Bank CreateBank(string name, decimal fees, decimal interestOnAccount, decimal limit)
    {
        if (_banks.FirstOrDefault(b => b.Name == name) != null)
            throw new Exception("Bank's name isn't unique");
        Bank bank = new (name, fees, interestOnAccount, limit, this);
        _banks.Add(bank);
        return bank;
    }

    public Client? FindClient(Bank bank, string phone)
    {
        return bank.Clients.SingleOrDefault(c => c.Phone == phone);
    }

    public void SBP(Bank bank, string phone, decimal sum)
    {
        Client client = FindClient(bank, phone) ?? throw new BankException("Client isn't exist");
        client.DefaultAccount.Recive(sum);
    }

    public void NextMonth()
    {
        _banks.ForEach(b => b.NextMonth());
    }
}