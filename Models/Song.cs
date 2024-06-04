using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class Song
{
    [Display(Name = "Id")]
    public int? SongId { get; set; }


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Nombre")]
    public required string Title { get; set; }


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Artista")]
    public string Artist { get; set; } = "Sin artista";


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Álbum")]
    public string Album { get; set; } = "Sin album";


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", 
    ErrorMessage ="El campo {0} debe cumplir con el formato 00:00:0.0")]
    [Display(Name = "Duración")]
    public TimeSpan Duration { get; set; }


    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Portada")]
    public int? FileId { get; set; }


    public ICollection<MusicGenre>? MusicGenres { get; set; }
}