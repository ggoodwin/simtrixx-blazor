using System.Reflection;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient<ILicenseRepository, LicenseRepository>()
                .AddTransient<IStripeRepository, StripeRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        //public static IServiceCollection AddServerStorage(this IServiceCollection services) => AddServerStorage(services, null);

        //public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        //{
        //    return services
        //        .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
        //        .AddScoped<IStorageProvider, ServerStorageProvider>()
        //        .AddScoped<IServerStorageService, ServerStorageService>()
        //        .AddScoped<ISyncServerStorageService, ServerStorageService>()
        //        .Configure<SystemTextJsonOptions>(configureOptions =>
        //        {
        //            configure?.Invoke(configureOptions);
        //            if (configureOptions.JsonSerializerOptions.Converters.All(c => c.GetType() != typeof(TimespanJsonConverter)))
        //                configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
        //        });
        //}
    }
}
