using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class GameTypeDto
{
    [MaxLength(50, ErrorMessage = "Identifier cannot exceed 50 characters.")]
    public string Identifier { get; set; } = "";

    [MaxLength(250, ErrorMessage = "Full name cannot exceed 250 characters.")]
    public string FullName { get; set; } = "";
}
