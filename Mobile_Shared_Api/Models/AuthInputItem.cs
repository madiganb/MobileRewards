using Newtonsoft.Json;

namespace Mobile_Shared_Api.Models
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class AuthInputItem
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}