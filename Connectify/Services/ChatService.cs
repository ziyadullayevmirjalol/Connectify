using Connectify.Interfaces;
using Connectify.Models;
using Newtonsoft.Json;
namespace Connectify.Services;

public class ChatService : IChatService
{
    UserService userService;
    public ChatService(UserService userService)
    {
        this.userService = userService;
    }
    public async ValueTask<Chat> Create(Chat chat)
    {
        var data = File.ReadAllText(Constants.CHATS_PATH);
        var Chats = JsonConvert.DeserializeObject<List<Chat>>(data) ?? new List<Chat>();

        var user1 = userService.Get(chat.User1_id);
        if (user1 == null)
            throw new Exception("User 1 is not found");

        var user2 = userService.Get(chat.User2_id);
        if (user2 == null)
            throw new Exception("User 2 is not found");

        if (Chats.Count == 0)
        {
            chat.Id = 1;
        }
        else
        {
            var lastChat = Chats.Last();
            chat.Id = lastChat.Id + 1;
        }

        Chats.Add(chat);

        var res = JsonConvert.SerializeObject(Chats, Formatting.Indented);
        File.WriteAllText(Constants.CHATS_PATH, res);

        return chat;
    }
    public async ValueTask<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
    public async ValueTask<Chat> Get(int id)
    {
        var data = File.ReadAllText(Constants.CHATS_PATH);
        var chats = JsonConvert.DeserializeObject<List<Chat>>(data) ?? new List<Chat>();

        var chat = chats.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception($"Chat is not found with id {id}");

        return chat;
    }
    public async ValueTask<List<Chat>> GetByUserId(int userId)
    {
        var data = File.ReadAllText(Constants.CHATS_PATH);
        var chats = JsonConvert.DeserializeObject<List<Chat>>(data) ?? new List<Chat>();

        var userchats = chats.Where(item => item.User1_id == userId && item.User2_id == userId).ToList();
        userchats = userchats.Distinct(new ChatDuplicatesComparer()).ToList();

        return userchats;
    }
    public async ValueTask<List<Chat>> GetAll()
    {
        var data = File.ReadAllText(Constants.CHATS_PATH);
        var chats = JsonConvert.DeserializeObject<List<Chat>>(data) ?? new List<Chat>();

        return chats;
    }

}
