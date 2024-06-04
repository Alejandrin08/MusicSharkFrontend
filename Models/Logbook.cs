using System.ComponentModel.DataAnnotations;

namespace frontendmusicshark.Models;

public class LogBook
{
    [Display(Name = "Id")]
    public int? LogBookId { get; set; }

    [Display(Name = "Acción")]
    public string? Action { get; set; }
    
    [Display(Name = "Elemento Id")]
    public string? IdElement { get; set; }

    [Display(Name = "IP")]
    public string? IP { get; set; }

    [Display(Name = "Usuario")]
    public string? User { get; set; }

    [Display(Name = "Fecha")]
    public DateTime? Date { get; set; }
}

