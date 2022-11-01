namespace ClassBicodeBLL.Dto;

public class PersonaUpdateDto
{
    public int? Id { get; set; }
    public int? IdDocumento { get; set; }
    public int? IdGenero { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public long? NumeroDocumento { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}