namespace macmod.services.entities;

public class GameType
{
    public long Id { get; set; }

    public string Identifier { get; set; } = "";
    
    public string FullName { get; set; } = "";

    public List<GameMap> GameMaps { get; set; } = [];
}