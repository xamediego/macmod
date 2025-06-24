using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace macmod.services.entities;

public class Link
{
    [Key]
    public long Id { get; set;}
    
    [MaxLength(100, ErrorMessage = "Provider name cannot exceed 100 characters.")]
    public string Provider { get; set; } = "";
    
    [MaxLength(250, ErrorMessage = "URL cannot exceed 250 characters.")]
    public string Url { get; set; } = "";
    
    [JsonIgnore]
    public long ProjectId { get; set;}
    public Project? Project { get; set; }
}