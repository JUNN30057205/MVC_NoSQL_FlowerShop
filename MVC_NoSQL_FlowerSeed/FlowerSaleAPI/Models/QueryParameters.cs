namespace FlowerSaleAPI.Models
{
    public class QueryParameters
    {
        const int MaxSize = 100;
        //size of my page size
        private int _pageSize = 50;

        public int Page { get; set; } = 1;

        public int Size
        {
            get { return _pageSize; }
            set 
            {
                _pageSize = Math.Min(_pageSize, value);
            }
        }

        public string sortBy { get; set; } = "Id";
        private string sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                if(value == "asc" || value == "desc")
                {
                    sortOrder = value;
                }
            }
        }
    }
}
