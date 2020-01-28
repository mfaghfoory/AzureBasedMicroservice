namespace AlteringsRegistrationService.Queries
{
    public class GetCustomersById
    {
        public int CustomerId { get; set; }
        public GetCustomersById(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
