using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Customers;
using Microsoft.EntityFrameworkCore;

namespace AzureBasedMicroservice.EntityFramework.DBContext
{
    public class AzureBasedMicroserviceContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Altering> Alterings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
