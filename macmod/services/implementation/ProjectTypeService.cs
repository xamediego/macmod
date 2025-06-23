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
        return await database.ProjectTypes.Select(pt => ProjectTypeMapper.MapDtoFromProjectType(pt)).ToArrayAsync();
    }

    public async Task<ProjectTypeDto?> FindByTypeAsync(string type)
    {
        var projectType = await database.ProjectTypes.FirstOrDefaultAsync(pt => pt.Type == type);

        return projectType != null ? ProjectTypeMapper.MapDtoFromProjectType(projectType) : null;
    }

    public async Task<ProjectTypeDto[]> FindAllCompleteAsync()
    {
        return await database.ProjectTypes
            .Include(type => type.Projects)
            .ThenInclude(project => project.Images)
            .Include(type => type.Projects)
            .ThenInclude(project => project.Links)
            .Select(pt => ProjectTypeMapper.MapDtoFromProjectType(pt))
            .ToArrayAsync();
    }
}