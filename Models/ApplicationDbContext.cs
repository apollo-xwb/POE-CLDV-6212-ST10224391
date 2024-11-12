using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace POE_CLDV_6221_ST10224391.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Add DbSet for the SubmittingToDatabase model
        public DbSet<SubmittingToDatabase> SubmittingToDatabases { get; set; }
    }
}
