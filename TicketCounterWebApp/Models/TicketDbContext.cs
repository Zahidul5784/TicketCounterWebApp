using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TicketCounterWebApp.Models
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<TicketInfo> TicketInfos { get; set; }


        public TicketDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
