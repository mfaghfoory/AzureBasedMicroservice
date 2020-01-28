using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureBasedMicroservice.EntityFramework.Customers
{
    public class Payment : BaseEntity<int>
    {
        public int AlteringId { get; set; }

        public string TrackingCode { get; set; }

        [ForeignKey(nameof(AlteringId))]
        public Altering Altering { get; set; }
    }
}
