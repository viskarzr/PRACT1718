using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRACT1718.ModelsBD;

public partial class OptStoreContext : DbContext
{
    public OptStoreContext()
    {
    }

    public OptStoreContext(DbContextOptions<OptStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-8DPHJN5P\\SQLEXPRESS; Database=OptStore; User=sa; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameFirm).HasMaxLength(50);
            entity.Property(e => e.NameTovar).HasMaxLength(50);
            entity.Property(e => e.NumberPart).HasMaxLength(20);
            entity.Property(e => e.PriceSold).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PriceUnit).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserLogin).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPassword).HasMaxLength(50);
            entity.Property(e => e.UserPatronymic).HasMaxLength(50);
            entity.Property(e => e.UserSurName).HasMaxLength(50);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
