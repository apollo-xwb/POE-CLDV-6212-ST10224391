using Microsoft.Azure.Cosmos.Table;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class Product : TableEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}

