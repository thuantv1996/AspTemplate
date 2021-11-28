using DataAccess;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class DataAccessInjector
    {
        public static void InjectDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebTemplateContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<UserToken>, Repository<UserToken>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
            services.AddScoped<IRepository<UserClaim>, Repository<UserClaim>>();
        }
    }
}
