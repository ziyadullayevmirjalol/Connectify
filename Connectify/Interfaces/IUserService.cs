using Connectify.Models;

namespace Connectify.Interfaces;

public interface IUserService
{
    ValueTask<User> Create(User user);
    ValueTask<User> Update(int id, User user);
    ValueTask<bool> Delete(int id);
    ValueTask<User> Get(int id);
    ValueTask<List<User>> GetAll();
}
