using macmod.controllers.dto;

namespace macmod.services.interfaces;

public interface IProjectTypeService
{
    Task<ProjectTypeDto[]> FindAllAsync();
    
    Task<ProjectTypeDto?> FindByTypeAsync(string type);
    
    Task<ProjectTypeDto[]> FindAllCompleteAsync();
    
    Task<ProjectTypeDto?> FindByTypeCompleteAsync(string type);
}