using Banks.Accounts;
using Spectre.Console;

namespace Banks.Console;

public static class AccountInterface
{
    public static List<string> GetAccountsNames(Client client)
    {
        List<string> names = new ();
        foreach (var account in client.Accounts)
        {
            names.Add(account.Name);
        }

        return names;
    }

    public static IBankAccount? ChooseAccount(Client client)
    {
        var bank = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose [green]account[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more accounts)[/]")
                .AddChoices(GetAccountsNames(client)));

        AnsiConsole.WriteLine($"{bank}");
        AnsiConsole.WriteLine();
        return client.Accounts.SingleOrDefault(b => b.Name == bank);
    }

    public static void TransactionHistoru(Client client)
    {
        IBankAccount account = ChooseAccount(client) ?? throw new BankException("Account isn't exist");
        var rows = new List<Text>();
        foreach (var t in account.Transactions)
        {
            rows.Add(new Text(string.Concat(t.Sum.ToString(), " ", t.Date)));
        }

        AnsiConsole.Write(new Rows(rows));
    }

    public static void Balance(Client client)
    {
        IBankAccount account = ChooseAccount(client) ?? throw new BankException("Account isn't exist");
        AnsiConsole.MarkupLineInterpolated($"Balance: [purple] {account.Balance} [/]");
    }

    public static string ChooseTransaction()
    {
        string transaction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose [green]transaction[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
                .AddChoices(new[]
                {
                    "Deposit", "Withdraw", "Send",
                }));
        return transaction;
    }

    public static void Transaction(Client client)
    {
        IBankAccount account = ChooseAccount(client) ?? throw new BankException("Account isn't exist");
        string transaction = ChooseTransaction();
        switch (transaction)
        {
            case "Deposit":
                Deposit(account);
                break;
            case "Withdraw":
                Withdraw(account);
                break;
            case "Send":
                Send(account);
                break;
        }
    }

    public static void Deposit(IBankAccount account)
    {
        decimal sum = AnsiConsole.Ask<decimal>("[green]Sum[/]");
        account.Recive(sum);
        AnsiConsole.MarkupLineInterpolated($"[purple] {sum} [/]");
    }

    public static void Withdraw(IBankAccount account)
    {
        decimal sum = AnsiConsole.Ask<decimal>("[green]Sum[/]");
        account.Withdraw(sum);
        AnsiConsole.MarkupLineInterpolated($"[purple] {-sum} [/]");
    }

    public static void Send(IBankAccount account)
    {
        string isSBP = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Send with SBP?[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Yes", "No",
                }));
        string phone = AnsiConsole.Ask<string>("[green]Phone[/]");
        decimal sum = AnsiConsole.Ask<decimal>("[green]Sum[/]");
        if (isSBP == "Yes")
        {
            Bank bank = BankInterface.ChooseBank(account.Client.Bank.CentralBank) ?? throw new BankException("Bank isn't exist");
            account.Send(bank, phone, sum);
        }
        else
        {
            account.Send(phone, sum);
        }

        AnsiConsole.MarkupLineInterpolated($"[purple] {-sum} [/]");
    }

    public static void CloseDeposit(Client client)
    {
        DepositAccount account = (DepositAccount)(ChooseAccount(client) ?? throw new BankException("Account isn't exist"));
        account.CloseDeposit();
    }
}