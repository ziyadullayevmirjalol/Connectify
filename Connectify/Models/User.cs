using Newtonsoft.Json;

namespace Connectify.Models;
public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("firstname")]
    public string Firstname { get; set; }
    [JsonProperty("lastname")]
    public string Lastname { get; set; }
}
