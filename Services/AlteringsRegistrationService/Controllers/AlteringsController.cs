using AlteringsRegistrationService.Models;
using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AlteringsRegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlteringsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        private readonly DbSet<Altering> context;
        private readonly DbSet<Customer> customers;
        public AlteringsController(IUnitOfWork unitOfWork, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
            context = unitOfWork.Set<Altering>();
            customers = unitOfWork.Set<Customer>();
        }
        Expression<Func<Altering, AlterationViewModel>> selector = x => new AlterationViewModel
        {
            Id = x.Id,
            Direction = x.Direction.ToString(),
            Operation = x.Operation.ToString(),
            State = x.State.ToString(),
            Value = x.Value + "cm"
        };

        [HttpGet]
        public async Task<ActionResult<IList<AlterationViewModel>>> GetAllAlterations(int customerId)
        {
            var res = await context.Where(x => x.CustomerId == customerId).Select(selector).ToListAsync();
            return res;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlteration([FromBody]Altering model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!customers.Any(x => x.Id == model.CustomerId))
            {
                return BadRequest($"There is no any customer with Id = {model.CustomerId}");
            }
            model.State = AlteringState.Initial;
            context.Add(model);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("SetOnGoing")]
        public async Task<IActionResult> SetOnGoing(int alterationId)
        {
            var alteration = await context.FirstOrDefaultAsync(x => x.Id == alterationId); ;
            if (alteration == null)
                return NotFound();
            alteration.State = AlteringState.OnGoing;
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("SetDone")]
        public async Task<IActionResult> SetDone(int alterationId)
        {
            var alteration = await context.FirstOrDefaultAsync(x => x.Id == alterationId); ;
            if (alteration == null)
                return NotFound();
            alteration.State = AlteringState.Done;
            await _unitOfWork.SaveChangesAsync();
            await _bus.Publish(new AlterationFinished { AlterationId = alterationId });
            return Ok();
        }
    }
}