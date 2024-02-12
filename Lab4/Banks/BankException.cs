namespace Banks;

public class BankException : Exception
{
    public BankException(string message)
    : base(message)
    {
    }
}