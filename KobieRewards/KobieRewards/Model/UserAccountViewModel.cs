using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KobieRewards.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class UserAccountViewModel : BaseViewModel
    {
        private List<UserProfileViewModel> _profiles;

        [JsonProperty("userId")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profiles")]
        public List<UserProfileViewModel> Profiles
        {
            get { return _profiles = (_profiles ?? new List<UserProfileViewModel>()); }
            set { _profiles = value; }
        }

        public override ValidationResponse ValidateViewModel()
        {
            return new ValidationResponse
            {
                IsValid = true,
                ValidationMessage = string.Empty
            };
        }
    }
}
