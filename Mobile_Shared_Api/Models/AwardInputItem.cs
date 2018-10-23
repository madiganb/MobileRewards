using Newtonsoft.Json;

namespace Mobile_Shared_Api.Models
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class AwardInputItem
    {
        [JsonProperty("adjustmentAmount")]
        public decimal AdjustmentAmount { get; set; }

        [JsonProperty("status")]
        public AwardStatus Status { get; set; }
    }
}