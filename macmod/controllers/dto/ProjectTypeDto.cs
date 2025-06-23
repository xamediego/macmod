using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class ProjectTypeDto
{
    [MaxLength(250)] public string Type { get; set; } = "";

    [MaxLength(250)] public string Title { get; set; } = "";

    [MaxLength(250)] public string Thumbnail { get; set; } = "";

    [MaxLength(250)] public string Icon { get; set; } = "";

    public List<ProjectDto> Projects { get; set; } = [];
}