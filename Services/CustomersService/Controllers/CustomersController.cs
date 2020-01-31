using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using CustomersService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Customer> context;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            context = unitOfWork.Set<Customer>();
        }

        Expression<Func<Customer, CustomerViewModel>> selector = x => new CustomerViewModel
        {
            Id = x.Id,
            FullName = x.FullName,
            OverallAlterings = x.Alterings.Count(),
            InitialAlterings = x.Alterings.Count(z => z.State == AlteringState.Initial),
            PaidAlterings = x.Alterings.Count(z => z.State == AlteringState.Paid),
            OnGoingAlterings = x.Alterings.Count(z => z.State == AlteringState.OnGoing),
            DoneAlterings = x.Alterings.Count(z => z.State == AlteringState.Done),
        };

        [HttpGet]
        public async Task<ActionResult<IList<CustomerViewModel>>> GetAllCustomers()
        {
            var res = await context.Select(selector).ToListAsync();
            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerViewModel>> Get(int id)
        {
            var res = await context.Select(selector).FirstOrDefaultAsync(x => x.Id == id);
            if(res == null)
            {
                return NotFound();
            }
            return res;
        }
    }
}