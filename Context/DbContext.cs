using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using spendo_be.Models;

namespace spendo_be.Context;

public partial class SpendoContext : DbContext
{
    public SpendoContext()
    {
    }

    public SpendoContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-soft-fog-a1kh8cnz.ap-southeast-1.aws.neon.tech;Database=spendo;Username=spendo_owner;Password=wLDdF0b3QRsV;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('account_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Budget_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('budget_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('category_id_seq'::regclass)");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Currency_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('currency_id_seq'::regclass)");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Expense_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('expense_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Income_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('income_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transfer_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('transfer_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('user_id_seq'::regclass)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.HasSequence("account_id_seq");
        modelBuilder.HasSequence("budget_id_seq");
        modelBuilder.HasSequence("category_id_seq");
        modelBuilder.HasSequence("currency_id_seq");
        modelBuilder.HasSequence("expense_id_seq");
        modelBuilder.HasSequence("income_id_seq");
        modelBuilder.HasSequence("transfer_id_seq");
        modelBuilder.HasSequence("user_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
