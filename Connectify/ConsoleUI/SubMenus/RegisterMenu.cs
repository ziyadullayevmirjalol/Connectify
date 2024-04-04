using Connectify.ConsoleUI.SubMenus.InnerMenu;
using Connectify.Models;
using Connectify.Services;
using Spectre.Console;

namespace Connectify.ConsoleUI.SubMenus;

public class RegisterMenu
{
    private UserService userService;
    private PostMenu postMenu;
    public RegisterMenu(UserService userService)
    {
        this.userService = userService;
    }
    public async Task Register()
    {
        Console.Clear();

        var firstname = AnsiConsole.Ask<string>("Enter your [green]Firstname[/]:");
        var lastname = AnsiConsole.Ask<string>("Enter your [green]Lastname[/]:");

        User createUser = new User { Firstname = firstname, Lastname = lastname };
        var created = await userService.Create(createUser);

        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Successfully created!");

        var user = await userService.Get(created.Id);

        Thread.Sleep(1000);
        AnsiConsole.Status()
        .Start("Login...", ctx =>
        {

            ctx.Status("loading data...");
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));

            AnsiConsole.MarkupLine("In process...");
            Thread.Sleep(2000);
        });
        Console.Clear();
        postMenu = new PostMenu(user);
        postMenu.Display();
    }
}
