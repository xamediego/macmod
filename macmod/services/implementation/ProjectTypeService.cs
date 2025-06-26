using macmod.controllers.dto;
using macmod.database;
using macmod.services.interfaces;
using macmod.services.mappers;
using Microsoft.EntityFrameworkCore;

namespace macmod.services.implementation;

public class ProjectTypeService(DatabaseContext database) : IProjectTypeService
{
    public async Task<ProjectTypeDto[]> FindAllAsync()
    {
        return await database.ProjectTypes.Include(pt => pt.Projects)
            .Select(pt => ProjectTypeMapper.MapDtoFromProjectType(pt)).ToArrayAsync();
    }

    public async Task<ProjectTypeDto?> FindByTypeAsync(string type)
    {
        var projectType = await database.ProjectTypes.FirstOrDefaultAsync(pt => pt.Type == type);

        return projectType != null ? ProjectTypeMapper.MapDtoFromProjectType(projectType) : null;
    }

    public async Task<ProjectTypeDto[]> FindAllCompleteAsync()
    {
        return await database.ProjectTypes
            .Include(pt => pt.Projects)
            .ThenInclude(project => project.Images)
            .Include(pt => pt.Projects)
            .ThenInclude(project => project.Links)
            .Select(pt => ProjectTypeMapper.MapDtoFromProjectType(pt))
            .ToArrayAsync();
    }

    public async Task<ProjectTypeDto?> FindByTypeCompleteAsync(string type)
    {
        var projectType = await database.ProjectTypes
            .Where(pt => pt.Type == type)
            .Include(pt => pt.Projects)
            .ThenInclude(project => project.Images)
            .Include(pt => pt.Projects)
            .ThenInclude(project => project.Links)
            .FirstOrDefaultAsync();
        
        return projectType != null ? ProjectTypeMapper.MapDtoFromProjectType(projectType) : null;
    }
}