using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
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
    }
}