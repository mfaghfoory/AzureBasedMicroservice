using Shared;

namespace AlteringsRegistrationService.Queries
{
    public class GetCustomersById : IRequest
    {
        public int CustomerId { get; set; }
        public GetCustomersById(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
