using Microsoft.EntityFrameworkCore;

namespace TacoStand.Models;

public partial class TacoDbContext : DbContext, ITacoDb
{
    public TacoDbContext()
    {
    }

    public TacoDbContext(DbContextOptions<TacoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Taco> Tacos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=TacoDB; Integrated Security=SSPI;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Combo__3214EC2772402847");

            entity.ToTable("Combo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Drink).WithMany(p => p.Combos)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK__Combo__DrinkId__5DCAEF64");

            entity.HasOne(d => d.Taco).WithMany(p => p.Combos)
                .HasForeignKey(d => d.TacoId)
                .HasConstraintName("FK__Combo__TacoId__5CD6CB2B");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drink__3214EC2795DDAF43");

            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Taco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Taco__3214EC276AE36808");

            entity.ToTable("Taco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
