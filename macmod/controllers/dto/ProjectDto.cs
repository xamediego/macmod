using System.ComponentModel.DataAnnotations;
using macmod.services.Enums;

namespace macmod.controllers.dto;

public class ProjectDto
{
    public long Id { get; set; }

    [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters.")]
    public string Title { get; set; } = "";

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 250 characters.")]
    public string Description { get; set; } = "";

    [MaxLength(150, ErrorMessage = "Version cannot exceed 150 characters.")]
    public string Version { get; set; } = "";

    [MaxLength(100, ErrorMessage = "Filesize cannot exceed 100 characters.")]
    public string Filesize { get; set; } = "";
    
    [MaxLength(250, ErrorMessage = "Filesize cannot exceed 250 characters.")]
    public string FileName { get; set; } = "";

    [MaxLength(250, ErrorMessage = "Project thumbnail path cannot exceed 250 characters.")]
    public string ProjectThumbnail { get; set; } = "";

    public DateTime PublishedDate { get; set; }

    public List<LinkDto> Links { get; set; } = [];

    public List<string> Images { get; set; } = [];

    public KeyValuePair<string, string>[] ExtraDetails { get; set; } = [];


    public ProjectSubType ProjectSubType;
}