using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerSaleAPI.Models
{
    public class Product
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string StoreLocation { get; set; }

        public int PostCode { get; set; }

        public Decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        [Required]

        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
