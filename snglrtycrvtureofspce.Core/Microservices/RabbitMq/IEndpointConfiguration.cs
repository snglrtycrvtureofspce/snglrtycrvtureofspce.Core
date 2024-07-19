using System;
using Microsoft.Extensions.Options;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq;

public interface IEndpointConfiguration
{
    void WithBinding(string exchange, string routingKey);

    IBus BuildWrapper(IServiceProvider services, IOptions<RabbitMqConfiguration> options);
}