using ClassBicodeBLL.Dto;

namespace Bicode.Models;

public class ResponsePersonaDto
{
    public PersonaDto? Result { get; set; }
    public string Message { get; set; } = null!;
    public Boolean State { get; set; }
}
