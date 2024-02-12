namespace Banks;

public class Transaction
{
    public Transaction(decimal sum, DateTime date)
    {
        Sum = sum;
        Date = date;
    }

    public decimal Sum { get; }
    public DateTime Date { get; }
}