using macmod.controllers.dto;

namespace macmod.services.interfaces;

public interface IProjectService
{
    Task<ProjectDto[]> FindAllAsync();
    
    Task<ProjectDto?> FindByTitleAsync(string title);
    
    Task<ProjectDto[]> FindByProjectTypeAsync(string type);
}