using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using MassTransit.Azure.ServiceBus.Core;
using System.Collections.Generic;

namespace AzureBasedMicroservice.Shared.CQRS
{
    public static class MassTransistSetup
    {
        public static void MassTransist(this IServiceCollection services, 
            string queueName, MassTransitConfig config, List<Type> consumers)
        {
            foreach (var item in consumers)
            {
                services.AddScoped(item);
            }

            services.AddMassTransit(x =>
            {
                x.AddBus(p =>
                {
                    IBusControl bus;
                    if(config.UseRabbit)
                    {
                        bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            var host = cfg.Host(config.Host, h =>
                            {
                                h.Username(config.Username);
                                h.Password(config.Password);
                            });

                            cfg.ReceiveEndpoint(queueName, e =>
                            {
                                foreach (var item in consumers)
                                {
                                    e.Consumer(item, c => services.BuildServiceProvider().GetRequiredService(c));
                                }
                            });
                            // or, configure the endpoints by convention
                            cfg.ConfigureEndpoints(p);

                            services.AddScoped(provider => host);
                        });
                    }
                    else
                    {
                        bus = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                        {
                            var host = cfg.Host(config.Host, h =>
                            {
                                // This is optional, but you can specify the protocol to use.
                                //h.TransportType = TransportType.AmqpWebSockets;
                            });

                            cfg.ReceiveEndpoint(queueName, e =>
                            {
                                foreach (var item in consumers)
                                {
                                    e.Consumer(item, c => services.BuildServiceProvider().GetRequiredService(c));
                                }
                            });
                            // or, configure the endpoints by convention
                            cfg.ConfigureEndpoints(p);

                            services.AddScoped(provider => host);
                        });
                    }                    
                    bus.Start();
                    services.AddScoped(provider => bus);
                    return bus;
                });
            });
        }
    }
}
