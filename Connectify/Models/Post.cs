using Newtonsoft.Json;

namespace Connectify.Models;

public class Post
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("title")]

    public string Title { get; set; }
    [JsonProperty("description")]

    public string Description { get; set; }
    [JsonProperty("author_id")]

    public int AuthorId { get; set; }
    [JsonProperty("views_count")]
    public double ViewsCount { get; set; }
}
