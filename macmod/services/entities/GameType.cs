using System.ComponentModel.DataAnnotations;

namespace macmod.services.entities;

public class GameType
{
    [Key]
    public long Id { get; set; }

    [MaxLength(50, ErrorMessage = "Identifier cannot exceed 50 characters.")]
    public string Identifier { get; set; } = "";
    
    [MaxLength(250, ErrorMessage = "Full name cannot exceed 250 characters.")]
    public string FullName { get; set; } = "";

    public List<GameMap> GameMaps { get; set; } = [];
}