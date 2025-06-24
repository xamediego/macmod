using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace macmod.services.entities;

public class Image
{
    [Key]
    public long Id { get; set; }
    public string ImageUrl { get; set; } = "";
    
    [JsonIgnore]
    public long ProjectId { get; set; }
    public Project? Project { get; set; }
}