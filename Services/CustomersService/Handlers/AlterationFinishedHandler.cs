using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CustomersService.Handlers
{
    public class AlterationFinishedHandler : IConsumer<AlterationFinished>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Altering> _repo;
        public AlterationFinishedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.Set<Altering>();
        }
        public Task Consume(ConsumeContext<AlterationFinished> context)
        {
            //todo -- send notification
            return Task.CompletedTask;
        }
    }
}
