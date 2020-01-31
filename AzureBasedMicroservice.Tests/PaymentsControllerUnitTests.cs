using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentService.Controllers;
using PaymentService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBasedMicroservice.Tests
{
    [TestClass]
    public class PaymentsControllerUnitTests
    {
        private PaymentsController controller;
        private AzureBasedMicroserviceContext dbContext;
        public PaymentsControllerUnitTests()
        {
            controller = new PaymentsController(new AzureBasedMicroserviceContext(), null);            
            dbContext = new AzureBasedMicroserviceContext();
        }

        [TestMethod]
        public async Task RegisterNewPayment_Should_Raise_BadRequest_InvalidAlterationId()
        {
            var result = await controller.RegisterNewPayment(new PaymentService.Models.NewPaymentViewModel
            {
                AlteringId = -1234
            });

            Assert.IsTrue((result as BadRequestObjectResult) != null, "It should return BadRequest with invalid CustomerId");
        }

        [TestMethod]
        public async Task RegisterNewPayment_CheckFunctionality()
        {
            var altering = new Altering
            {
                CustomerId = 1
            };
            dbContext.Alterings.Add(altering);
            dbContext.SaveChanges();

            var mock = new Mock<IBus>();
            mock.Setup(x => x.Publish(new OrderPaid { AlterationId = altering.Id }, default)).Returns(() =>
            {
                return Task.CompletedTask;
            });
            controller = new PaymentsController(new AzureBasedMicroserviceContext(), mock.Object);

            var data = new NewPaymentViewModel
            {
                AlteringId = altering.Id,
                Amount = 1000
            };
            var result = await controller.RegisterNewPayment(data);

            Assert.IsTrue((result as OkResult) != null, "It should return OkResult with valid AlterationId");


            removeAlterationFromDb(altering.Id);
            removePaymentFromDb(altering.Id);
        }


        void removeAlterationFromDb(int id)
        {
            dbContext.Alterings.Remove(dbContext.Alterings.FirstOrDefault(x => x.Id == id));
            dbContext.SaveChanges();
        }

        void removePaymentFromDb(int alteringId)
        {
            dbContext.Payments.RemoveRange(dbContext.Payments.Where(x => x.AlteringId == alteringId));
            dbContext.SaveChanges();
        }
    }
}
