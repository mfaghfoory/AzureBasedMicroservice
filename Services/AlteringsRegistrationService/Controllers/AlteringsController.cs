using AlteringsRegistrationService.Models;
using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.DBContext;
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
        private readonly DbSet<Altering> context;
        public AlteringsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            context = unitOfWork.Set<Altering>();
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
        public async Task<IList<AlterationViewModel>> GetAllAlterations(int customerId)
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
            model.State = AlteringState.Initial;
            context.Add(model);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}