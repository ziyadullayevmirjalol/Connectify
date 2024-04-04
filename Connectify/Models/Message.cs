using Newtonsoft.Json;

namespace Connectify.Models;

public class Message
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("chat_id")]
    public int ChatId { get; set; }
    [JsonProperty("user_id")]
    public int UserID { get; set; }
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
