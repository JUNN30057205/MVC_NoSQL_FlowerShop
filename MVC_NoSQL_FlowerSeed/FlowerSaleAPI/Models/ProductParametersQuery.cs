namespace FlowerSaleAPI.Models
{
    //Implement pagenation Query Parameters
    public class ProductParametersQuery : QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set;}

        //Sku Stock Keeping Unit = Unique string of letters and numbers 
        public string Sku { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string SearchTerm { get; set; } = String.Empty;

        public bool? IsAvailable { get; set; }

        public string storeLocation { get; set; } = String.Empty;

        public int? PostCode { get; set; } 
    }
}
