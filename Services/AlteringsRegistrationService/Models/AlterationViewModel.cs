namespace AlteringsRegistrationService.Models
{
    public class AlterationViewModel
    {
        public int Id { get; set; }

        public string State { get; set; }

        public string Operation { get; set; }

        public string Direction { get; set; }

        public string Value { get; set; }

        public bool IsIncrease { get; set; }
    }
}
