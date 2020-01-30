namespace AzureBasedMicroservice.Shared.CQRS
{
    public class MassTransitConfig
    {
        public bool UseRabbit { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
