
using System.Collections.Generic;

namespace DataAccess.Models.Identity
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserTokens = new HashSet<UserToken>();
            UserClaims = new HashSet<UserClaim>();
        }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string OtherEmail { get; set; }
        public string NumberPhone { get; set; }
        public int IsLogin { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}
