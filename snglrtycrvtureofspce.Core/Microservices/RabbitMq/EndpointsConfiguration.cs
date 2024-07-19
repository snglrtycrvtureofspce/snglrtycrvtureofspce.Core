using System.Collections.Generic;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq;

public class EndpointsConfiguration : IEndpointsConfiguration
{
    public IEndpointConfiguration MapEndpoint<T>(string queue, bool durable = true, bool exclusive = false, 
        bool autoDelete = false, IDictionary<string, object> arguments = null)
    {
        EndpointConfiguration<T> endpointConfiguration = new EndpointConfiguration<T>()
        {
            Queue = queue,
            Durable = durable,
            Exclusive = exclusive,
            AutoDelete = autoDelete,
            Arguments = arguments
        };
        Endpoints.Add((IEndpointConfiguration) endpointConfiguration);
        
        return (IEndpointConfiguration) endpointConfiguration;
    }

    public List<IEndpointConfiguration> Endpoints { get; set; } = [];
}