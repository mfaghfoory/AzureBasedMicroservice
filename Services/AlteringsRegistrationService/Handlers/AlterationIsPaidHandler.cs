using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AlteringsRegistrationService.Handlers
{
    public class AlterationIsPaidHandler : IConsumer<OrderPaid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Altering> _repo;
        public AlterationIsPaidHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.Set<Altering>();
        }
        public async Task Consume(ConsumeContext<OrderPaid> context)
        {
            var obj = await _repo.FindAsync(context.Message.AlterationId);
            obj.State = AlteringState.Paid;

            //todo -- send message/email to tailor to notify him about the new payment
            //so then he can start working on the paid order

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
