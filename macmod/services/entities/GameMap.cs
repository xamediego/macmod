using System.ComponentModel.DataAnnotations;
using macmod.services.Enums;

namespace macmod.services.entities;

public class GameMap
{
    [Key]
    public long Id { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "MinimumPlayer must be at least 1.")]
    public int MinimumPlayer { get; set; }
    
    [Range(1, 300, ErrorMessage = "MaximumPlayer cannot exceed 300.")]
    public int MaximumPlayer { get; set; }

    public MapEnvironment MapEnvironment { get; set; }

    public long GameTypeId { get; set; }

    public GameType? GameType { get; set; }

    public long ProjectId { get; set;}
    
    public Project? Project { get; set;}
}