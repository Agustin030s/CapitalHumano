using Application.Interfaces;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repository;

namespace Persistance
{
    public static class ServicesExtensions
    {
        public static void AddPersistanceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("No tiene una cadena de conexión"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHangfireServer();

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
        }
    }
}
