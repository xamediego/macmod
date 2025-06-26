using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.mappers;

public static class GameMapMapper
{
    public static GameMap MapGameMapFromDto(GameMapDto dto, GameType gameType, Project project)
    {
        return new GameMap
        {
            MinimumPlayer = dto.MinimumPlayer,
            MaximumPlayer = dto.MaximumPlayer,
            GameType = gameType,
            MapEnvironment = dto.Environment,
            ProjectId = project.Id
        };
    }

    public static GameMapDto MapDtoFromGameMap(GameMap gameMap)
    {
        GameMapDto project = (GameMapDto) ProjectMapper.MapDtoFromProject(gameMap.Project);
        
        KeyValuePair<string, string>[] details = [
            new("Players", $"{gameMap.MinimumPlayer} - {gameMap.MaximumPlayer}"),
            new("Environment", $"{gameMap.MapEnvironment.ToString()}"),
            new("GameType", $"{(gameMap.GameType != null ? gameMap.GameType.FullName : "")}")
        ];

        project.MinimumPlayer = gameMap.MinimumPlayer;
        project.MaximumPlayer = gameMap.MaximumPlayer;
        project.GameType = gameMap.GameType != null ? GameTypeMapper.MapDtoFromGameType(gameMap.GameType) : null;
        project.ExtraDetails = details;
        
        return project;
    }
}