using creWin.API.Extensions.JwtConf;
using creWin.API.Services.Auth;
using creWin.API.Services.EmailSender;
using creWin.API.Services.Token;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CreWin.Infrastructure.Context;
using CreWin.Infrastructure.IRepositories;
using CreWin.Infrastructure.Repositories;
using System.Reflection;
using System.Text;
using creWin.API.Services.Categories;
using Nowadays.Infrastructure.Services;

namespace creWin.API.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddAPIRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddHttpClient();


            // **** JWT CONFIGURATION START ****
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = JwtTokenDefaults.ValidAudience,
                    ValidIssuer = JwtTokenDefaults.ValidIssuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
                    ValidateIssuerSigningKey = true, // To verify whether the token value belongs to our application.
                    ValidateLifetime = true
                };

            });
            // **** JWT CONFIGURATION END ****

            return services;

        }
    }
}
