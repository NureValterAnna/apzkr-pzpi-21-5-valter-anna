using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Hubs;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<IPrescriptionRepository, PrecriptionRepository>();
        services.AddScoped<IDispenserRepository, DispenserRipository>();
        services.AddScoped<IMedicineStockRepository, MedicineStockRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IPasswordService, PasswordService>();

        return services;
    }
}
