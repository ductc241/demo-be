using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WEB_API.Model
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Shipment_Status> Shipment_Status { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<Tracking_Status> Tracking_Status { get; set; }
        public DbSet<Shipment_Detail> Shipment_Details { get; set; }
        public DbSet<Shipping_Carrier> Shipping_Carrier { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Shipment_Status)
                .WithMany(st => st.Shipments)
                .HasForeignKey(s => s.Shipment_Status_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tracking>()
                .HasOne(t => t.Shipment)
                .WithMany(s => s.Trackings)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Tracking>()
            //    .HasOne(t => t.Tracking_Status)
            //    .WithMany(ts => ts.Trackings)
            //    .HasForeignKey(t => t.Tracking_Status_Id);

            //modelBuilder.Entity<Shipping_Carrier>()
            //    .HasMany(sc => sc.Shipment_Detail)
            //    .WithOne(sd => sd.Shipping_Carrier)
            //    .HasForeignKey(sd => sd.Shipping_Carrier_Id);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
