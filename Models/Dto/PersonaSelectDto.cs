using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bicode.Models.Dto
{
    public class PersonaSelectDto
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public long? NumeroDocumento { get; set; }
        public string? TipoDeDocumento { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Clasificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}