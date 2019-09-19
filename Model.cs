using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Model
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Movement> Movements { get; set; }

        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }
    }

    public class Transaction
    {
        [Key]
        public string TransactionNumber { get; set; }
        public virtual List<Movement> Movements { get; set; }
    }

    public class Movement
    {
        public int MovementId { get; set; }
        public string TransactionNumber { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}