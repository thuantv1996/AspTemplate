using Microsoft.EntityFrameworkCore;
using DataAccess.Models.Identity;

namespace DataAccess.Contexts
{
    public class WebTemplateContext : DbContext
    {
        // Đóng lại khi migration db
        // dùng khi dependency injection
        public WebTemplateContext(DbContextOptions<WebTemplateContext> opts) : base(opts)
        {
        }

        // Dùng khi migration db
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-OQLFA92\SQLEXPRESS;Database=WebTemplateDB;Trusted_Connection=True;");
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}
