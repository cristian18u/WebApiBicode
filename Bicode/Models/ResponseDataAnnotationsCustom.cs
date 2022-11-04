namespace Bicode.Models;

public class ResponseDataAnnotationsCustom
{
    public string? Result { get; set; }
    public List<string> Message { get; set; } = null!;
    public Boolean State { get; set; }
}
