using Xunit;

namespace Banks.Test;

public class BanksTest
{
    private CentralBank _cb = new ();

    [Fact]
    public void CreateClient()
    {
        Bank tink = _cb.CreateBank("Tinkoff", 100, 3.65M, 50000);
        ClientBuilder builder = new (tink);
        builder.AddFullName("Трегубович Елизавета").AddPhone("+79215504851");
        Client client = tink.CreateClient(builder);
        decimal startBalance = 10000;
        client.DefaultAccount.Recive(startBalance);

        Assert.Contains(tink, _cb.Banks);
        Assert.Contains(client, tink.Clients);
        Assert.Equal(startBalance, client.DefaultAccount.Balance);
    }

    [Fact]
    public void SBP()
    {
        Bank tink = _cb.CreateBank("Tinkoff", 100, 3.65M, 50000);
        ClientBuilder builder = new (tink);
        builder.AddFullName("Трегубович Елизавета").AddPhone("+79215504851");
        Client client = tink.CreateClient(builder);
        decimal startBalance = 10000;
        client.DefaultAccount.Recive(startBalance);

        Bank sber = _cb.CreateBank("Sber", 100, 3.65M, 50000);
        ClientBuilder builder1 = new (sber);
        builder1.AddFullName("Мастер по ноготочкам").AddPhone("+79*********");
        Client client1 = sber.CreateClient(builder1);

        decimal sum = 2000;
        client.DefaultAccount.Send(sber, client1.Phone, sum);

        Assert.Equal(startBalance - sum, client.DefaultAccount.Balance);
        Assert.Equal(sum, client1.DefaultAccount.Balance);
        Assert.Equal(2, client.DefaultAccount.Transactions.Count);
        Assert.Equal(1, client1.DefaultAccount.Transactions.Count);
    }

    [Fact]
    public void InterestOnAccount()
    {
        Bank tink = _cb.CreateBank("Tinkoff", 100, 3.65M, 50000);
        ClientBuilder builder = new (tink);
        builder.AddFullName("Трегубович Елизавета").AddPhone("+79215504851");
        Client client = tink.CreateClient(builder);
        client.AddAddress("x");
        client.AddPassport(1234, 123456);

        client.DefaultAccount.Recive(100000);
        client.DefaultAccount.Recive(100000);
        client.DefaultAccount.Withdraw(150000);
        Assert.Equal(35, client.DefaultAccount.InterestOnAccount);
        Assert.Equal(50000, client.DefaultAccount.Balance);

        decimal interestOnAccount = client.DefaultAccount.InterestOnAccount;
        decimal endMonthBalance = client.DefaultAccount.Balance;
        _cb.NextMonth();
        Assert.Equal(endMonthBalance + interestOnAccount, client.DefaultAccount.Balance);
    }
}