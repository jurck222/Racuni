using Microsoft.EntityFrameworkCore;
using Racuni.Models;

namespace Racuni.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {

        }

        public DbSet<InvoicesHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }


    }
}
