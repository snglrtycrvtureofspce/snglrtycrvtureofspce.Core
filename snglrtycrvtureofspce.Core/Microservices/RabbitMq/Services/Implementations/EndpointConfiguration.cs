using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Configurations;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.RabbitMqEndpointBinder;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;

public class EndpointConfiguration<T> : IEndpointConfiguration
{
    public string Queue { get; init; }

    public bool Durable { get; init; }

    public bool Exclusive { get; init; }

    public bool AutoDelete { get; init; }

    public IDictionary<string, object> Arguments { get; init; }

    public string Exchange { get; private set; }

    public string RoutingKey { get; private set; }

    public void WithBinding(string exchange, string routingKey)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
    }

    public IBus BuildWrapper(IServiceProvider services, IOptions<RabbitMqConfiguration> options) =>
        new RabbitMqWrapper<T>(options, this, services);
}