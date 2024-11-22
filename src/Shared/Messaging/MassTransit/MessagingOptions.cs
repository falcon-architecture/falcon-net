namespace Falcon.Messaging.MassTransit;

public class MessagingOptions
{
    public required Action<IBusRegistrationConfigurator> ApplicationBusConfigurator { get; set; }
    public required Action<IBusRegistrationConfigurator> DomainBusConfigurator { get; set; }
    public required Action<IBusRegistrationConfigurator> InfrastructureBusConfigurator { get; set; }

    public required Action<IBusRegistrationConfigurator> QueryBusConfigurator { get; set; }
    public required Action<IMediatorRegistrationConfigurator> CommandBusConfigurator { get; set; }
}

