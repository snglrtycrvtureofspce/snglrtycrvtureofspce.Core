using System.Collections.Generic;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

public interface IEndpointsConfiguration
{
    List<IEndpointConfiguration> Endpoints { get; set; }
}