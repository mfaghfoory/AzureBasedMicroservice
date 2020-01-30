namespace CustomersService.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int PaidAlterings { get; set; }
        public int OverallAlterings { get; set; }
        public int DoneAlterings { get; internal set; }
        public int OnGoingAlterings { get; internal set; }
        public int InitialAlterings { get; internal set; }
    }
}
