using macmod.controllers.dto;

namespace macmod.services.interfaces;

public interface IGameMapService
{
    Task<List<GameMapDto>> FindAll();
    
    Task<GameMapDto> FindByTitle(string title);
}