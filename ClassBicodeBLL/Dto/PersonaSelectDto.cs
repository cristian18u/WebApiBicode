using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBicodeBLL.Dto
{
    public class PersonaSelectDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public long NumeroDocumento { get; set; }
        public string TipoDeDocumento { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string Clasificacion { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}