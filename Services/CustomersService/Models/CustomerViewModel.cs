namespace CustomersService.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int UnpaidAlterings { get; set; }
        public int OverallAlterings { get; set; }
    }
}
