using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class Upload
{    
    [Display(Name = "Id")]
    public int? FileId { get; set; }    
    
    public string? Name { get; set; }

    [Display(Name = "Portada")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Upload)]
    public required IFormFile Image { get; set; }
}

