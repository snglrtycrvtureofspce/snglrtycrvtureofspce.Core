using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using snglrtycrvtureofspce.Core.Base.Infrastructure;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Attributes;
using snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Interfaces;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq.Services.Implementations;

public class RabbitManager(IPooledObjectPolicy<IModel> objectPolicy) : IRabbitManager
{
  private readonly DefaultObjectPool<IModel> _objectPool = new(objectPolicy, Environment.ProcessorCount * 2);

  public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey) where T : class
  {
    if ((object)message == null)
      return;
    IModel model = _objectPool.Get();
    try
    {
      model.ExchangeDeclare(exchangeName, exchangeType, true, false, (IDictionary<string, object>)null);
      var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object)message));
      IBasicProperties basicProperties = model.CreateBasicProperties();
      basicProperties.Persistent = true;
      basicProperties.Type = message.GetType().Name;
      model.BasicPublish(exchangeName, routeKey, basicProperties, (ReadOnlyMemory<byte>)bytes);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
    finally
    {
      _objectPool.Return(model);
    }
  }

  public T Subscribe<T>(string queueName, Guid id) where T : IEntity
  {
    BlockingCollection<T> respQueue = new BlockingCollection<T>();
    IModel channel = _objectPool.Get();
    channel.QueueDeclare(queueName, true, false, false, (IDictionary<string, object>)null);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    consumer.Received += (EventHandler<BasicDeliverEventArgs>)((ch, ea) =>
    {
      T obj = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(ea.Body.ToArray()));
      channel.BasicAck(ea.DeliveryTag, false);
      if (obj.Id != id)
        return;
      respQueue.Add(obj);
      respQueue.CompleteAdding();
    });
    channel.BasicConsume(queueName, false, (IBasicConsumer)consumer);
    return respQueue.Take();
  }

  public void Send<T>(T message) where T : class
  {
    if ((object)message == null)
      return;
    IModel model = this._objectPool.Get();
    if (!(typeof(T).GetCustomAttributes(typeof(RabbitQueryAttribute)).FirstOrDefault<Attribute>() is
          RabbitQueryAttribute rabbitQueryAttribute))
      throw new Exception("The RabbitQueryAttribute attribute is not exist");
    try
    {
      model.ExchangeDeclare(rabbitQueryAttribute.ExchangeName, rabbitQueryAttribute.ExchangeType, true, false,
        (IDictionary<string, object>)null);
      byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object)message));
      IBasicProperties basicProperties = model.CreateBasicProperties();
      basicProperties.Persistent = true;
      model.BasicPublish(rabbitQueryAttribute.ExchangeName, rabbitQueryAttribute.RouteKey, basicProperties,
        (ReadOnlyMemory<byte>)bytes);
    }
    finally
    {
      this._objectPool.Return(model);
    }
  }

  public void Consume<T, TE>(Func<T, TE> lambda)
  {
    if (!(typeof(T).GetCustomAttributes(typeof(RabbitQueryAttribute)).FirstOrDefault<Attribute>() is
          RabbitQueryAttribute))
      throw new Exception("The RabbitQueryAttribute attribute is not exist");
    IModel channel = _objectPool.Get();
    channel.QueueDeclare(Assembly.GetExecutingAssembly().FullName + nameof(T), true, false, false, 
      (IDictionary<string, object>)null);
    new EventingBasicConsumer(channel).Received += (EventHandler<BasicDeliverEventArgs>)(async (ch, ea) =>
    {
      var content = Encoding.UTF8.GetString(ea.Body.ToArray());
      try
      {
        T message = JsonConvert.DeserializeObject<T>(content);
        TE res = lambda(message);
        message = default(T);
        res = default(TE);
      }
      finally
      {
        channel.BasicAck(ea.DeliveryTag, false);
      }
      
      content = (string)null;
    });
  }
}