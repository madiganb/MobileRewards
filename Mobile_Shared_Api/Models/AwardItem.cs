using Mobile_Shared_Api.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mobile_Shared_Api.Models
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
    public class AwardItem
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

        public AwardItem CreateAward(Guid id, string awardName, decimal earnedValue)
        {
            return new AwardItem
            {
                Id = id,
                AwardName = awardName,
                EarnedValue = earnedValue,
                CurrentValue = earnedValue,
                Status = AwardStatus.Active
            };
        }

        public void RedeemAward(decimal amountRedeemed)
        {
            if (amountRedeemed > CurrentValue)
            {
                throw new Exception("Cannot redeem more than the current value of the award");
            }

            CurrentValue = CurrentValue - amountRedeemed;
            Status = CurrentValue == 0.00m ? AwardStatus.FullyRedeemed : AwardStatus.PartiallyRedeemed;
        }

        public void RevokeAward(AwardStatus status, decimal amountRevoked)
        {
            if (amountRevoked > CurrentValue)
            {
                throw new Exception("Cannot revoke more than the award is worth");
            }

            CurrentValue = CurrentValue - (amountRevoked == 0.00m ? CurrentValue : amountRevoked);
            Status = status;
        }

        public static void SaveAwardItems(List<AwardItem> items)
        {
            using (var tbl = new DataTable())
            {
                tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("UserProfileId", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("AwardStatusId", typeof(int)));
                tbl.Columns.Add(new DataColumn("AwardName", typeof(string)));
                tbl.Columns.Add(new DataColumn("EarnedValue", typeof(decimal)));
                tbl.Columns.Add(new DataColumn("CurrentValue", typeof(decimal)));

                foreach (var i in items)
                {
                    var row = tbl.NewRow();
                    row["Id"] = i.Id;
                    row["UserProfileId"] = i.UserProfileId;
                    row["AwardStatusId"] = (int)i.Status;
                    row["AwardName"] = i.AwardName;
                    row["EarnedValue"] = i.EarnedValue;
                    row["CurrentValue"] = i.CurrentValue;
                    tbl.Rows.Add(row);
                }

                var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);
                var parameters = new List<IDbDataParameter> { dataLayer.BuildParameter("Items", tbl, "dbo.AwardItemParam") };

                dataLayer.ExecuteNonQuery("dbo.SaveAwardItems", parameters);
            }
        }
    }
}