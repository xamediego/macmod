using macmod.controllers.dto;
using macmod.database;
using macmod.services.interfaces;

namespace macmod.services.implementation;

public class ProgrammingProjectService(DatabaseContext database) : IProgrammingProjectService
{
    private readonly DatabaseContext _database = database;

    public Task<List<ProgrammingProjectDto>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<GameMapDto> ProgrammingProjectDto(string title)
    {
        throw new NotImplementedException();
    }
}