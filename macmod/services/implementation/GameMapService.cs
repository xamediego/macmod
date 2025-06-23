using macmod.controllers.dto;
using macmod.database;
using macmod.services.interfaces;

namespace macmod.services.implementation;

public class GameMapService(DatabaseContext database) : IGameMapService
{
    private readonly DatabaseContext _database = database;

    public Task<List<GameMapDto>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<GameMapDto> FindByTitle(string title)
    {
        throw new NotImplementedException();
    }
}