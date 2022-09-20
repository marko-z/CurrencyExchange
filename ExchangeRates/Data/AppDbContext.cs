using ExchangeRates.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Rate> Rates { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //    optionsbuilder.UseSqlServer(@"Server=(localdb)\\MSQSQLLLocalDB; Initial Catalog=testCatalog; ");
        //}
    }

}