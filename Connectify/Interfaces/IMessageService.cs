using Connectify.Models;

namespace Connectify.Interfaces;

public interface IMessageService
{
    ValueTask<Message> Create(Message message);
    ValueTask<bool> Delete(int id);
    ValueTask<Message> Get(int id);
    ValueTask<List<Message>> GetAllByUserId(int userId);
    ValueTask<Message> Update(int id, Message message);
}