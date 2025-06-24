using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace macmod.services.entities;

public class Project
{
    [Key]
    public long Id { get; set; }
    
    [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters.")]
    public string Title { get; set; } = "";
    
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 250 characters.")]
    public string Description { get; set; } = "";
    
    [MaxLength(150, ErrorMessage = "Version cannot exceed 150 characters.")]
    public string Version { get; set; } = "";
    
    [MaxLength(100, ErrorMessage = "Filesize cannot exceed 100 characters.")]
    public string Filesize { get; set; } = "";
    
    [MaxLength(250, ErrorMessage = "Project thumbnail path cannot exceed 250 characters.")]
    public string ProjectThumbnail { get; set; } = "";

    [Required] public DateTime PublishedDate { get; set; } = new();
    
    [JsonIgnore]
    public long ProjectTypeId { get; set;}
    public ProjectType? ProjectType { get; set; }
    
    public List<Link> Links = [];

    public List<Image> Images { get; set; } = [];
    
    public GameMap? GameMap { get; set; }
    public ProgrammingProject? ProgrammingProject { get; set; }
}