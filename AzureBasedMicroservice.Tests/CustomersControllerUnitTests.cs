using AzureBasedMicroservice.EntityFramework.DBContext;
using CustomersService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AzureBasedMicroservice.Tests
{
    [TestClass]
    public class CustomersControllerUnitTests
    {
        private CustomersController controller;

        public CustomersControllerUnitTests()
        {
            controller = new CustomersController(new AzureBasedMicroserviceContext());
        }

        [TestMethod]
        public async Task GetAllCustomers_Returns_Data()
        {
            var result = await controller.GetAllCustomers();

            Assert.IsTrue(result.Value.Count > 0, "There is no data in db");
        }

        [TestMethod]
        public async Task Get_Returns_Expected()
        {
            var result = await controller.Get(1);

            Assert.IsTrue(!(result.Result is NotFoundResult), "There is no data in db");
            Assert.IsTrue(result.Value.Id == 1, "Returned data is not correct");
        }

        [TestMethod]
        public async Task Get_Check_Invalid_Data()
        {
            var result = await controller.Get(100);

            Assert.IsTrue(result.Result is NotFoundResult, "Returned data is not correct");
        }
    }
}
