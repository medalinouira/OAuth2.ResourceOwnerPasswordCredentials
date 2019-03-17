/// Mohamed Ali NOUIRA
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using Newtonsoft.Json;

namespace OAuth2.ResourceOwnerPasswordCredentials.Models
{
    public class AuthInfo
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("grant_type")]
        private string GrantType { get; set; }
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

        public AuthInfo()
        {
            this.GrantType = "password";
        }
    }
}
