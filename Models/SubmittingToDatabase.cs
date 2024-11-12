using System.ComponentModel.DataAnnotations;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class SubmittingToDatabase
    {
        public int Id { get; set; }  // Primary key for SQL Database

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
