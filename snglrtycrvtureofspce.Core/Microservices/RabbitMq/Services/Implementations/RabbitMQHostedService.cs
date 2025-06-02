using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Configurations;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;

public class RabbitMqHostedService : BackgroundService
{
    private readonly IList<IBus> _wrappers;

    public RabbitMqHostedService(IServiceProvider services, IOptions<RabbitMqConfiguration> options,
        IEndpointsConfiguration configuration)
    {
        _wrappers = (IList<IBus>)configuration.Endpoints
            .Select<IEndpointConfiguration, IBus>(
                (Func<IEndpointConfiguration, IBus>)
                (x => x.BuildWrapper(services, options)))
            .ToList<IBus>();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
        Task.WhenAll(_wrappers.Select<IBus, Task>((Func<IBus, Task>)(x => x.ExecuteAsync(stoppingToken))));
}