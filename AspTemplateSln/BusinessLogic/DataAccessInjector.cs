using DataAccess.Contexts;
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
        }
    }
}
