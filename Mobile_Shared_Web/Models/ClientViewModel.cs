using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mobile_Shared_Web.Models
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class ClientViewModel : BaseViewModel
    {
        private List<UserProfileViewModel> _profiles;

        [JsonProperty("clientId")]
        public Guid Id { get; set; }

        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        [JsonProperty("userProfiles")]
        public List<UserProfileViewModel> UserProfiles
        {
            get { return _profiles = (_profiles ?? new List<UserProfileViewModel>()); }
            set { _profiles = value; }
        }

        public override ValidationResponse ValidateViewModel()
        {
            if (string.IsNullOrWhiteSpace(ClientName))
            {
                return new ValidationResponse
                {
                    IsValid = false,
                    ValidationMessage = "Client Name must have a valid value."
                };
            }

            return new ValidationResponse
            {
                IsValid = true,
                ValidationMessage = "Model is valid"
            };
        }
    }
}