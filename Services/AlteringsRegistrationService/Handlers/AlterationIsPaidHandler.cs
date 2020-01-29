using AlteringsRegistrationService.Commands;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace AlteringsRegistrationService.Handlers
{
    public class AlterationIsPaidHandler : IConsumer<AlterationIsPaid>
    {
        public Task Consume(ConsumeContext<AlterationIsPaid> context)
        {
            throw new NotImplementedException();
        }
    }
}
