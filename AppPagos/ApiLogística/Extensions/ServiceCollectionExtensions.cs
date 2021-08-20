using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApiLogística.DAL;

namespace ApiLogística.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseSql(this IServiceCollection services, IConfiguration configuration)
        {
            string conexion = configuration["Secrets:ConnectionSql"];
            if (!string.IsNullOrEmpty(conexion))
                services.AddDbContext<DbeContext>(options => options.UseSqlite(conexion));

            return services;
        }
    }
}
