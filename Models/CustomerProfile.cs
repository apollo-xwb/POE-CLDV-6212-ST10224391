using Microsoft.Azure.Cosmos.Table;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class CustomerProfile : TableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
