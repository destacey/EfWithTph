using EfWithTph.Enums;
using EfWithTph.Models;
using Microsoft.EntityFrameworkCore;

namespace EfWithTph.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Roadmap> Roadmaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roadmap>(entity =>
        {
            entity.ToTable("Roadmaps");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .IsRequired();

            entity.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(e => e.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<BaseRoadmapItem>(entity =>
        {
            entity.ToTable("RoadmapItems");

            entity.HasKey(e => e.Id);

            entity.HasDiscriminator(e => e.Type)
                .HasValue<RoadmapActivity>(RoadmapItemType.Activity)
                .HasValue<RoadmapMilestone>(RoadmapItemType.Milestone);

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(1024);

            entity.Property(e => e.Type);

            entity.HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId);
        });

        modelBuilder.Entity<RoadmapActivity>(entity =>
        {
            entity.Property(e => e.Start)
                .HasColumnName("Start")
                .IsRequired();

            entity.Property(e => e.End)
                .HasColumnName("End")
                .IsRequired();

            entity.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<RoadmapMilestone>(entity =>
        {
            entity.Property(e => e.Date)
                .HasColumnName("Start")
                .IsRequired();
        });
    }
}
