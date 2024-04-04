using Connectify.Models;

namespace Connectify.Interfaces;

public interface IChatService
{
    ValueTask<Chat> Create(Chat chat);
    ValueTask<bool> Delete(int id);
    ValueTask<Chat> Get(int id);
    ValueTask<List<Chat>> GetByUserId(int userId);
    ValueTask<List<Chat>> GetAll();
}
