using Mobile_Shared_Api.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mobile_Shared_Api.Models
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class UserProfile
    {
        private List<AwardItem> _awards;

        [JsonProperty("profileId")]
        public Guid Id { get; set; }

        [JsonProperty("userAccountId")]
        public Guid UserAccountId { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("client")]
        public ClientAccount Client { get; set; }

        [JsonProperty("awards")]
        public List<AwardItem> Awards
        {
            get { return _awards = (_awards ?? new List<AwardItem>()); }
            set { _awards = value; }
        }

        public UserProfile CreateUserProfile(Guid id, string emailAddress, ClientAccount client, List<AwardItem> awards)
        {
            return new UserProfile
            {
                Id = id,
                EmailAddress = emailAddress,
                Client = client,
                Awards = awards
            };
        }

        public void UpdateUserProfile(UserProfile profile)
        {
            Id = profile.Id;
            EmailAddress = profile.EmailAddress;
            Client = profile.Client;
            Awards = profile.Awards != null && profile.Awards.Any() ? profile.Awards : Awards;
        }

        public List<AwardItem> GetActiveAwards()
        {
            return Awards.Where(a => a.Status == AwardStatus.Active || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRedeemedAwards()
        {
            return Awards.Where(a => a.Status == AwardStatus.FullyRedeemed || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRevokedAwards()
        {
            return Awards.Where(a => a.Status == AwardStatus.RevokedDueToError || a.Status == AwardStatus.RevokedDueToFraud).ToList();
        }

        public AwardItem GetAwardById(Guid id)
        {
            if (!Awards.Any(a => a.Id == id))
            {
                return null;
            }

            return Awards.Single(a => a.Id == id);
        }

        public void CreateAward(Guid id, string awardName, decimal earnedValue)
        {
            Awards.Add(new AwardItem().CreateAward(id, awardName, earnedValue));
        }

        public void RedeemAward(Guid awardId, decimal amountRedeemed)
        {
            if (!Awards.Any(a => a.Id == awardId))
            {
                throw new Exception("Could not find award to redeem");
            }

            var award = Awards.Single(a => a.Id == awardId);

            award.RedeemAward(amountRedeemed);
        }

        public void RevokeAwardFraud(Guid awardId)
        {
            if (!Awards.Any(a => a.Id == awardId))
            {
                throw new Exception("Could not find award to revoke");
            }

            var award = Awards.Single(a => a.Id == awardId);

            award.RevokeAward(AwardStatus.RevokedDueToFraud, award.CurrentValue);
        }

        public void RevokeAwardError(Guid awardId, decimal amountRevoked)
        {
            if (!Awards.Any(a => a.Id == awardId))
            {
                throw new Exception("Could not find award to revoke");
            }

            var award = Awards.Single(a => a.Id == awardId);

            award.RevokeAward(AwardStatus.RevokedDueToError, amountRevoked);
        }

        public static void SaveUserProfiles(List<UserProfile> items)
        {
            using (var tbl = new DataTable())
            {
                tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("UserAcccountId", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("ClientAccountId", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("EmailAddress", typeof(string)));

                foreach (var i in items)
                {
                    var row = tbl.NewRow();
                    row["Id"] = i.Id;
                    row["UserAccountId"] = i.UserAccountId;
                    row["ClientAccountId"] = i.Client.Id;
                    row["EmailAddress"] = i.EmailAddress;
                    tbl.Rows.Add(row);
                }

                var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);
                var parameters = new List<IDbDataParameter> { dataLayer.BuildParameter("Items", tbl, "dbo.UserProfileParam") };

                dataLayer.ExecuteNonQuery("dbo.SaveUserProfiles", parameters);
            }
        }
    }
}