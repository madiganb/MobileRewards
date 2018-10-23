using Newtonsoft.Json;
using System;

namespace KobieRewards.Model
{
    public enum AwardStatus
    {
        Active = 1,
        PartiallyRedeemed = 2,
        FullyRedeemed = 3,
        RevokedDueToError = 4,
        RevokedDueToFraud = 5
    }

    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class AwardItemViewModel : BaseViewModel
    {
        [JsonProperty("awardId")]
        public Guid Id { get; set; }

        [JsonProperty("userProfileId")]
        public Guid UserProfileId { get; set; }

        [JsonProperty("awardName")]
        public string AwardName { get; set; }

        [JsonProperty("awardValue")]
        public decimal EarnedValue { get; set; }

        [JsonProperty("currentValue")]
        public decimal CurrentValue { get; set; }

        [JsonProperty("awardStatus")]
        public AwardStatus Status { get; set; }

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
