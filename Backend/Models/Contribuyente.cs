using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Contribuyente
{
    [Key]
    public string? RncCedula { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Tipo { get; set; }
    [Required]
    public string? Estatus { get; set; }
}
