using System;
using Microsoft.Extensions.Options;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Configurations;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

public interface IEndpointConfiguration
{
    void WithBinding(string exchange, string routingKey);

    IBus BuildWrapper(IServiceProvider services, IOptions<RabbitMqConfiguration> options);
}