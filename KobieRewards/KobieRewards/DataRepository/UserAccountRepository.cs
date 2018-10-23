using KobieRewards.Model;
using System;

namespace KobieRewards.DataRepository
{
    public class UserAccountRepository
    {
        private static readonly Lazy<UserAccountRepository> repo = new Lazy<UserAccountRepository>(() => new UserAccountRepository());

        public static UserAccountRepository Instance { get { return repo.Value; } }

        public UserAccountViewModel Account { get; set; }
        public string Password { get; set; }
        
        private UserAccountRepository() { }
    }
}
