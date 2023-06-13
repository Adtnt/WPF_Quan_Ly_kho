using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.DAL
{
    public class StorageContext : DbContext
    {
        public StorageContext() : base("StorageContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StorageContext,WPF_Quan_Ly_kho.Migrations.Configuration>("StorageContext"));
        }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Suplier> Supliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<InputInfo> InputInfos { get; set; }
        public DbSet<OutputInfo> OutputInfos { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>()
            .HasRequired<Unit>(s => s.Unit)
            .WithMany(g => g.Materials)
            .HasForeignKey<int>(s => s.IDUnit);

            modelBuilder.Entity<Material>()
           .HasRequired<Suplier>(s => s.Suplier)
           .WithMany(g => g.Materials)
           .HasForeignKey<int>(s => s.IDSuplier);

            modelBuilder.Entity<InputInfo>()
           .HasRequired<Input>(s => s.Input)
           .WithMany(g => g.InputInfos)
           .HasForeignKey<int>(s => s.IDInput);

           // modelBuilder.Entity<InputInfo>()
           //.HasRequired<Material>(s => s.Material)
           //.WithMany(g => g.InputInfos)
           //.HasForeignKey<int>(s => s.IDMaterial);

           // modelBuilder.Entity<OutputInfo>()
           //.HasRequired<Material>(s => s.Material)
           //.WithMany(g => g.OutputInfos)
           //.HasForeignKey<int>(s => s.IDMaterial);

            modelBuilder.Entity<OutputInfo>()
           .HasRequired<Customer>(s => s.Customer)
           .WithMany(g => g.OutputInfos)
           .HasForeignKey<int>(s => s.IDCustomer);

            modelBuilder.Entity<OutputInfo>()
           .HasRequired<Output>(s => s.Output)
           .WithMany(g => g.OutputInfos)
           .HasForeignKey<int>(s => s.IDOutput);
        }
    }
}
