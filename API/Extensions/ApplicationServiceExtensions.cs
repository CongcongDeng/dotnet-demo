using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<DataContext>(opt => { opt.UseSqlite(config.GetConnectionString("DefaultConnection")); });
        // config cors
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"); });
        });
        services.AddMediatR(typeof(AList.Handler));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<AList>>();
        services.AddSingleton(typeof(ILogger), logger);

        return services;
    }
}