namespace Banks;

public class Passport
{
    public Passport(int series, int no)
    {
        if (series.ToString().Length != 4)
            throw new BankException("Client's passport series is incorrect");
        if (no.ToString().Length != 6)
            throw new BankException("Client's passport № is incorrect");
        Series = series;
        No = no;
    }

    public int Series { get; }
    public int No { get; }
}