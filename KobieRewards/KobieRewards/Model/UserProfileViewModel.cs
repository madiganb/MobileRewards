using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KobieRewards.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class UserProfileViewModel : BaseViewModel
    {
        private List<AwardItemViewModel> _awards;

        [JsonProperty("profileId")]
        public Guid Id { get; set; }

        [JsonProperty("userAccountId")]
        public Guid UserAccountId { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("client")]
        public ClientViewModel Client { get; set; }

        [JsonProperty("awards")]
        public List<AwardItemViewModel> Awards
        {
            get { return _awards = (_awards ?? new List<AwardItemViewModel>()); }
            set { _awards = value; }
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
