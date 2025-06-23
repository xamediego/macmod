using System.ComponentModel.DataAnnotations;

namespace macmod.controllers.dto;

public class ProjectDto
{
    [MaxLength(250)] 
    public string Title { get; set; } = "";
    
    [MaxLength(250)] 
    public string Description { get; set; } = "";
    
    [MaxLength(250)] 
    public string Version { get; set; } = "";
    
    [MaxLength(250)] 
    public string Filesize { get; set; } = "";
    
    [MaxLength(250)] 
    public string ProjectThumbnail { get; set; } = "";
    
    public DateTime PublishedDate { get; set; }

    public List<LinkDto> Links { get; set; }
    
    public List<string> Images { get; set; }
}