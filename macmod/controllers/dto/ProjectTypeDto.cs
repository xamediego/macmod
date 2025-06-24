using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class ProjectTypeDto
{
    [MaxLength(100, ErrorMessage = "Type cannot exceed 100 characters.")]
    public string Type { get; set; } = "";

    [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters.")]
    public string Title { get; set; } = "";

    [MaxLength(250, ErrorMessage = "Thumbnail path cannot exceed 250 characters.")]
    public string Thumbnail { get; set; } = "";

    [MaxLength(250, ErrorMessage = "Icon path cannot exceed 250 characters.")]
    public string Icon { get; set; } = "";

    public List<ProjectDto> Projects { get; set; } = [];
}
