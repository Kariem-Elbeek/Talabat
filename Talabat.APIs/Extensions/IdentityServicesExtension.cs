using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data.Identity;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options => { })
            .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication();
            return services;

        }
    }
}
