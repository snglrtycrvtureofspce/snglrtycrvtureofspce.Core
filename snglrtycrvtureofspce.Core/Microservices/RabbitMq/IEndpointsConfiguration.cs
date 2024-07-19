using System.Collections.Generic;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq;

public interface IEndpointsConfiguration
{
    List<IEndpointConfiguration> Endpoints { get; set; }
}