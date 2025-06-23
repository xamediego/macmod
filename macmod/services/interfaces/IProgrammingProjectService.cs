using macmod.controllers.dto;

namespace macmod.services.interfaces;

public interface IProgrammingProjectService
{
    Task<List<ProgrammingProjectDto>> FindAll();
    
    Task<GameMapDto> ProgrammingProjectDto(string title);
}