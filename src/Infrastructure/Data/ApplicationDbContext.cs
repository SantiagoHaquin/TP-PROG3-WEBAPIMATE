using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<SysAdmin>("Admin")
                .HasValue<Seller>("Seller");

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    UserName = "Cueton",
                    Email = "cueton@example.com",
                    UserType = "Client",
                    Password = "Cueton912"
                }
            );

            modelBuilder.Entity<SysAdmin>().HasData(
                new SysAdmin
                {
                    Id = 2,
                    UserName = "admin",
                    Email = "admin@example.com",
                    UserType = "Admin",
                    Password = "Admin123"
                }
            );

            modelBuilder.Entity<Seller>().HasData(
                new Seller
                {
                    Id = 3,
                    UserName = "Miguel",
                    Email = "miguel@example.com",
                    UserType = "Seller",
                    Password = "Miguelito3520"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "MATE STANLEY", Price = 35000, StockAvailable = 6, Category = "Mates", SellerId = 3 },
                new Product { Id = 2, Name = "TERMO LUMILAGRO", Price = 20000, StockAvailable = 1, Category = "Termos", SellerId = 3 },
                new Product { Id = 3, Name = "MOCHILA MATERA DE CUERO", Price = 25000, StockAvailable = 5, Category = "Materas", SellerId = 3 },
                new Product { Id = 4, Name = "BOMBILLA", Price = 6500, StockAvailable = 3, Category = "Bombillas", SellerId = 3 }
            );

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartProduct",
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j => j.HasOne<Cart>().WithMany().HasForeignKey("CartId")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
