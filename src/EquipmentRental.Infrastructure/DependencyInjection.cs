using EquipmentRental.Application.Interfaces;
using EquipmentRental.Infrastructure.Repositories;
using EquipmentRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentRental.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));


        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
