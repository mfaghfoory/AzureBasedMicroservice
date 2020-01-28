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
        public string Value { get; set; }

        /// <summary>
        /// If true, the value will be added to previous size and vice versa
        /// </summary>
        public bool IsIncrease { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
