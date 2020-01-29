using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using CustomersService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AzureBasedMicroservice.EntityFramework.Alterings;
using System;
using System.Linq.Expressions;

namespace CustomersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AzureBasedMicroserviceContext _dbContext = new AzureBasedMicroserviceContext();

        Expression<Func<Customer, CustomerViewModel>> selector = x => new CustomerViewModel
        {
            Id = x.Id,
            FullName = x.FullName,
            OverallAlterings = x.Alterings.Count(),
            UnpaidAlterings = x.Alterings.Count(z => z.State == AlteringState.Paid)
        };

        [HttpGet]
        public async Task<IList<CustomerViewModel>> GetAllCustomers()
        {
            var res = await _dbContext.Customers.Select(selector).ToListAsync();
            return res;
        }

        [HttpGet("{id}")]
        public async Task<CustomerViewModel> Get(int id)
        {
            var res = await _dbContext.Customers.Select(selector).FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
    }
}