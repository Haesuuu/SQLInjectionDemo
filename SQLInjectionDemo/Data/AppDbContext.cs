using Microsoft.EntityFrameworkCore;
using SQLInjectionDemo.Models;
using System.Reflection.Emit;

namespace SQLInjectionDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.RoleID);

            entity.Property(r => r.RoleName)
                .HasMaxLength(20)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserID);

            entity.Property(u => u.FullName)
                .HasMaxLength(50);

            entity.Property(u => u.EmailAdd)
                .HasMaxLength(150);

            entity.Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Password)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(u => u.Bio)
                .HasColumnType("nvarchar(max)");

            entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

