using macmod.services.entities;
using Microsoft.EntityFrameworkCore;

namespace macmod.database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProjectType>()
            .HasMany(pt => pt.Projects)
            .WithOne(p => p.ProjectType)
            .HasForeignKey(p => p.ProjectTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Project>()
            .HasMany(project => project.Links)
            .WithOne(link => link.Project)
            .HasForeignKey(link => link.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Project>()
            .HasOne(project => project.GameMap)
            .WithOne(gameMap => gameMap.Project)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Project>()
            .HasOne(project => project.ProgrammingProject)
            .WithOne(pp => pp.Project)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<GameType>()
            .HasMany(gameType => gameType.GameMaps)
            .WithOne(gameMap => gameMap.GameType)
            .HasForeignKey(gameMap => gameMap.GameTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<ProjectType> ProjectTypes { get; set; } = default!;

    public DbSet<Project> Projects { get; set; } = default!;

    public DbSet<GameMap> GameMaps { get; set; } = default!;

    public DbSet<GameType> GameTypes { get; set; } = default!;

    public DbSet<ProgrammingProject> ProgrammingProject { get; set; } = default!;

    public DbSet<Image> Images { get; set; } = default!;
}