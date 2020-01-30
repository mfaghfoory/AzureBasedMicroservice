using AzureBasedMicroservice.EntityFramework.Common;
using AzureBasedMicroservice.EntityFramework.Customers;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureBasedMicroservice.EntityFramework.Alterings
{
    public class Altering : BaseEntity<int>
    {
        public int CustomerId { get; set; }

        /// <summary>
        /// State of the altering. Initial/Paid
        /// </summary>
        public AlteringState State { get; set; }

        /// <summary>
        /// Operation of altering. ShortenSleeves/ShortenTrousers
        /// </summary>
        public AlteringOperate Operation { get; set; }

        /// <summary>
        /// Direction of altering. Left/Right/Both
        /// </summary>
        public AlteringDirection Direction { get; set; }

        /// <summary>
        /// Value of altering. e.g: 5cm
        /// </summary>
        public int Value { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
