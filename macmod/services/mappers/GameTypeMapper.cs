using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.mappers;

public static class GameTypeMapper
{
    public static GameType MapGameTypeFromDto(GameTypeDto dto)
    {
        return new GameType
        {
            Identifier = dto.Identifier,
            FullName = dto.FullName
        };
    }

    public static GameTypeDto MapDtoFromGameType(GameType gameType)
    {
        return new GameTypeDto
        {
            Identifier = gameType.Identifier,
            FullName = gameType.FullName
        };
    }
}