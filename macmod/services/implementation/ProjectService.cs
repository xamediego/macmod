using macmod.controllers.dto;
using macmod.database;
using macmod.services.entities;
using macmod.services.Enums;
using macmod.services.interfaces;
using macmod.services.mappers;
using Microsoft.EntityFrameworkCore;

namespace macmod.services.implementation;

public class ProjectService(DatabaseContext database, IBlobService blobService) : IProjectService
{
    public async Task<ProjectDto[]> FindAllAsync()
    {
        var projects = await database.Projects
            .Include(p => p.Links)
            .Include(p => p.Images)
            .ToArrayAsync();

        return await MapProjectsToDtosAsync(projects);
    }

    public async Task<ProjectDto?> FindByTitleAsync(string name)
    {
        var project = await database.Projects
            .Include(p => p.Links)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Title == name);

        return project is null ? null : await MapProjectToDtoAsync(project);
    }

    public async Task<ProjectDto[]> FindByProjectTypeAsync(string type)
    {
        var projects = await database.Projects
            .Include(p => p.ProjectType)
            .Include(p => p.Links)
            .Include(p => p.Images)
            .Where(p => p.ProjectType != null && p.ProjectType.Type == type)
            .ToArrayAsync();

        return await MapProjectsToDtosAsync(projects);
    }

    public async Task<ProjectDto[]> FindFeaturedAsync()
    {
        var projects = await database.Projects
            .Where(p => p.IsFeatured)
            .Include(p => p.Images)
            .ToArrayAsync();
        
        return await MapProjectsToDtosAsync(projects);
    }

    public async Task<DownloadResult?> DownloadAsync(long projectId)
    {
        var project = await database.Projects
            .Where(project => project.Id == projectId)
            .Include(project => project.ProjectType)
            .FirstOrDefaultAsync();

        if (project?.ProjectType == null) return null;

        var extraPath = await GetExtraPath(project);
        
        var container = "files";
        var path = $"project/{project.ProjectType.Type.ToLower()}/{extraPath.ToLower()}{project.FileName}";
        
        var blobClientResult = await blobService.DownloadAsync(path, container);
        var properties = await blobClientResult.GetPropertiesAsync();
        
        var stream = await blobClientResult.OpenReadAsync();
        
        return new DownloadResult
        {
            FileName = project.FileName,
            FileStream = stream,
            ContentType = properties.Value.ContentType
        };
    }

    private async Task<ProjectDto[]> MapProjectsToDtosAsync(IEnumerable<Project> projects)
    {
        var dtoTasks = projects.Select(MapProjectToDtoAsync);
        return await Task.WhenAll(dtoTasks);
    }

    private async Task<ProjectDto> MapProjectToDtoAsync(Project project)
    {
        var dto = ProjectMapper.MapDtoFromProject(project);
        dto.ExtraDetails = await GetExtraDetailsAsync(project);
        return dto;
    }

    private async Task<KeyValuePair<string, string>[]> GetExtraDetailsAsync(Project project)
    {
        return project.ProjectSubType switch
        {
            ProjectSubType.GAMEMAP => await MapGameMapDetailsAsync(project.Id),
            ProjectSubType.PROGRAMMINGPROJECT => await MapProgrammingProjectDetailsAsync(project.Id),
            _ => []
        };
    }

    private async Task<KeyValuePair<string, string>[]> MapGameMapDetailsAsync(long projectId)
    {
        var gameMap = await database.GameMaps
            .Include(g => g.GameType)
            .FirstOrDefaultAsync(g => g.ProjectId == projectId);

        return gameMap is null
            ? []
            :
            [
                new KeyValuePair<string, string>("Players", $"{gameMap.MinimumPlayer} - {gameMap.MaximumPlayer}"),
                new KeyValuePair<string, string>("Environment", gameMap.MapEnvironment.ToString()),
                new KeyValuePair<string, string>("GameType", gameMap.GameType?.FullName ?? "")
            ];
    }

    private async Task<KeyValuePair<string, string>[]> MapProgrammingProjectDetailsAsync(long projectId)
    {
        var programmingProject = await database.ProgrammingProject
            .FirstOrDefaultAsync(pp => pp.ProjectId == projectId);

        return programmingProject is null
            ? []
            :
            [
                new KeyValuePair<string, string>("Purpose", programmingProject.Purpose)
            ];
    }
    
    
    private async Task<string> GetExtraPath(Project project)
    {
        return project.ProjectSubType switch
        {
            ProjectSubType.GAMEMAP => await GetExtraGamePath(project.Id),
            ProjectSubType.PROGRAMMINGPROJECT => "",
            _ => ""
        };
    }

    private async Task<string> GetExtraGamePath(long projectId)
    {
        var gameMap = await database.GameMaps.Where(g => g.ProjectId == projectId).Include(g => g.GameType).FirstOrDefaultAsync();
        return gameMap is { GameType: not null } ? (gameMap.GameType.Identifier + "/") : "";
    }
}