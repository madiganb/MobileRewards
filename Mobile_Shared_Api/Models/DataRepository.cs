using System;
using System.Collections.Generic;
using System.Linq;

namespace Mobile_Shared_Api.Models
{
    public sealed class DataRepository
    {
        private static readonly Lazy<DataRepository> repo = new Lazy<DataRepository>(() => new DataRepository());

        private Dictionary<Guid, ClientAccount> _clients;
        private Dictionary<Guid, UserAccount> _users;

        public static DataRepository Instance { get { return repo.Value; } }

        public Dictionary<Guid, ClientAccount> ClientAccounts
        {
            get { return _clients = (_clients ?? new Dictionary<Guid, ClientAccount>()); }
            set { _clients = value; }
        }

        public Dictionary<Guid, UserAccount> UserAccounts
        {
            get { return _users = (_users ?? new Dictionary<Guid, UserAccount>()); }
            set { _users = value; }
        }

        public List<UserProfile> UserProfiles
        {
            get { return UserAccounts.Values.SelectMany(ua => ua.Profiles).ToList(); }
        }

        public List<AwardItem> Awards
        {
            get { return UserProfiles.SelectMany(up => up.Awards).ToList(); }
        }

        private DataRepository() { }

        public ClientAccount GetClientAccountById(Guid id)
        {
            return ClientAccounts.ContainsKey(id) ? ClientAccounts[id] : null;
        }

        public ClientAccount AddClientAccount(string clientName)
        {
            var id = Guid.NewGuid();
            var clientAccount = new ClientAccount().CreateAccount(id, clientName);
            
            ClientAccounts.Add(id, clientAccount);

            return clientAccount;
        }

        public ClientAccount UpdateClientAccount(Guid id, string clientName)
        {
            if (!ClientAccounts.ContainsKey(id))
            {
                return null;
            }

            var clientAccount = ClientAccounts[id];
            clientAccount.ClientName = clientName;

            return clientAccount;
        }

        public void DeleteClientAccount(Guid id)
        {
            if (ClientAccounts.ContainsKey(id))
            {
                ClientAccounts.Remove(id);
            }
        }

        public UserAccount GetUserAccountById(Guid id)
        {
            return UserAccounts.ContainsKey(id) ? UserAccounts[id] : null;
        }

        public UserAccount AddUserAccount(string firstName, string lastName, string username, string password = null)
        {
            var id = Guid.NewGuid();
            var userAccount = new UserAccount().CreateAccount(id, firstName, lastName, username, password);

            UserAccounts.Add(id, userAccount);

            return userAccount;
        }

        public UserAccount UpdateUserAccount(Guid id, string firstName, string lastName, string username, string password = null)
        {
            if (!UserAccounts.ContainsKey(id))
            {
                return null;
            }

            var updatedAccount = new UserAccount().CreateAccount(id, firstName, lastName, username, password);
            var userAccount = UserAccounts[id];

            userAccount.UpdateAccount(updatedAccount, password);

            return userAccount;
        }

        public void DeleteUserAccount(Guid id)
        {
            if (UserAccounts.ContainsKey(id))
            {
                UserAccounts.Remove(id);
            }
        }

        public List<UserProfile> GetAllProfilesForUserAccount(Guid userAccountId)
        {
            var account = GetUserAccountById(userAccountId);

            if (account == null)
            {
                return new List<UserProfile>();
            }

            return account.Profiles;
        }

        public List<UserProfile> GetProfilesForClientAccount(Guid clientAccountId)
        {
            return UserProfiles.Where(up => up.Client.Id == clientAccountId).ToList();
        }

        public UserProfile GetUserProfileByProfileId(Guid userProfileId)
        {
            return UserProfiles.Single(up => up.Id == userProfileId);
        }

        public UserProfile AddNewUserProfileToAccount(Guid userAccountId, Guid clientId, string emailAddress, List<AwardItem> awards = null)
        {
            if (!UserAccounts.ContainsKey(userAccountId) || !ClientAccounts.ContainsKey(clientId))
            {
                return null;
            }

            var clientAccount = ClientAccounts[clientId];
            var newProfile = new UserProfile().CreateUserProfile(Guid.NewGuid(), emailAddress, clientAccount, awards);

            UserAccounts[userAccountId].AddProfile(newProfile);

            return newProfile;
        }

        public UserProfile UpdateUserProfile(Guid profileId, Guid clientId, string emailAddress, List<AwardItem> awards = null)
        {
            if (!ClientAccounts.ContainsKey(clientId) || !UserProfiles.Any(up => up.Id == profileId))
            {
                return null;
            }

            var clientAccount = ClientAccounts[clientId];
            var updatedUserProfile = new UserProfile().CreateUserProfile(profileId, emailAddress, clientAccount, awards);
            var currentProfile = UserProfiles.Single(up => up.Id == profileId);

            currentProfile.UpdateUserProfile(updatedUserProfile);

            return currentProfile;
        }

        public void DeleteProfile(Guid profileId, Guid userAccountId)
        {
            if (!UserAccounts.ContainsKey(userAccountId))
            {
                return;
            }

            UserAccounts[userAccountId].DeleteProfile(profileId);
        }

        public List<AwardItem> GetActiveAwardsForUserProfile(Guid userProfileId)
        {
            if (!UserProfiles.Any(up => up.Id == userProfileId))
            {
                return new List<AwardItem>();
            }

            var profile = UserProfiles.Single(up => up.Id == userProfileId);

            return profile.GetActiveAwards();
        }

        public List<AwardItem> GetRedeemedAwardsForUserProfile(Guid userProfileId)
        {
            if (!UserProfiles.Any(up => up.Id == userProfileId))
            {
                return new List<AwardItem>();
            }

            var profile = UserProfiles.Single(up => up.Id == userProfileId);

            return profile.GetRedeemedAwards();
        }

        public List<AwardItem> GetRevokedAwardsForUserProfile(Guid userProfileId)
        {
            if (!UserProfiles.Any(up => up.Id == userProfileId))
            {
                return new List<AwardItem>();
            }

            var profile = UserProfiles.Single(up => up.Id == userProfileId);

            return profile.GetRevokedAwards();
        }

        public List<AwardItem> GetAwardsForClient(Guid clientId)
        {
            return UserProfiles.Where(up => up.Client.Id == clientId).SelectMany(up => up.Awards).ToList();
        }

        public List<AwardItem> GetActiveAwardsForClient(Guid clientId)
        {
            return GetAwardsForClient(clientId).Where(a => a.Status == AwardStatus.Active || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRedeemedAwardsForClient(Guid clientId)
        {
            return GetAwardsForClient(clientId).Where(a => a.Status == AwardStatus.FullyRedeemed || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRevokedAwardsForClient(Guid clientId)
        {
            return GetAwardsForClient(clientId).Where(a => a.Status == AwardStatus.RevokedDueToError || a.Status == AwardStatus.RevokedDueToFraud).ToList();
        }

        public List<AwardItem> GetAwardsForUser(Guid userAccountId)
        {
            if (!UserAccounts.ContainsKey(userAccountId))
            {
                return new List<AwardItem>();
            }

            return UserAccounts[userAccountId].Profiles.SelectMany(up => up.Awards).ToList();
        }

        public List<AwardItem> GetActiveAwardsForUser(Guid userAccountId)
        {
            return GetAwardsForUser(userAccountId).Where(a => a.Status == AwardStatus.Active || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRedeemedAwardsForUser(Guid userAccountId)
        {
            return GetAwardsForUser(userAccountId).Where(a => a.Status == AwardStatus.FullyRedeemed || a.Status == AwardStatus.PartiallyRedeemed).ToList();
        }

        public List<AwardItem> GetRevokedAwardsForUser(Guid userAccountId)
        {
            return GetAwardsForUser(userAccountId).Where(a => a.Status == AwardStatus.RevokedDueToError || a.Status == AwardStatus.RevokedDueToFraud).ToList();
        }

        public AwardItem CreateAwardForUserProfile(Guid userProfileId, string awardName, decimal earnedValue)
        {
            if (!UserProfiles.Any(up => up.Id == userProfileId))
            {
                return null;
            }

            var profile = UserProfiles.Single(up => up.Id == userProfileId);
            var awardId = Guid.NewGuid();

            profile.CreateAward(awardId, awardName, earnedValue);

            return profile.GetAwardById(awardId);
        }

        public void RedeemAward(Guid awardId, decimal amountRedeemed)
        {
            if (!Awards.Any(a => a.Id == awardId))
            {
                return;
            }

            var award = Awards.Single(a => a.Id == awardId);

            award.RedeemAward(amountRedeemed);
        }

        public void RevokeAward(Guid awardId, AwardStatus status, decimal amountRevoked = 0.00m)
        {
            if (!Awards.Any(a => a.Id == awardId))
            {
                return;
            }

            var award = Awards.Single(a => a.Id == awardId);

            award.RevokeAward(status, amountRevoked);
        }

        public void SaveRepository()
        {
            ClientAccount.SaveClientAccounts(ClientAccounts.Values.Select(a => a).ToList());
            UserAccount.SaveUserAccounts(UserAccounts.Values.Select(a => a).ToList());
            UserProfile.SaveUserProfiles(UserProfiles);
            AwardItem.SaveAwardItems(Awards);
        }

        public void LoadRepository()
        {
            ClientAccounts = ClientAccount.GetAllClientAccounts().ToDictionary(ca => ca.Id);
            UserAccounts = UserAccount.GetAllUserAccounts().ToDictionary(ua => ua.Id);
        }
    }
}