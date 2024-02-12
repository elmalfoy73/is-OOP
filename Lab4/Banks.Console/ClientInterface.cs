using Spectre.Console;

namespace Banks.Console;

public static class ClientInterface
{
    public static string ChooseAction(Client client)
    {
        List<string> actions = new ()
        {
            "Create new account", "Balance", "New transaction", "Transactions history", "Close deposit", "Next Month",
        };

        if (client.IsSuspicious)
        {
            actions.Add("Add address and passport");
        }

        string action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose [green]action[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
                .AddChoices(actions));
        return action;
    }

    public static void Action(Client client)
    {
        string action = ClientInterface.ChooseAction(client);
        switch (action)
        {
            case "Create new account":
                CreateAccount(client);
                break;
            case "Balance":
                AccountInterface.Balance(client);
                break;
            case "New transaction":
                AccountInterface.Transaction(client);
                break;
            case "Transactions history":
                AccountInterface.TransactionHistoru(client);
                break;
            case "Close deposit":
                AccountInterface.CloseDeposit(client);
                break;
            case "Next Month":
                BankInterface.NextMonth(client);
                break;
            case "Add address and passport":
                AddAddressPassport(client);
                break;
        }
    }

    public static void AddAddressPassport(Client client)
    {
        string address = AnsiConsole.Prompt(
            new TextPrompt<string>("[grey][[Optional]][/] [green]Address[/]")
                .AllowEmpty());
        if (!string.IsNullOrWhiteSpace(address))
        {
            client.AddAddress(address);
        }

        int passportSeries = AnsiConsole.Prompt(
            new TextPrompt<int>("[grey][[Optional, type 0 for skipping]][/] [green]Passport series[/]")
                .AllowEmpty());
        int passportNo = AnsiConsole.Prompt(
            new TextPrompt<int>("[grey][[Optional, type 0 for skipping]][/] [green]Passport number[/]")
                .AllowEmpty());
        if (passportSeries != 0 & passportNo != 0)
        {
            client.AddPassport(passportSeries, passportNo);
        }
    }

    public static void CreateAccount(Client client)
    {
        string accountType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose [green]account type[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
                .AddChoices(new[]
                {
                    "Debit", "Credit", "Deposit",
                }));
        string name = AnsiConsole.Prompt(new TextPrompt<string>("[green]Account name[/]"));
        decimal startBalance = AnsiConsole.Prompt(new TextPrompt<decimal>("[green]Start Balance[/]"));
        client.CreateAccount(accountType, name, startBalance);
    }
}