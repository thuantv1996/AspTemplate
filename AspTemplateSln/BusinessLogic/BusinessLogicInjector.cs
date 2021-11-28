using BusinessLogic.Services.Abstract;
using BusinessLogic.Services.Implement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public static class BusinessLogicInjector
    {
        public static void InjectBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
        }
    }
}
