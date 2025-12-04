using System.Threading;
using System.Threading.Tasks;

namespace snglrtycrvtureofspce.Core.Contracts.Microservices.RabbitMq.Services.Interfaces;

public interface IBus
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}
