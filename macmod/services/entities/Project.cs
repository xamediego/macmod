using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace macmod.services.entities;

public class Project
{
    public long Id { get; set; }
    
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
    
    [JsonIgnore]
    public long ProjectTypeId { get; set;}
    public ProjectType? ProjectType { get; set; }
    
    public List<Link> Links = [];

    public List<Image> Images { get; set; } = [];
    
    public GameMap? GameMap { get; set; }
    public ProgrammingProject? ProgrammingProject { get; set; }
}