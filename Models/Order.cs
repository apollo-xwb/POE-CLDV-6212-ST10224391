
using Microsoft.Azure.Cosmos.Table;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class Order : TableEntity
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
