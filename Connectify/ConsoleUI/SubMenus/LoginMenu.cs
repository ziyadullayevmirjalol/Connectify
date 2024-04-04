using Connectify.ConsoleUI.SubMenus.InnerMenu;
using Connectify.Services;
using Spectre.Console;

namespace Connectify.ConsoleUI.SubMenus;

public class LoginMenu
{
    private UserService userService;
    private PostMenu postMenu;
    public LoginMenu(UserService userService)
    {
        this.userService = userService;
    }
    public async Task Login()
    {
        Console.Clear();

    reenter:
        var id = AnsiConsole.Ask<int>("Enter your [green]ID[/]:");

        var user = await userService.Get(id);

        if (user == null)
        {
            AnsiConsole.WriteLine("User is not found");
            goto reenter;
        }
        else
        {
            AnsiConsole.WriteLine("Success\n");
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
}
