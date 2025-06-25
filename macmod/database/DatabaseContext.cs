using macmod.services.entities;
using Microsoft.EntityFrameworkCore;

namespace macmod.database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Entity Relations
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
        
        // Entity Unique Constraints
        builder.Entity<ProjectType>()
            .HasIndex(pt => pt.Type)
            .IsUnique();
        builder.Entity<ProjectType>()
            .HasIndex(pt => pt.Title)
            .IsUnique();
        
        builder.Entity<Project>()
            .HasIndex(pt => pt.Title)
            .IsUnique();

        builder.Entity<GameType>()
            .HasIndex(pt => pt.Identifier)
            .IsUnique();
        builder.Entity<GameType>()
            .HasIndex(pt => pt.FullName)
            .IsUnique();
    }

    public DbSet<ProjectType> ProjectTypes { get; set; } = null!;

    public DbSet<Project> Projects { get; set; } = null!;

    public DbSet<GameMap> GameMaps { get; set; } = null!;

    public DbSet<GameType> GameTypes { get; set; } = null!;

    public DbSet<ProgrammingProject> ProgrammingProject { get; set; } = null!;

    public DbSet<Image> Images { get; set; } = null!;
    
    public async Task ClearAllAsync()
    {
        // Delete children first due to foreign key dependencies
        Images.RemoveRange(Images);
        ProgrammingProject.RemoveRange(ProgrammingProject);
        GameMaps.RemoveRange(GameMaps);
        Projects.RemoveRange(Projects);
        ProjectTypes.RemoveRange(ProjectTypes);
        GameTypes.RemoveRange(GameTypes);

        await SaveChangesAsync();
    }

}