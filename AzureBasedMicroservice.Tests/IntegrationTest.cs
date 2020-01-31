using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PaymentService.Models;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBasedMicroservice.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        private AzureBasedMicroserviceContext dbContext;

        public IntegrationTest()
        {
            dbContext = new AzureBasedMicroserviceContext();
        }

        [TestMethod]
        public async Task TestTheWholeProcess()
        {
            var client = new RestClient("http://localhost:14524/");

            var createNewAlterationRequest = new RestRequest("api/AlteringsService/Alterings", Method.POST);

            createNewAlterationRequest.AddJsonBody(new Altering
            {
                CustomerId = 1,
                Value = 1
            });

            IRestResponse response = client.Execute(createNewAlterationRequest);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, $"Actual statuscode is {response.StatusCode}");

            int alterationId = JsonConvert.DeserializeObject<dynamic>(response.Content).id;

            var newPaymentRequest = new RestRequest("api/PaymentsService/Payments", Method.POST);

            newPaymentRequest.AddJsonBody(new NewPaymentViewModel
            {
                AlteringId = alterationId,
                Amount = 1000
            });

            response = client.Execute(newPaymentRequest);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, $"Actual statuscode is {response.StatusCode}");

            // We should wait here for being sure about operation that is behind Masstransit and connected to
            // RabbitMq/Azure to be done
            await Task.Delay(2000);

            var alteration = await dbContext.Alterings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == alterationId);
            Assert.IsTrue(alteration != null && alteration.State == AlteringState.Paid);

            var setOnGoingRequest = new RestRequest("api/AlteringsService/Alterings/SetOnGoing", Method.POST);

            setOnGoingRequest.AddQueryParameter("alterationId", alterationId.ToString());
            response = client.Execute(setOnGoingRequest);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, $"Actual statuscode is {response.StatusCode}");

            alteration = await dbContext.Alterings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == alterationId);
            Assert.IsTrue(alteration != null && alteration.State == AlteringState.OnGoing);

            var setSetDoneRequest = new RestRequest("api/AlteringsService/Alterings/SetDone", Method.POST);

            setSetDoneRequest.AddQueryParameter("alterationId", alterationId.ToString());
            response = client.Execute(setSetDoneRequest);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, $"Actual statuscode is {response.StatusCode}");

            alteration = await dbContext.Alterings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == alterationId);
            Assert.IsTrue(alteration != null && alteration.State == AlteringState.Done);
            removeAlterationFromDb(alterationId);
        }

        void removeAlterationFromDb(int id)
        {
            dbContext.Alterings.Remove(dbContext.Alterings.FirstOrDefault(x => x.Id == id));
            dbContext.SaveChanges();
        }
    }
}
