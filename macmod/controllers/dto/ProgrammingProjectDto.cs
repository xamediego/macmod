using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class ProgrammingProjectDto : ProjectDto
{
    [MaxLength(100, ErrorMessage = "Purpose cannot exceed 100 characters.")]
    public string Purpose { get; set; } = "";
}
