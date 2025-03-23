using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Services;
using Sistema_Gestor_De_Usuarios.Core.Application.Services;
using Sistema_Gestor_De_Usuarios.Core.Domain.Settings;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Sistema_Gestor_De_Usuarios.Core.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserTokenService, UserTokenService>();


            services.Configure<JWTSetting>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();  
                        c.Response.StatusCode = 401;  
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new { mensaje = "You are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new { mensaje = "You are not Authorized to access this resource" });
                        return c.Response.WriteAsync(result);
                    }
                };

            });

        }
    }
}
