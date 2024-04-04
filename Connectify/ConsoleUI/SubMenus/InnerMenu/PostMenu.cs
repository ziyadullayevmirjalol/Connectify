using Connectify.Models;

namespace Connectify.ConsoleUI.SubMenus.InnerMenu;

public class PostMenu
{
	private User user;
	public PostMenu(User user)
	{
		this.user = user;
	}
	public async Task Display()
	{
        await Console.Out.WriteLineAsync("Service is out of work!\n Press any key to exit...");
		Console.ReadLine();
    }
}
