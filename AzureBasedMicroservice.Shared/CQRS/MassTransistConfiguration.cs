using GreenPipes;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace AzureBasedMicroservice.Shared.CQRS
{
    public static class MassTransistConfiguration
    {
        static string connectionString = "Endpoint=sb://your-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xyz";

        public static void MassTransist(this IServiceCollection services, string appName, List<Type> handlers)
        {
            foreach (var item in handlers)
            {
                services.AddScoped(item);
            }

            services.AddMassTransit(x =>
            {
                x.AddBus(p =>
                {
                    var bus = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {
                        var host = cfg.Host(connectionString, h =>
                        {
                            // This is optional, but you can specify the protocol to use.
                            //h.TransportType = TransportType.AmqpWebSockets;
                        });

                        cfg.ReceiveEndpoint(host, $"", e =>
                        {
                            foreach (var item in handlers)
                            {                                
                                e.Consumer(item, c => p.GetRequiredService(c));
                            }
                        });
                        // or, configure the endpoints by convention
                        cfg.ConfigureEndpoints(p);

                        services.AddScoped(provider => host);
                    });
                    bus.Start();
                    services.AddSingleton<IPublishEndpoint>(bus);
                    services.AddSingleton<ISendEndpointProvider>(bus);
                    services.AddSingleton<IBus>(bus);
                    return bus;
                });
            });
        }
    }
}
