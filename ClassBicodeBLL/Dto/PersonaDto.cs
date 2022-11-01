using System.ComponentModel.DataAnnotations;

namespace ClassBicodeBLL.Dto;

public class PersonaDto
{
    private int? Id { get; set; }
    [Required]
    public int? IdDocumento { get; set; }
    [Required]
    public int? IdGenero { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    [Required]
    public long NumeroDocumento { get; set; }
    [Required]
    public DateTime? FechaNacimiento { get; set; }
}