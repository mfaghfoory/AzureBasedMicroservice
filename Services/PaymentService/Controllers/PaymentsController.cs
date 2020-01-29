using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        private readonly DbSet<Payment> context;
        private readonly DbSet<Altering> alterings;
        public PaymentsController(IUnitOfWork unitOfWork, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
            context = unitOfWork.Set<Payment>();
            alterings = unitOfWork.Set<Altering>();
        }

        public async Task<IActionResult> RegisterNewPayment([FromBody]NewPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            var trackingCode = Guid.NewGuid().ToString();
            context.Add(new Payment
            {
                AlteringId = model.AlteringId,
                Amount = model.Amount,
                TrackingCode = trackingCode
            });
            await _unitOfWork.SaveChangesAsync();
            await _bus.Publish(new AlterationIsPaid { AlterationId = model.AlteringId });
            return Ok(trackingCode);
        }
    }
}