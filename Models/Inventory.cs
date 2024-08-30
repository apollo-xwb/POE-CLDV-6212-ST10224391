using Microsoft.Azure.Cosmos.Table;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class Inventory : TableEntity
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}
