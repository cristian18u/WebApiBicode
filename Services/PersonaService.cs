using Bicode.Models;
using Bicode.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Bicode.Services
{
    public class PersonaService
    {
        private readonly BI_TESTGENContext _context;

        public PersonaService(BI_TESTGENContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>?> GetAsync()
        {
            if (_context.Personas == null) return null;
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona?> GetAsyncId(int id)
        {
            if (_context.Personas == null) return null;
            return await _context.Personas.FindAsync(id);
        }
        public async Task CreateAsync(Persona newPersona)
        {
            _context.Personas.Add(newPersona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PersonaDto personaDto, Persona personaDb)
        {
            if (personaDto.IdDocumento != null) personaDb.IdDocumento = personaDto.IdDocumento;
            if (personaDto.IdGenero != null) personaDb.IdGenero = personaDto.IdGenero;
            if (personaDto.Nombre != null) personaDb.Nombre = personaDto.Nombre;
            if (personaDto.Apellido != null) personaDb.Apellido = personaDto.Apellido;
            if (personaDto.NumeroDocumento != null) personaDb.NumeroDocumento = personaDto.NumeroDocumento;
            if (personaDto.FechaNacimiento != null) personaDb.FechaNacimiento = personaDto.FechaNacimiento;
            personaDb.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Persona persona)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

    }

}