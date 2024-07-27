using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Context;

namespace TaskManagementSystem.API.Extensions
{
    public static class RepositoryCollectionExtension
    {
        public static void InjectRepository(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider()?.GetService<IOptions<AppSettings>>()?.Value;

            services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseLazyLoadingProxies().UseSqlServer(appSettings?.DatabaseString,
                    opts =>
                    {
                        opts.CommandTimeout(Convert.ToInt32(appSettings?.DbTimeoutInSecond));
                        opts.EnableRetryOnFailure();
                    });
                });
        }
    }
}
