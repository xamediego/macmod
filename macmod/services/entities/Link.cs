using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace macmod.services.entities;

public class Link
{
    public long Id { get; set;}
    
    [MaxLength(50)] 
    public string Provider { get; set; } = "";
    
    [MaxLength(100)] 
    public string Url { get; set; } = "";
    
    [JsonIgnore]
    public long ProjectId { get; set;}
    public Project? Project { get; set; }
}