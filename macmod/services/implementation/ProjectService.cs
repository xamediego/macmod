using macmod.controllers.dto;
using macmod.database;
using macmod.services.interfaces;
using macmod.services.mappers;
using Microsoft.EntityFrameworkCore;

namespace macmod.services.implementation;

public class ProjectService(DatabaseContext database) : IProjectService
{
    public async Task<ProjectDto[]> FindAllAsync()
    {
        return await database.Projects
            .Include(project => project.Links)
            .Include(project => project.Images)
            .Select(project => ProjectMapper.MapDtoFromProject(project))
            .ToArrayAsync();
    }

    public async Task<ProjectDto?> FindByTitleAsync(string name)
    {
        var project =  await database.Projects
            .Where(p => p.Title == name)
            .Include(project => project.Links)
            .Include(project => project.Images)
            .FirstOrDefaultAsync();

        return project != null ? ProjectMapper.MapDtoFromProject(project) : null;
    }

    public async Task<ProjectDto[]> FindByProjectTypeAsync(string type)
    {
        var projects = await database.Projects
            .Include(project => project.ProjectType)
            .Where(project => project.ProjectType.Type == type)
            .ToArrayAsync();

        return projects.Select(ProjectMapper.MapDtoFromProject).ToArray();
    }
}