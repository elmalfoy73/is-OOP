using Spectre.Console;

namespace Banks.Console;

public static class BankInterface
{
    public static List<string> GetBanksNames(CentralBank cb)
    {
        List<string> names = new ();
        foreach (var bank in cb.Banks)
        {
            names.Add(bank.Name);
        }

        return names;
    }

    public static Bank? ChooseBank(CentralBank cb)
    {
        var bank = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose [green]bank[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more banks)[/]")
                .AddChoices(GetBanksNames(cb)));

        AnsiConsole.WriteLine($"{bank}");
        AnsiConsole.WriteLine();
        return cb.Banks.SingleOrDefault(b => b.Name == bank);
    }

    public static void NextMonth(Client client)
    {
        client.Bank.CentralBank.NextMonth();
        AnsiConsole.WriteLine("[purple] Accounts statement");
        var rows = new List<Text>();
        foreach (var a in client.Accounts)
        {
            rows.Add(new Text(string.Concat(a.Name, ", balance: ", a.Balance.ToString())));
        }

        AnsiConsole.Write(new Rows(rows));
    }
}