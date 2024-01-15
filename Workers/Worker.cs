using MassTransit;

namespace Workers;

/// <summary>
///     This may not be required for rabbitMQ,
///     since MassTransit internally uses a hosted service
///     We can still use this to start/stop the bus
/// </summary>
public class Worker : IHostedService
{
    private readonly IBusControl _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, IBusControl bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting the bus...");
        await _bus.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping the bus...");
        await _bus.StopAsync(cancellationToken);
    }
}