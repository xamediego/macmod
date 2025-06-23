using System.ComponentModel.DataAnnotations;

namespace macmod.services.entities;

public class ProgrammingProject
{
    public long Id { get; set; }
    
    [MaxLength(100)] 
    public string Purpose { get; set; } = "";
    
    public long ProjectId { get; set;}
    
    public Project Project { get; set;}
}