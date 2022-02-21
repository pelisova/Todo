using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Context;
using EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore
{
    // Microsoft.Extensions.DependencyInjection => IServiceCollection, ServiceLifetime
    // Microsoft.EntityFrameworkCore => DbContextOptionsBuilder
    public static class EFCoreExtensions
    {
        public static IServiceCollection AddEFCore(
                this IServiceCollection services,
                Action<DbContextOptionsBuilder> dboptions,
                ServiceLifetime scope = ServiceLifetime.Scoped)
        {
            
            services.AddDbContext<DataContext>(dboptions, scope);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            // services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // services.AddScoped<IBusinessRepository, BusinessRepository>();
            
            return services;
        }
    }
}