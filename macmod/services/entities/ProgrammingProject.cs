using System.ComponentModel.DataAnnotations;

namespace macmod.services.entities;

public class ProgrammingProject
{
    [Key]
    public long Id { get; set; }
    
    [MaxLength(100, ErrorMessage = "Purpose cannot exceed 100 characters.")]
    public string Purpose { get; set; } = "";
    
    public long ProjectId { get; set;}
    
    public Project Project { get; set;}
}