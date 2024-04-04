using Connectify.Interfaces;
using Connectify.Models;
using Newtonsoft.Json;

namespace Connectify.Services;

public class PostService : IPostService
{
    private UserService userService;
    public PostService(UserService userService)
    {
        this.userService = userService;
    }
    public async ValueTask<Post> Create(Post post)
    {
        var user = userService.Get(post.AuthorId);
        if (user == null)
        {
            throw new Exception("Author is not found");
        }
        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();
        if (posts.Count == 0)
        {
            post.Id = 1;
        }
        else
        {
            var lastPost = posts.Last();
            post.Id = lastPost.Id + 1;
        }
        posts.Add(post);

        var res = JsonConvert.SerializeObject(posts, Formatting.Indented);
        File.WriteAllText(Constants.POSTS_PATH, res);

        return post;
    }
    public async ValueTask<bool> Delete(int id)
    {
        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();

        var found = false;
        foreach (var post in posts)
        {
            if (post.Id == id)
            {
                found = true;
                posts.Remove(post);
                break;
            }
        }
        if (!found) 
        {
            throw new Exception("Post is not found");
        }

        var res = JsonConvert.SerializeObject(posts, Formatting.Indented);
        File.WriteAllText(Constants.POSTS_PATH, res);

        return found;
    }
    public async ValueTask<Post> Get(int id)
    {
        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();

        var found = posts.FirstOrDefault(posts => posts.Id == id);

        return found;
    }
    public async ValueTask<List<Post>> GetAll()
    {
        var data = File.ReadAllText(Constants.POSTS_PATH);
        return JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();
    }
    public async ValueTask<List<Post>> GetAllByAuthorId(int id)
    {
        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();

        List<Post> found = posts.Where(post => post.AuthorId == id).ToList();

        return found;
    }
    public async ValueTask<Post> Update(int id, int userId, Post post)
    {
        var user = userService.Get(post.AuthorId);
        if (user == null)
        {
            throw new Exception("Author is not found");
        }

        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();

        var found = posts.FirstOrDefault(post => post.Id == id);
        if (found == null)
        {
            throw new Exception("Post is not found");
        }

        found.Title = post.Title;
        found.Description = post.Description;
        
        var res = JsonConvert.SerializeObject(posts, Formatting.Indented);
        File.WriteAllText(Constants.POSTS_PATH, res);

        return post;
    }
    public async ValueTask<bool> IncViewsCount(int id)
    {
        var data = File.ReadAllText(Constants.POSTS_PATH);
        var posts = JsonConvert.DeserializeObject<List<Post>>(data) ?? new List<Post>();

        var found = posts.FirstOrDefault(post => post.Id == id);
        if (found == null)
        {
            throw new Exception("Post is not found");
        }

        found.ViewsCount++;

        var res = JsonConvert.SerializeObject(posts, Formatting.Indented);
        File.WriteAllText(Constants.POSTS_PATH, res);

        return true;
    }
}
