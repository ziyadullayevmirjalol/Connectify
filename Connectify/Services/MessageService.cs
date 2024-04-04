using Connectify.Interfaces;
using Connectify.Models;
using Newtonsoft.Json;

namespace Connectify.Services;

public class MessageService : IMessageService
{
    private ChatService chatService;
    private UserService userService;
    public MessageService(UserService userService, ChatService chatService)
    {
        this.chatService = chatService;
        this.userService = userService;
    }
    public async ValueTask<Message> Create(Message message)
    {
        var user = await userService.Get(message.UserID);
        if (user == null)
        {
            throw new Exception("User is not found");
        }
        var chat = await chatService.Get(message.ChatId);
        if (chat == null)
        {
            throw new Exception("Chat is not found");
        }

        var data =  File.ReadAllText(Constants.MESSAGES_PATH);
        var Messages = JsonConvert.DeserializeObject<List<Message>>(data) ?? new List<Message>();

        if (Messages.Count == 0)
        {
            message.Id = 1;
        }
        else
        {
            var lastMessage = Messages.Last();
            message.Id = lastMessage.Id+1;
        }

        Messages.Add(message);

        var res = JsonConvert.SerializeObject(Messages, Formatting.Indented);
        File.WriteAllText(Constants.MESSAGES_PATH, res);

        return message;
    }
    public async ValueTask<bool> Delete(int id)
    {
        return false;
    }
    public async ValueTask<Message> Get(int id)
    {
        throw new NotImplementedException();
    }   
    public async ValueTask<List<Message>> GetAllByUserId(int userId)
    {
        var data = File.ReadAllText(Constants.MESSAGES_PATH);
        var Messages = JsonConvert.DeserializeObject<List<Message>>(data) ?? new List<Message>();
        
        List<Message> result = new List<Message>();
        foreach ( var message in Messages)
        {
            if (message.UserID == userId)
            {
                result.Add(message);
            }
        }
        return result;
    }
    public async ValueTask<Message> Update(int id, Message message)
    {
        return null;
    }
}
