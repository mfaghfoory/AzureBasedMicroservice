using AzureBasedMicroservice.EntityFramework.Customers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Payment> context;
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            context = unitOfWork.Set<Payment>();
        }

    }
}