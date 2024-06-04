using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class SongMusicGenre
{
    [Display(Name = "Categoría")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int? MusicGenreId { get; set; }

    public string? Name { get; set; }

    public Song? Song { get; set; }
}