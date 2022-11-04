using System.ComponentModel.DataAnnotations;

namespace ClassBicodeBLL.Dto;

public class PersonaUpdateDto
{
    public int? Id { get; set; }
    [Range(1, 3)]
    public int? IdDocumento { get; set; }
    [Range(1, 3)]
    public int? IdGenero { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public long? NumeroDocumento { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}