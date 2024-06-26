using CreWin.Entity.Models.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CreWin.Infrastructure.Context;
using CreWin.Infrastructure.IRepositories;
using CreWin.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CreWin.Infrastructure.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CreWinContext>(conf =>
            {
                var connStr = configuration["CreWinDbConnectionString"].ToString();
                conf.UseSqlServer(connStr);
            });

            var assm = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assm);
            services.AddValidatorsFromAssembly(assm);

            services.AddIdentity<AppUser, AppRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<CreWinContext>(); // Identity'yi ekledik.   

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
