using AlteringsRegistrationService.Queries;
using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlteringsRegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase,
        IConsumer<GetAllCustomers>,
        IConsumer<GetCustomersById>
    {
        private readonly AzureBasedMicroserviceContext _dbContext = new AzureBasedMicroserviceContext();

        [HttpGet]
        public async Task<IList<Customer>> GetAllCustomers()
        {
            var res = await _dbContext.Customers.ToListAsync();
            return res;
        }

        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
            var res = await _dbContext.Customers.FindAsync(id);
            return res;
        }

        //****** Although we can/should create a seperate class to hande commands/queries, I prefered to consider this controller as a handler
        //****** because by doind this, we can avoid duplicate codes between handlers and action methods
        [NonAction]
        public async Task Consume(ConsumeContext<GetAllCustomers> context)
        {
            var res = await GetAllCustomers();
            await context.RespondAsync(res);
        }

        [NonAction]
        public async Task Consume(ConsumeContext<GetCustomersById> context)
        {
            var res = await Get(context.Message.CustomerId);
            await context.RespondAsync(res);
        }
    }
}