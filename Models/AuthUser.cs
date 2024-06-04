using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class AuthUser
{
    [RegularExpression(@"^(?i)(([a-z0-9._%+-]+)@((uv\.mx|estudiantes\.uv\.mx|gmail\.com|hotmail\.com|outlook\.com|edu\.mx))|\s*)$", 
    ErrorMessage ="El campo {0} tiene un formato inválido")]
    public required string Email { get; set; }

    [Display(Name = "Nombre")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$", 
    ErrorMessage ="El campo {0} no acepta numeros o caracteres especiales")]
    public required string Name { get; set; }

    [Display(Name = "Rol")]
    public required string Role { get; set; }
    public required string Jwt { get; set; }
}