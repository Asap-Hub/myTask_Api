using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using myShop.Domain.Model;

namespace myShop.Infrastructure.Persistence.Data;

public partial class MyShopContext : DbContext
{
    public MyShopContext()
    {
    }

    public MyShopContext(DbContextOptions<MyShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.; Database=myShop; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblAccount");

            entity.Property(e => e.ConfirmPassword).HasColumnName("confirmPassword");
            entity.Property(e => e.CountryName)
                .HasMaxLength(255)
                .HasColumnName("countryName");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstName");
            entity.Property(e => e.FirstPassword).HasColumnName("firstPassword");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.SecondName)
                .HasMaxLength(255)
                .HasColumnName("secondName");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");
        });


        modelBuilder.Entity<TblMyTodo>(entity =>
        {
            entity.HasNoKey().ToTable("tblMyTodo");
            entity.Property(e => e.TodoName).HasColumnName("TodoName");
            entity.Property(e => e.StartTime).HasColumnName("StartTime");
            entity.Property(e => e.EndTime).HasColumnName("EndTime");
            entity.Property(e => e.Note).HasMaxLength(255).HasColumnName("Note");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
