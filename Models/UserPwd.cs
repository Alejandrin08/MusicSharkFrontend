using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class UserPwd
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo válido.")]
    [RegularExpression(@"^(?i)(([a-z0-9._%+-]+)@((uv\.mx|estudiantes\.uv\.mx|gmail\.com|hotmail\.com|outlook\.com|edu\.mx))|\s*)$",
    ErrorMessage = "El campo {0} tiene un formato válido")]
    [DataType(DataType.EmailAddress)]    
    [Display(Name = "Correo electrónico")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [MinLength(8, ErrorMessage = "El campo {0} debe tener un mínimo de {1} caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])([A-Za-z\d$@$!%*?&]|[^ ]){8,15}$",ErrorMessage = "La {0} debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un dígito y un carácter especial.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public required string Password { get; set; }


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$",
    ErrorMessage = "El campo {0} no acepta numeros o caracteres especiales")]
    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Rol")]
    public required string Role { get; set; } = "Usuario";
}