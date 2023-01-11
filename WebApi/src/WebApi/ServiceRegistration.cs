using BL.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace WebApi
{
    public static class ServiceRegistration
    {
        public static string CorsPolicyName = "corsPolicy";
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);
            serviceCollection.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.SectionName)
            );
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });
            return serviceCollection;
        }

        public static IServiceCollection AddAllAutoMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(GetAutoMapperAssemblies());
            return serviceCollection;
        }

        private static Assembly[] GetAutoMapperAssemblies()
        {
            return new[] { typeof(ServiceRegistration).Assembly }
                    .Concat(BL.ServiceRegistration.GetAutomapperAssemblies()).ToArray();
        }


        public static IServiceCollection RegisterCors(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                                  policy =>
                                  {
                                      policy.WithOrigins(configuration["FrontendOrigin"]).AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            return serviceCollection;
        }
    }
}
