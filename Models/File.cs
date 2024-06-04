using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class File
{
    [Display(Name = "Id")]
    public int? FileId { get; set; }

    [Display(Name = "MIME")]
    public string? Mime { get; set; }
    
    public string? Name { get; set; }

    [Display(Name = "Tamaño")]
    public int? Size { get; set; }

    [Display(Name = "Repositorio")]
    public bool InDb { get; set; } = true;
}

