
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using System.Diagnostics.CodeAnalysis;



namespace StudentApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         // modelBuilder.Entity<User>()
           // .HasOne(a => a.Transaction)
           // .WithOne(a => a.Transfer)
           // .HasForeignKey<TransferTransaction>(c => c.TransferID);
           // base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
       
    }
}