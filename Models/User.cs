using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class User
{
    [Display(Name = "Id")]
    public string? Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo válido.")]
    
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$", 
    ErrorMessage ="El campo {0} no acepta numeros o caracteres especiales")]
    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Rol")]
    public required string Role { get; set; }
}