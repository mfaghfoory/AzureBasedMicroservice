using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Common;
using System.Collections.Generic;

namespace AzureBasedMicroservice.EntityFramework.Customers
{
    public class Customer : BaseEntity<int>
    {
        public string FullName { get; set; }

        public ICollection<Altering> Alterings { get; set; }
    }
}
