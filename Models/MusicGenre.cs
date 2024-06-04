using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class MusicGenre
{
    [Display(Name = "Id")]
    public int? MusicGenreId { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$", 
    ErrorMessage ="El campo {0} no acepta numeros o caracteres especiales")]
    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Display(Name = "Eliminable")]
    public bool Protected { get; set; } = false;
}

