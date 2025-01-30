using Ambev.Prototypes.Domain.Interfaces;
using Ambev.Prototypes.Domain.Services;
using Ambev.Prototypes.Infra.Repositories;

namespace Ambev.Prototypes.WebApi
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<SaleService>();
        }
    }
}
