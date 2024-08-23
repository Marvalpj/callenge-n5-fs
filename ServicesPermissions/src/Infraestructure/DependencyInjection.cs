using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Data;
using Infraestructure.Persistence;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Services;
using Domain.Primitives;
using Domain.Permissions;
using Domain.Services;
using Domain.PermissionTypes;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;


namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // inyecta base de datos
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("database")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            // inyecta repositorios 
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();

            // inyecta servicios
            services.AddScoped<IKafkaProducer>(p => new KafkaProducer(configuration["kafka:bootstrapServer"]));

            var settingsElasticS = new ElasticsearchClientSettings(new Uri(configuration["elasticsearch:uri"]))
                .Authentication(new BasicAuthentication(configuration["elasticsearch:username"], configuration["elasticsearch:password"]))
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .DefaultIndex("permission");
            
            services.AddSingleton(new ElasticsearchClient(settingsElasticS));
            
            services.AddScoped(typeof(IElasticRepository<>), typeof(ElasticRepository<>));

            return services;
        }

    }
}
