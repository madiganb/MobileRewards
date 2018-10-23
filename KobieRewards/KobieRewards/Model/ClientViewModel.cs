using Newtonsoft.Json;
using System;

namespace KobieRewards.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ClientViewModel : BaseViewModel
    {
        [JsonProperty("clientId")]
        public Guid Id { get; set; }

        [JsonProperty("clientName")]
        public string ClientName { get; set; }

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
