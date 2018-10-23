using Newtonsoft.Json;

namespace KobieRewards.Model
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class AuthItem
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
