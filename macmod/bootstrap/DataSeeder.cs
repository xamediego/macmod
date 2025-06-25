using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using macmod.controllers.dto;
using macmod.database;
using macmod.services.entities;
using macmod.services.mappers;

namespace macmod.bootstrap;

public abstract class DataSeeder
{
    public static async Task SeedDatabase(IServiceScope serviceScope)
    {
        Console.WriteLine("Seeding Database.......");
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

        await dbContext.ClearAllAsync();
        
        await GenerateProjectTypes(dbContext);
        await GenerateGameTypes(dbContext);
        await GenerateMaps(dbContext);
    }

    private static async Task GenerateProjectTypes(DatabaseContext dbContext)
    {
        var java = new ProjectType
        {
            Type = "Java",
            Title = "Java",
            Thumbnail = "https://images.maxjonge.com/images/project-type/java/logo.png",
            Icon = "https://images.maxjonge.com/images/project-type/java/icon.png"
        };

        var udk = new ProjectType
        {
            Type = "UDK",
            Title = "Unreal Development Kit (UDK)",
            Thumbnail = "https://images.maxjonge.com/images/project-type/udk/logo.png",
            Icon = "https://images.maxjonge.com/images/project-type/udk/icon.png"
        };

        var ut3 = new ProjectType
        {
            Type = "UT3",
            Title = "Unreal Tournament 3",
            Thumbnail = "https://images.maxjonge.com/images/project-type/ut3/logo.png",
            Icon = "https://images.maxjonge.com/images/project-type/ut3/icon.png"
        };

        var ut2K4 = new ProjectType
        {
            Type = "UT2k4",
            Title = "Unreal Tournament 2004",
            Thumbnail = "https://images.maxjonge.com/images/project-type/ut2k4/logo.png",
            Icon = "https://images.maxjonge.com/images/project-type/ut2k4/icon.png"
        };
        
        await dbContext.ProjectTypes.AddRangeAsync(java, udk, ut3, ut2K4);
        await dbContext.SaveChangesAsync();
    }
    
    private static async Task GenerateGameTypes(DatabaseContext dbContext)
    {
        var allGameTypes = new List<GameType>
        {
            // UT2004
            new() { Identifier = "DM", FullName = "Deathmatch" },
            new() { Identifier = "TDM", FullName = "Team Deathmatch" },
            new() { Identifier = "CTF", FullName = "Capture the Flag" },
            new() { Identifier = "DOM", FullName = "Double Domination" },
            new() { Identifier = "BR", FullName = "Bombing Run" },
            new() { Identifier = "ONS", FullName = "Onslaught" },
            new() { Identifier = "AS", FullName = "Assault" },

            // UT3
            new() { Identifier = "VCTF", FullName = "Vehicle CTF" },
            new() { Identifier = "WAR", FullName = "Warfare" },
            new() { Identifier = "Duel", FullName = "Duel" }
        };

        await dbContext.GameTypes.AddRangeAsync(allGameTypes);
        await dbContext.SaveChangesAsync();
    }

    private static async Task GenerateMaps(DatabaseContext dbContext)
    {
        await GenerateGameMaps(dbContext,"bootstrap/data/ut2k4-maps.json", "UT2k4");
        await GenerateGameMaps(dbContext,"bootstrap/data/udk-maps.json", "UDK");
        await GenerateGameMaps(dbContext,"bootstrap/data/ut3-maps.json", "UT3");
        await GenerateProgrammingProject(dbContext,"bootstrap/data/java.json", "Java");
    }
    
    private static async Task GenerateProgrammingProject(DatabaseContext dbContext, string path, string type)
    {
        var projectType = await dbContext.ProjectTypes.FirstAsync(p => p.Type == type);
        var mapsJson = await File.ReadAllTextAsync(path);
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var mapDtos = JsonSerializer.Deserialize<List<ProgrammingProjectDto>>(mapsJson, jsonOptions);
        
        var programmingProjects = (from dto in mapDtos let project = 
                ProjectMapper.MapProjectFromDto(dto, projectType) select new ProgrammingProject { Purpose = dto.Purpose, Project = project })
            .ToList();

        await dbContext.ProgrammingProject.AddRangeAsync(programmingProjects);
        await dbContext.SaveChangesAsync();
        
        var programmingResult = dbContext.ProgrammingProject.ToList();
        
        Console.WriteLine($"Size: {programmingResult.Count}");
    }
    
    private static async Task GenerateGameMaps(DatabaseContext dbContext, string path, string type)
    {
        var projectType = await dbContext.ProjectTypes.FirstAsync(p => p.Type == type);
        var mapsJson = await File.ReadAllTextAsync(path);
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var mapDtos = JsonSerializer.Deserialize<List<GameMapDto>>(mapsJson, jsonOptions);
        var gameMaps = new List<GameMap>();

        var gameTypes = await dbContext.GameTypes.ToListAsync();
        
        foreach (var dto in mapDtos)
        {
            Console.WriteLine("Map: " + dto.Title);
            
            var gameType = await dbContext.GameTypes.FirstOrDefaultAsync(gt => gt.Identifier == dto.GameType.Identifier);
            if (gameType == null) continue;

            var project = ProjectMapper.MapProjectFromDto(dto, projectType);
            
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            
            project = await dbContext.Projects.FirstAsync(p => p.Title == project.Title);

            var map = GameMapMapper.MapGameMapFromDto(dto, gameType, project);
            
            gameMaps.Add(map);
        }

        Console.WriteLine($"To: {gameMaps.Count}");
        await dbContext.GameMaps.AddRangeAsync(gameMaps);
        await dbContext.SaveChangesAsync();

        var mapResult = await dbContext.GameMaps.Include(g => g.Project).ThenInclude(p => p.ProjectType).Where(p => p.Project.ProjectType.Type == "UT3").ToListAsync();
        
        Console.WriteLine($"Size: {mapResult.Count}");
        foreach (var gameMap in mapResult)
        {
            Console.WriteLine("map: " + gameMap.Project.Title);
        }
    }
}