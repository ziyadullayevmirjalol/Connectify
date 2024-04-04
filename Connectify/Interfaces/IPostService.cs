using Connectify.Models;

namespace Connectify.Interfaces;

public interface IPostService
{
    ValueTask<Post> Create(Post post);
    ValueTask<bool> Delete(int id);
    ValueTask<Post> Get(int id);
    ValueTask<List<Post>> GetAll();
    ValueTask<List<Post>> GetAllByAuthorId(int id);
    ValueTask<Post> Update(int id, int userId, Post post);
    ValueTask<bool> IncViewsCount(int id);
}
