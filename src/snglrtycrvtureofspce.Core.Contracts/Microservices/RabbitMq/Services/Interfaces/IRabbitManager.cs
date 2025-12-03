using System;
using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

public interface IRabbitManager
{
    void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey) where T : class;
    
    T Subscribe<T>(string queueName, Guid id) where T : IEntity;
    
    void Send<T>(T message) where T : class;
}