using Spectre.Console;

namespace Banks.Console;

public static class Program
{
    private static CentralBank _centralBank = new ();
    public static void CreateBanks()
    {
        Bank tink = _centralBank.CreateBank("Tinkoff", 100, 3.65M, 50000);
        Bank sber = _centralBank.CreateBank("SberBank", 60, 3, 50000);
        Bank alfa = _centralBank.CreateBank("AlfaBank", 150, 5, 50000);
        ClientBuilder builder = new (tink);
        builder.AddFullName("Трегубович Елизавета").AddPhone("+79215504851");
        Client client = tink.CreateClient(builder);
        ClientBuilder builder1 = new (sber);
        builder1.AddFullName("Мастер по ноготочкам").AddPhone("+79*********");
        sber.CreateClient(builder1);
    }

    public static void Main(string[] args)
    {
        CreateBanks();
        Bank bank = BankInterface.ChooseBank(_centralBank) ?? throw new BankException("Bank isn't exist");
        Client client;
        if (NewClientInterface.IsClientNew())
            client = NewClientInterface.CreateClient(bank);
        else
            client = NewClientInterface.Login(_centralBank, bank);

        while (true)
        {
            ClientInterface.Action(client);
        }
    }
}