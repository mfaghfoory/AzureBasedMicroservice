using System.ComponentModel.DataAnnotations;

namespace AzureBasedMicroservice.EntityFramework.Common
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
