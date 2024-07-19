using System.Threading;
using System.Threading.Tasks;

namespace snglrtycrvtureofspce.Core.Microservices.RabbitMq;

public interface IBus
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}