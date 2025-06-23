using macmod.services.Enums;

namespace macmod.controllers.dto;

public class GameMapDto : ProjectDto
{
    public int MinimumPlayer { get; set; }
    
    public int MaximumPlayer { get; set; }
    
    public MapEnvironment Environment { get; set; }
    
    public GameTypeDto? GameType { get; set;}
}