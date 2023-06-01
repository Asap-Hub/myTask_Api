using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using myShop.Domain.Model;
using myShop.Infrastructure.Utility;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace myShop.Infrastructure.Persistence.Data;

public partial class MyShopContext : DbContext
{
    private readonly IOptions<ConnectionString> _connectionString;

    protected IHttpContextAccessor _httpContextAccessor { get; set; }
    //public MyShopContext()
    //{
    //}

    public MyShopContext(DbContextOptions<MyShopContext> options, IHttpContextAccessor httpContextAccessor, IOptions<ConnectionString> connectinString)
        : base(options)
    {
        _connectionString = connectinString;
        _httpContextAccessor = httpContextAccessor; 
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }
    public virtual DbSet<TblMyTodo> TblMyTodos { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("");

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
            entity.HasKey(e => e.TodoId);
            entity.ToTable("tblMyTodo");
        });
         
    } 
}
