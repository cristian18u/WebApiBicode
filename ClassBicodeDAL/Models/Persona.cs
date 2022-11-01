using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClassBicodeDAL.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdGenero { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public long? NumeroDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        [JsonIgnore]
        public virtual Documento? IdDocumentoNavigation { get; set; }
        [JsonIgnore]
        public virtual Genero? IdGeneroNavigation { get; set; }
    }
}
