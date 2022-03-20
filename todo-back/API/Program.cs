using System.Text;
using BusinessLayer;
using BusinessLayer.Extensions;
using Core.Entities;
using EFCore;
using EFCore.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddBusinessLayer();

builder.Services.AddCors();

builder.Services.AddIdentityServices(builder.Configuration);



// connection string is sent for binding app with database
builder.Services.AddEFCore(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        // .WithOrigins("http://localhost:4200")
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// seed data on starting app
using (var scope = app.Services.CreateScope())
{
    try
    {
        var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
        var roleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<Role>>();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedUsers(userManager, roleManager, builder.Configuration);
    }
    catch (System.Exception ex)
    {
        // intro for ILogger
        var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }

}

app.Run();
