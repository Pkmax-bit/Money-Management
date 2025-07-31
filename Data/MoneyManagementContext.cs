using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Money_Management.Data;

public partial class MoneyManagementContext : DbContext
{
    public MoneyManagementContext()
    {
    }

    public MoneyManagementContext(DbContextOptions<MoneyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-0U55SOV\\SQLEXPRESS;Initial Catalog=MoneyManagement;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Idcategory).HasName("PK__category__113A1A819E8C2383");

            entity.ToTable("category");

            entity.Property(e => e.Ghichu).HasColumnName("ghichu");
            entity.Property(e => e.Kichhoat)
                .HasDefaultValue(true)
                .HasColumnName("kichhoat");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__category__Iduser__3C69FB99");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Iddetail).HasName("PK__detail__BA5149B41CEDC524");

            entity.ToTable("detail");

            entity.Property(e => e.Ghichu).HasColumnName("ghichu");
            entity.Property(e => e.Giatien)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("giatien");
            entity.Property(e => e.Gio).HasColumnName("gio");
            entity.Property(e => e.Ngay).HasColumnName("ngay");
            entity.Property(e => e.Thu)
                .HasMaxLength(20)
                .HasColumnName("thu");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.Idcategory)
                .HasConstraintName("FK__detail__Idcatego__4BAC3F29");

            entity.HasOne(d => d.IdexpenseNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.Idexpense)
                .HasConstraintName("FK__detail__Idexpens__4AB81AF0");

            entity.HasOne(d => d.IdrelationshipNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.Idrelationship)
                .HasConstraintName("FK__detail__Idrelati__4CA06362");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Idexpense).HasName("PK__expense__F713EEB40E6D662C");

            entity.ToTable("expense");

            entity.Property(e => e.Denngay).HasColumnName("denngay");
            entity.Property(e => e.Notification)
                .HasDefaultValue(false)
                .HasColumnName("notification");
            entity.Property(e => e.Tonggia)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("tonggia");
            entity.Property(e => e.Tuan).HasColumnName("tuan");
            entity.Property(e => e.Tungay).HasColumnName("tungay");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__expense__Iduser__4316F928");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Idnotification).HasName("PK__notifica__A0705CD03AD6EF62");

            entity.ToTable("notification");

            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngaytao");
            entity.Property(e => e.Noidung).HasColumnName("noidung");

            entity.HasOne(d => d.IdexpenseNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Idexpense)
                .HasConstraintName("FK__notificat__Idexp__47DBAE45");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__notificat__Iduse__46E78A0C");
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => e.Idrelationship).HasName("PK__relation__00A46CF8CCCD852F");

            entity.ToTable("relationship");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("hoten");
            entity.Property(e => e.Relationship1)
                .HasMaxLength(100)
                .HasColumnName("relationship");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("sdt");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Relationships)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__relations__Iduse__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__user__2A50F1CED8FD7ADA");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164597CDE07").IsUnique();

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("hoten");
            entity.Property(e => e.Kichhoat)
                .HasDefaultValue(true)
                .HasColumnName("kichhoat");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Random)
                .HasMaxLength(50)
                .HasColumnName("random");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("sdt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
