using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerSaleAPI.Models
{
    public class Product
    { 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string StoreLocation { get; set; } = string.Empty;
        [Required]
        public int PostCode { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
