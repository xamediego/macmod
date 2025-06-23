using macmod.services.Enums;

namespace macmod.services.entities;

public class GameMap
{
    public long Id { get; set; }
    
    public int MinimumPlayer { get; set; }

    public int MaximumPlayer { get; set; }

    public MapEnvironment MapEnvironment { get; set; }

    public long GameTypeId { get; set; }

    public GameType? GameType { get; set; }

    public long ProjectId { get; set;}
    
    public Project? Project { get; set;}
}