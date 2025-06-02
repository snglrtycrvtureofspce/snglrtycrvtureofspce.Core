using System.Collections.Generic;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;

public class EndpointsConfiguration : IEndpointsConfiguration
{
    public IEndpointConfiguration MapEndpoint<T>(string queue, bool durable = true, bool exclusive = false,
        bool autoDelete = false, IDictionary<string, object> arguments = null)
    {
        var endpointConfiguration = new EndpointConfiguration<T>
        {
            Queue = queue,
            Durable = durable,
            Exclusive = exclusive,
            AutoDelete = autoDelete,
            Arguments = arguments
        };
        Endpoints.Add((IEndpointConfiguration)endpointConfiguration);

        return (IEndpointConfiguration)endpointConfiguration;
    }

    public List<IEndpointConfiguration> Endpoints { get; set; } = [];
}