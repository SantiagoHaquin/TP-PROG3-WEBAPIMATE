using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext 
    {
        

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }
        public DbSet<Product> Products { get; set; }
        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<SysAdmin>("Admin")
                .HasValue<Seller>("Seller");

            


            base.OnModelCreating(modelBuilder);
        }
    }
}
