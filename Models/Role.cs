using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class Role
{
    public required string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Rol")]
    public required string Name { get; set; }
}