using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bicode.Models.Dto
{
    public class PersonaUpdateDto
    {
        public int Id { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdGenero { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public long? NumeroDocumento { get; set; }
    }
}