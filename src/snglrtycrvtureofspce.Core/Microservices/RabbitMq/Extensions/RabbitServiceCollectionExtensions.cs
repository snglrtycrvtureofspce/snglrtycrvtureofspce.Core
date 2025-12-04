using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using snglrtycrvtureofspce.Core.Contracts.Microservices.RabbitMq.Services.Interfaces;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Configurations;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Extensions;

public static class RabbitServiceCollectionExtensions
{
    public static IServiceCollection AddRabbit(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
        services.AddSingleton<IRabbitManager, RabbitManager>();

        return services;
    }

    public static void AddRabbitMqEndpoints(
        this IServiceCollection services,
        Action<EndpointsConfiguration> configuration)
    {
        var implementationInstance = new EndpointsConfiguration();
        configuration(implementationInstance);
        services.AddSingleton<IEndpointsConfiguration>((IEndpointsConfiguration) implementationInstance);
        services.AddHostedService<RabbitMqHostedService>();
    }
}
