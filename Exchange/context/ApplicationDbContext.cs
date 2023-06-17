using Microsoft.EntityFrameworkCore;
using Exchange.Models;

namespace Exchange.context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    public DbSet<Exchange.Models.Rates>? Rates { get; set; }
    public DbSet<Exchange.Models.ExchangeForm>? ExchangeForm { get; set; }
}