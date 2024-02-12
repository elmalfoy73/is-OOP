using System.Net.Sockets;
using Spectre.Console;

namespace Banks.Console;

public static class NewClientInterface
{
    public static bool IsClientNew()
    {
        var isNewClient = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Are you a [green]new client[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[]
                {
                    "Yes", "No",
                }));

        if (isNewClient == "Yes")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Client Login(CentralBank cb, Bank bank)
    {
        string phone = AnsiConsole.Ask<string>("[green]Phone[/]");
        Client client = cb.FindClient(bank, phone) ?? throw new BankException("Check the phone number, there is no user with that phone");
        AnsiConsole.Write(new Markup("[purple]Successful login[/]"));
        AnsiConsole.WriteLine();
        return client;
    }

    public static Client CreateClient(Bank bank)
    {
        ClientBuilder builder = new (bank);
        AnsiConsole.Write(new Markup("[purple]Registration[/]"));
        AnsiConsole.WriteLine();
        string lastName = AnsiConsole.Ask<string>("[green]Last name[/]");
        string name = AnsiConsole.Ask<string>("[green]Name[/]");
        string patronymic = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Patronymic[/]")
                .AllowEmpty());
        builder.AddFullName(string.Concat(lastName, name, patronymic));
        string phone = AnsiConsole.Ask<string>("[green]Phone[/]");
        builder.AddPhone(phone);
        string address = AnsiConsole.Prompt(
            new TextPrompt<string>("[grey][[Optional]][/] [green]Address[/]")
                .AllowEmpty());
        if (!string.IsNullOrWhiteSpace(address))
        {
            builder.AddAddress(address);
        }

        int passportSeries = AnsiConsole.Prompt(
            new TextPrompt<int>("[grey][[Optional, type 0 for skipping]][/] [green]Passport series[/]")
                .AllowEmpty());
        int passportNo = AnsiConsole.Prompt(
            new TextPrompt<int>("[grey][[Optional, type 0 for skipping]][/] [green]Passport number[/]")
                .AllowEmpty());
        if (passportSeries != 0 & passportNo != 0)
        {
            builder.AddPassport(passportSeries, passportNo);
        }

        AnsiConsole.Write(new Markup("[purple]Registration was successful[/]"));
        AnsiConsole.WriteLine();
        return bank.CreateClient(builder);
    }
}