using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace DemoExam1.DB;

public partial class ConferenceContext : DbContext
{
    private static ConferenceContext instance;

    public ConferenceContext()
    {
    }

    public ConferenceContext(DbContextOptions<ConferenceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderuserlist> Orderuserlists { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlist> Userlists { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=Conference; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("order_pk");

            entity.ToTable("order");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Amountguests).HasColumnName("amountguests");
            entity.Property(e => e.Datecreation)
                .HasColumnType("date")
                .HasColumnName("datecreation");
            entity.Property(e => e.Equipment)
                .HasMaxLength(50)
                .HasColumnName("equipment");
            entity.Property(e => e.Nameconference)
                .HasMaxLength(50)
                .HasColumnName("nameconference");
            entity.Property(e => e.Orderstatus)
                .HasMaxLength(50)
                .HasColumnName("orderstatus");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(50)
                .HasColumnName("paymentstatus");
        });

        modelBuilder.Entity<Orderuserlist>(entity =>
        {
            entity.HasKey(e => e.Orderuserlistid).HasName("orderuserlist_pk");

            entity.ToTable("orderuserlist");

            entity.Property(e => e.Orderuserlistid).HasColumnName("orderuserlistid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderuserlists)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderuserlist___fk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Orderuserlists)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderuserlist___fk");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Shiftid).HasName("shift_pk");

            entity.ToTable("shift");

            entity.Property(e => e.Shiftid).HasColumnName("shiftid");
            entity.Property(e => e.Dateend)
                .HasColumnType("date")
                .HasColumnName("dateend");
            entity.Property(e => e.Datestart)
                .HasColumnType("date")
                .HasColumnName("datestart");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Userroleid).HasColumnName("userroleid");

            entity.HasOne(d => d.Userrole).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userroleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user___fk");
        });

        modelBuilder.Entity<Userlist>(entity =>
        {
            entity.HasKey(e => e.Userlistid).HasName("userlist_pk");

            entity.ToTable("userlist");

            entity.Property(e => e.Userlistid).HasColumnName("userlistid");
            entity.Property(e => e.Shiftid).HasColumnName("shiftid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Shift).WithMany(p => p.Userlists)
                .HasForeignKey(d => d.Shiftid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userlist___fk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userlists)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userlist___fk");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.Userroleid).HasName("userrole_pk");

            entity.ToTable("userrole");

            entity.Property(e => e.Userroleid).HasColumnName("userroleid");
            entity.Property(e => e.Namerole)
                .HasMaxLength(50)
                .HasColumnName("namerole");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public static ConferenceContext Instance()
    {
        if (instance == null)
        {
            instance = new ConferenceContext();
        }
        return instance;
    }

}
