using System.ComponentModel.DataAnnotations;
using macmod.services.Enums;

namespace macmod.controllers.dto;

public class GameMapDto : ProjectDto
{
    [Range(1, int.MaxValue, ErrorMessage = "MinimumPlayer must be at least 1.")]
    public int MinimumPlayer { get; set; }
    
    [Range(1, 300, ErrorMessage = "MaximumPlayer cannot exceed 300.")]
    public int MaximumPlayer { get; set; }
    
    public MapEnvironment Environment { get; set; }
    
    public GameTypeDto? GameType { get; set;}
}