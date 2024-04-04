using Connectify.Interfaces;
using Connectify.Models;
using Newtonsoft.Json;

namespace Connectify.Services;


public class UserService : IUserService
{
    public async ValueTask<User> Create(User user)
    {
        var data = File.ReadAllText(Constants.USERS_PATH);
        var users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();

        if (users.Count == 0)
        {
            user.Id = 1;
        }
        else
        {
            var lastWord = users.Last();
            user.Id = lastWord.Id + 1;
        }

        users.Add(user);

        var res = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(Constants.USERS_PATH, res);

        return user;
    }

    public async ValueTask<bool> Delete(int id)
    {
        var data = File.ReadAllText(Constants.USERS_PATH);
        var users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();

        var user = users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User is not found with id {id}");

        users.Remove(user);

        data = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(Constants.USERS_PATH, data);

        return true;
    }

    public async ValueTask<User> Get(int id)
    {
        var data = File.ReadAllText(Constants.USERS_PATH);
        var users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();

        var user = users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User is not found with id {id}");

        return user;
    }

    public async ValueTask<List<User>> GetAll()
    {
        var data = File.ReadAllText(Constants.USERS_PATH);
        var users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();

        return users;
    }

    public async ValueTask<User> Update(int id, User user)
    {
        var data = File.ReadAllText(Constants.USERS_PATH);
        var users = JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();

        var existUser = users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User is not found with id {id}");

        existUser.Firstname = user.Firstname;
        existUser.Lastname = user.Lastname;

        data = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(Constants.USERS_PATH, data);

        return existUser;
    }
}
