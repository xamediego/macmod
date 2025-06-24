using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class LinkDto
{
    [MaxLength(100, ErrorMessage = "Provider name cannot exceed 100 characters.")]
    public string Provider { get; set; } = "";

    [MaxLength(250, ErrorMessage = "URL cannot exceed 250 characters.")]
    public string Url { get; set; } = "";
}
