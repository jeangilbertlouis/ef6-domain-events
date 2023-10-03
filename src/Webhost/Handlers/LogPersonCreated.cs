using MediatR;

namespace Webhost.Handlers;

public class LogPersonCreated : INotificationHandler<PersonCreatedEvent>
{
    private readonly ILogger<LogPersonCreated> _logger;

    public LogPersonCreated(ILogger<LogPersonCreated> logger)
    {
        _logger = logger;
    }

    public Task Handle(PersonCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Created Person: {notification.Person}");
        return Task.CompletedTask;
    }
}