using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bicode.Models.Dto
{
    public class PersonaDto
    {
        private int? Id { get; set; }
        public int IdDocumento { get; set; }
        public int IdGenero { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public long NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}