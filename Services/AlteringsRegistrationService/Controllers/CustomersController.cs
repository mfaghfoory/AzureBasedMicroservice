using AlteringsRegistrationService.Queries;
using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlteringsRegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase,
        IRequestHandler<GetAllCustomers, IList<Customer>>, 
        IRequestHandler<GetCustomersById, Customer>
    {
        private readonly AzureBasedMicroserviceContext context = new AzureBasedMicroserviceContext();

        [HttpGet]
        public async Task<IList<Customer>> GetAllCustomers()
        {
            var result = await Handle(new GetAllCustomers());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var result = await Handle(new GetCustomersById(id));
            return result;
        }

        //****** Although we can/should create a seperate class to hande commands/queries, I prefered to consider this controller as a handler
        //****** because by doind this, we can avoid duplicate codes between handlers and action methods
        [NonAction]
        public async Task<IList<Customer>> Handle(GetAllCustomers request)
        {
            var res = await context.Customers.ToListAsync();
            return res;
        }

        [NonAction]
        public async Task<Customer> Handle(GetCustomersById request)
        {
            var res = await context.Customers.FindAsync(request.CustomerId);
            return res;
        }
    }
}