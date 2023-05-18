using System.ComponentModel.DataAnnotations;

namespace DGIIAPP.Domain.Models;

public class ComprobanteFiscal
{
    [Key]
    public string? NCF { get; set; }
    [Required]
    public string? RncCedula { get; set; }
    public decimal Monto { get; set; }
    public decimal Itbis18 { get; set; }
}
