using AutoMapper;
using BusinessLayer.Mappings;
using BusinessLayer.Services;
using BusinessLayer.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

// Microsoft.Extensions.DependencyInjection => IServiceCollection
public static class BusinessLayerExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            var mapperList = new List<Profile>();

            mapperList.Add(new MappingUsers());
            mapperList.Add(new MappingTasks());
            // profileList.Add(new MappingEmployees());
            // profileList.Add(new MappingBusinesses());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITokenService, TokenService>();
            // services.AddScoped<IBusinessService, BusinessService>();

            services.AddAutoMapper(c => c.AddProfiles(mapperList), typeof(List<Profile>));
            
            
            return services;
        }
}
