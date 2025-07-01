using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.interfaces;

public interface IProjectService
{
    Task<ProjectDto[]> FindAllAsync();
    
    Task<ProjectDto?> FindByTitleAsync(string title);
    
    Task<ProjectDto[]> FindByProjectTypeAsync(string type);
    
    Task<ProjectDto[]> FindFeaturedAsync();

    Task<DownloadResult?> DownloadAsync(long projectId);
}