using Mobile_Shared_Api.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mobile_Shared_Api.Models
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class UserAccount
    {
        private List<UserProfile> _profiles;
        private string _password;

        [JsonProperty("userId")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profiles")]
        public List<UserProfile> Profiles
        {
            get { return _profiles = (_profiles ?? new List<UserProfile>()); }
            set { _profiles = value; }
        }

        public UserAccount CreateAccount(Guid id, string firstName, string lastName, string username, string password)
        {
            return new UserAccount
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                _password = string.IsNullOrWhiteSpace(password) ? "Password123" : password
            };
        }

        public void UpdateAccount(UserAccount account, string password)
        {
            Id = account.Id;
            FirstName = account.FirstName;
            LastName = account.LastName;
            Username = account.Username;

            if (!string.IsNullOrWhiteSpace(password))
            {
                _password = password;
            }
        }

        public UserProfile GetProfile(Guid id)
        {
            if (!Profiles.Any(p => p.Id == id))
            {
                throw new Exception("Could not find the requested User Profile");
            }

            return Profiles.Single(p => p.Id == id);
        }

        public void AddProfile(UserProfile profile)
        {
            Profiles.Add(profile);
        }

        public void UpdateProfile(Guid profileId, UserProfile profile)
        {
            if (!Profiles.Any(p => p.Id == profileId))
            {
                throw new Exception("Could not find the profile to updated");
            }

            var editProfile = Profiles.Single(p => p.Id == profileId);

            editProfile.UpdateUserProfile(profile);
        }

        public void DeleteProfile(Guid profileId)
        {
            if (Profiles.Any(p => p.Id == profileId))
            {
                var existing = Profiles.Single(p => p.Id == profileId);
                Profiles.Remove(existing);
            }
        }

        public static void SaveUserAccounts(List<UserAccount> items)
        {
            using (var tbl = new DataTable())
            {
                tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("FirstName", typeof(string)));
                tbl.Columns.Add(new DataColumn("LastName", typeof(string)));
                tbl.Columns.Add(new DataColumn("Username", typeof(string)));
                tbl.Columns.Add(new DataColumn("Password", typeof(string)));

                foreach (var i in items)
                {
                    var row = tbl.NewRow();
                    row["Id"] = i.Id;
                    row["FirstName"] = i.FirstName;
                    row["LastName"] = i.LastName;
                    row["Username"] = i.Username;
                    row["Password"] = i._password;
                    tbl.Rows.Add(row);
                }

                var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);
                var parameters = new List<IDbDataParameter> { dataLayer.BuildParameter("Items", tbl, "dbo.UserAccountParam") };

                dataLayer.ExecuteNonQuery("dbo.SaveUserAccounts", parameters);
            }
        }

        public static List<UserAccount> GetAllUserAccounts()
        {
            var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);
            var results = dataLayer.GetTypedList("dbo.GetAllUserAccounts", LoadFromView);

            return AggregateUserAccounts(results);
        }

        public static List<UserAccount> AggregateUserAccounts(List<UserAccount> accounts)
        {
            var accts = new Dictionary<Guid, UserAccount>();
            var profiles = new Dictionary<Guid, UserProfile>();
            var awards = new Dictionary<Guid, AwardItem>();

            foreach(var a in accounts.SelectMany(act => act.Profiles.SelectMany(p => p.Awards)))
            {
                if (awards.ContainsKey(a.Id))
                {
                    continue;
                }

                awards.Add(a.Id, a);
            }

            foreach (var p in accounts.SelectMany(act => act.Profiles))
            {
                if (profiles.ContainsKey(p.Id))
                {
                    continue;
                }

                profiles.Add(p.Id, p);
            }

            foreach (var a in accounts)
            {
                if (accts.ContainsKey(a.Id))
                {
                    continue;
                }

                accts.Add(a.Id, a);
            }

            foreach (var p in profiles.Values)
            {
                p.Awards = awards.Values.Where(a => a.UserProfileId == p.Id).ToList();
            }

            foreach (var a in accts.Values)
            {
                a.Profiles = profiles.Values.Where(p => p.UserAccountId == a.Id).ToList();
            }

            return accts.Values.Select(a => a).ToList();
        }

        public static UserAccount LoadFromView(IDataRecord reader)
        {
            return new UserAccount
            {
                Id = (Guid)reader["UserAccountId"],
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Username = reader["Username"].ToString(),
                _password = reader["Password"].ToString(),
                Profiles = new List<UserProfile>
                {
                    new UserProfile
                    {
                        Id = (Guid)reader["UserProfileId"],
                        EmailAddress = reader["EmailAddress"].ToString(),
                        Client = new ClientAccount
                        {
                            Id = (Guid)reader["ClientAccountId"],
                            ClientName = reader["ClientName"].ToString()
                        },
                        Awards = new List<AwardItem>
                        {
                            new AwardItem
                            {
                                Id = (Guid)reader["AwardItemId"],
                                Status = (AwardStatus)reader["AwardStatusId"],
                                AwardName = reader["AwardName"].ToString(),
                                EarnedValue = (decimal)reader["EarnedValue"],
                                CurrentValue = (decimal)reader["CurrentValue"]
                            }
                        }
                    }
                }
            };
        }
    }
}