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

        public async Task<List<PersonaSelectDto>?> GetAsync()
        {
            if (_context.Personas == null) return null;
            return await (from a in (from p in _context.Personas
                                     join g in _context.Generos on p.IdGenero equals g.Id
                                     join d in _context.Documentos on p.IdDocumento equals d.Id
                                     let EdadYear = DateTime.Now.Year - ((DateTime)p.FechaNacimiento!).Year
                                     select new PersonaSelectDto
                                     {
                                         Id = p.Id,
                                         Nombre = p.Nombre!,
                                         Apellido = p.Apellido!,
                                         NumeroDocumento = (long)p.NumeroDocumento!,
                                         TipoDeDocumento = d.Abreviatura!,
                                         Genero = g.Nombre!,
                                         FechaNacimiento = (DateTime)p.FechaNacimiento!,
                                         FechaCreacion = (DateTime)p.FechaCreacion!,
                                         FechaActualizacion = (DateTime)p.FechaActualizacion!,
                                         Edad = DateTime.Now.DayOfYear < ((DateTime)p.FechaNacimiento).DayOfYear ? EdadYear - 1 : EdadYear
                                     })
                          select new PersonaSelectDto
                          {
                              Id = a.Id,
                              Nombre = a.Nombre,
                              Apellido = a.Apellido,
                              NumeroDocumento = a.NumeroDocumento,
                              TipoDeDocumento = a.TipoDeDocumento,
                              Genero = a.Nombre,
                              FechaNacimiento = a.FechaNacimiento,
                              FechaCreacion = a.FechaCreacion,
                              FechaActualizacion = a.FechaActualizacion,
                              Edad = a.Edad,
                              Clasificacion = (
                              a.Edad <= 14 ? "Niño" :
                              a.Edad >= 15 && a.Edad <= 20 ? "Adolecente" :
                              a.Edad >= 21 && a.Edad <= 60 ? "Mayor de Edad" :
                              "Tercera Edad"
                              )
                          }).ToListAsync(); ;
        }

        public async Task<PersonaSelectDto?> GetAsyncId(int id)
        {
            if (_context.Personas == null) return null;
            return await (
                                                from a in (from p in _context.Personas
                                                           join g in _context.Generos on p.IdGenero equals g.Id
                                                           join d in _context.Documentos on p.IdDocumento equals d.Id
                                                           let EdadYear = DateTime.Now.Year - ((DateTime)p.FechaNacimiento!).Year
                                                           select new PersonaSelectDto
                                                           {
                                                               Id = p.Id,
                                                               Nombre = p.Nombre!,
                                                               Apellido = p.Apellido!,
                                                               NumeroDocumento = (long)p.NumeroDocumento!,
                                                               TipoDeDocumento = d.Abreviatura!,
                                                               Genero = g.Nombre!,
                                                               FechaNacimiento = (DateTime)p.FechaNacimiento!,
                                                               FechaCreacion = (DateTime)p.FechaCreacion!,
                                                               FechaActualizacion = (DateTime)p.FechaActualizacion!,
                                                               Edad = DateTime.Now.DayOfYear < ((DateTime)p.FechaNacimiento).DayOfYear ? EdadYear - 1 : EdadYear
                                                           })
                                                where a.Id == id
                                                select new PersonaSelectDto
                                                {
                                                    Id = a.Id,
                                                    Nombre = a.Nombre,
                                                    Apellido = a.Apellido,
                                                    NumeroDocumento = a.NumeroDocumento,
                                                    TipoDeDocumento = a.TipoDeDocumento,
                                                    Genero = a.Nombre,
                                                    FechaNacimiento = a.FechaNacimiento,
                                                    FechaCreacion = a.FechaCreacion,
                                                    FechaActualizacion = a.FechaActualizacion,
                                                    Edad = a.Edad,
                                                    Clasificacion = (
                                                    a.Edad <= 14 ? "Niño" :
                                                    a.Edad >= 15 && a.Edad <= 20 ? "Adolecente" :
                                                    a.Edad >= 21 && a.Edad <= 60 ? "Mayor de Edad" :
                                                    "Tercera Edad"
                                                    )
                                                }).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Persona newPersona)
        {
            _context.Personas.Add(newPersona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PersonaUpdateDto personaUpdateDto, Persona personaDb)
        {
            if (personaUpdateDto.IdDocumento != null) personaDb.IdDocumento = personaUpdateDto.IdDocumento;
            if (personaUpdateDto.IdGenero != null) personaDb.IdGenero = personaUpdateDto.IdGenero;
            if (personaUpdateDto.Nombre != null) personaDb.Nombre = personaUpdateDto.Nombre;
            if (personaUpdateDto.Apellido != null) personaDb.Apellido = personaUpdateDto.Apellido;
            if (personaUpdateDto.NumeroDocumento != null) personaDb.NumeroDocumento = personaUpdateDto.NumeroDocumento;
            if (personaUpdateDto.FechaNacimiento != null) personaDb.FechaNacimiento = personaUpdateDto.FechaNacimiento;
            personaDb.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Persona persona)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<Persona?> GetPersonaAsyncId(int id)
        {
            return await _context.Personas.FindAsync(id);
        }
    }

}