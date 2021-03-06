using AlteringsRegistrationService.Controllers;
using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBasedMicroservice.Tests
{
    [TestClass]
    public class AlteringsControllerUnitTests
    {
        private AlteringsController controller;
        private AzureBasedMicroserviceContext dbContext;
        private int alterationIdPublishedByMockedBus;
        public AlteringsControllerUnitTests()
        {
            controller = new AlteringsController(new AzureBasedMicroserviceContext(), null);
            dbContext = new AzureBasedMicroserviceContext();
        }

        [TestMethod]
        public async Task GetAllAlterations_ShouldNotNull()
        {
            var result = await controller.GetAllAlterations(10);

            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task CreateAlteration_Should_Raise_BadRequest_InvalidCustomerId()
        {
            var result = await controller.CreateAlteration(new EntityFramework.Alterings.Altering
            {
                CustomerId = -1234
            });

            Assert.IsTrue((result as BadRequestObjectResult) != null, "It should return BadRequest with invalid CustomerId");
        }

        [TestMethod]
        public async Task CreateAlteration_CreatesData()
        {
            var data = new EntityFramework.Alterings.Altering
            {
                CustomerId = 1,
                Value = 1
            };
            var result = await controller.CreateAlteration(data);

            Assert.IsTrue((result as OkObjectResult) != null, result.ToString());

            removeAlterationFromDb(data.Id);
        }

        [TestMethod]
        public async Task SetOnGoing_CheckValidity()
        {
            var result = await controller.SetOnGoing(-111);

            Assert.IsTrue((result as NotFoundResult) != null, "It should return NotFound with invalid AlterationId");
        }

        [TestMethod]
        public async Task SetOnGoing_CheckFunctionality()
        {
            var data = new EntityFramework.Alterings.Altering
            {
                CustomerId = 1,
                Value = 1
            };
            await controller.CreateAlteration(data);
            var result = await controller.SetOnGoing(data.Id);

            Assert.IsTrue(data.State == EntityFramework.Alterings.AlteringState.OnGoing);
            Assert.IsTrue((result as OkResult) != null, "It should return OkResult with valid AlterationId");

            removeAlterationFromDb(data.Id);
        }


        [TestMethod]
        public async Task SetDone_CheckValidity()
        {
            var result = await controller.SetDone(-111);

            Assert.IsTrue((result as NotFoundResult) != null, "It should return NotFound with invalid AlterationId");
        }

        [TestMethod]
        public async Task SetDone_CheckFunctionality()
        {
            var mock = new Mock<IBus>();
            var data = new EntityFramework.Alterings.Altering
            {
                CustomerId = 1,
                Value = 1
            };
            mock.Setup(x => x.Publish(new AlterationFinished(), default)).Returns(Task.CompletedTask);
            controller = new AlteringsController(new AzureBasedMicroserviceContext(), mock.Object);

            await controller.CreateAlteration(data);
            var result = await controller.SetDone(data.Id);

            Assert.IsTrue(data.State == EntityFramework.Alterings.AlteringState.Done);
            Assert.IsTrue((result as OkResult) != null, "It should return OkResult with valid AlterationId");

            removeAlterationFromDb(data.Id);
        }


        void removeAlterationFromDb(int id)
        {
            dbContext.Alterings.Remove(dbContext.Alterings.FirstOrDefault(x => x.Id == id));
            dbContext.SaveChanges();
        }

    }
}
