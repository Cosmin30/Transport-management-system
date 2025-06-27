using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens; 
using System.Text;

namespace Aplicatie_Transporturi.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var tokenKey = config["TokenKey"];
            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentNullException(nameof(config), "TokenKey is not configured in the application settings.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}