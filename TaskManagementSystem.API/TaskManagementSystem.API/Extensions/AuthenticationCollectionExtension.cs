using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementSystem.Data.Base;

namespace TaskManagementSystem.API.Extensions
{
    public static class AuthenticationCollectionExtension
    {
        public static void InjectAuthentication(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider()?.GetService<IOptions<AppSettings>>()?.Value;
            var key = Encoding.ASCII.GetBytes(appSettings?.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
