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

    public SpendoContext(DbContextOptions<SpendoContext> options)
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
    {
        if (optionsBuilder.IsConfigured) return;
        
        // Dynamically build the connection string
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var database = Environment.GetEnvironmentVariable("DB_DATABASE");
        var username = Environment.GetEnvironmentVariable("DB_USERNAME");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var sslMode = Environment.GetEnvironmentVariable("DB_SSL_MODE");
        var trustServerCert = Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERT");

        var connectionString = $"Host={host};Database={database};Username={username};Password={password};SSL Mode={sslMode};Trust Server Certificate={trustServerCert}";
        optionsBuilder.UseNpgsql(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('account_id_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_userid_fk");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Budget_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('budget_id_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Category).WithMany(p => p.Budgets).HasConstraintName("budget_categoryid_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("budget_userid_fk");
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
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");
            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Account).WithMany(p => p.Expenses).HasConstraintName("expense_accountid_fk");

            entity.HasOne(d => d.Category).WithMany(p => p.Expenses).HasConstraintName("expense_categoryid_fk");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Income_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('income_id_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");
            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Account).WithMany(p => p.Incomes).HasConstraintName("income_accountid_fk");

            entity.HasOne(d => d.Category).WithMany(p => p.Incomes).HasConstraintName("income_categoryid_fk");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transfer_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('transfer_id_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");
            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Category).WithMany(p => p.Transfers).HasConstraintName("transfer_categoryid_fk");

            entity.HasOne(d => d.Sourceaccount).WithMany(p => p.TransferSourceaccounts).HasConstraintName("transfer_sourceaccountid_fk");

            entity.HasOne(d => d.Targetaccount).WithMany(p => p.TransferTargetaccounts).HasConstraintName("transfer_targetaccountid_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('user_id_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Currency).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_currencyid_fk");
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
