using Microsoft.Extensions.DependencyInjection;
using myShop.Application.Interface;
using myShop.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Infrastructure.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        public static IServiceCollection AddValidtors(this IServiceCollection services)
        {
           // services.AddScoped<IValidator<TblGenTaskUpdate>, InsertUpdateTaskValidator>();
            return services;
        }

    }
    
}
