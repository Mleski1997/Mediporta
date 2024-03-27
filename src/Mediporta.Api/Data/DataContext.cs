using Mediporta.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          



        }
    }
}