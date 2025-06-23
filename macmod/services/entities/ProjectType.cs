using System.ComponentModel.DataAnnotations;

namespace macmod.services.entities;

public class ProjectType
{
    public long Id { get; set;}

    [MaxLength(250)] 
    public string Type { get; set; } = "";
    
    [MaxLength(250)] 
    public string Title { get; set; } = "";
    
    [MaxLength(250)] 
    public string Thumbnail { get; set; } = "";
    
    [MaxLength(250)] 
    public string Icon { get; set; } = "";

    public List<Project> Projects = [];
}