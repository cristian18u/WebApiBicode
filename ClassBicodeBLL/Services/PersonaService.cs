using ClassBicodeBLL.Dto;
using ClassBicodeDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassBicodeBLL.Services
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
            return await querySql().ToListAsync();
        }

        public async Task<PersonaSelectDto?> GetAsyncId(int id)
        {
            if (_context.Personas == null) return null;
            return await querySql().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Boolean> CreateAsync(Persona newPersona)
        {
            if (await ValidationPersonExits(newPersona) == false)
            {
                _context.Personas.Add(newPersona);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<Boolean> UpdateAsync(PersonaUpdateDto personaUpdateDto, Persona personaDb)
        {
            if (personaUpdateDto.NumeroDocumento != null)
            {
                Persona newPersona = new Persona();
                newPersona.NumeroDocumento = personaUpdateDto.NumeroDocumento;
                if (personaUpdateDto.IdDocumento != null) newPersona.IdDocumento = personaUpdateDto.IdDocumento;
                else newPersona.IdDocumento = personaDb.IdDocumento;
                if (await ValidationPersonExits(newPersona) == false) return await SavePersonInDb(personaUpdateDto, personaDb);
                else return false;
            }
            else return await SavePersonInDb(personaUpdateDto, personaDb);
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

        public async Task<Boolean> ValidationPersonExits(Persona newPersona)
        {
            Persona? personaDb = await _context.Personas.Where(x => x.NumeroDocumento == newPersona.NumeroDocumento && x.IdDocumento == newPersona.IdDocumento).FirstOrDefaultAsync();
            if (personaDb == null) return false;
            else return true;
        }
        public async Task<String?> TipoDeDocumento(int? IdDocumento)
        {
            return await (from d in _context.Documentos where d.Id == IdDocumento select d.Abreviatura).FirstOrDefaultAsync();
        }
        public async Task<Boolean> SavePersonInDb(PersonaUpdateDto personaUpdateDto, Persona personaDb)
        {
            if (personaUpdateDto.IdDocumento != null) personaDb.IdDocumento = personaUpdateDto.IdDocumento;
            if (personaUpdateDto.IdGenero != null) personaDb.IdGenero = personaUpdateDto.IdGenero;
            if (personaUpdateDto.Nombre != null) personaDb.Nombre = personaUpdateDto.Nombre;
            if (personaUpdateDto.Apellido != null) personaDb.Apellido = personaUpdateDto.Apellido;
            if (personaUpdateDto.NumeroDocumento != null) personaDb.NumeroDocumento = personaUpdateDto.NumeroDocumento;
            if (personaUpdateDto.FechaNacimiento != null) personaDb.FechaNacimiento = personaUpdateDto.FechaNacimiento;
            personaDb.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
        public IQueryable<PersonaSelectDto> querySql()
        {
            return (from a in (from p in _context.Personas
                               join g in _context.Generos on p.IdGenero equals g.Id
                               join d in _context.Documentos on p.IdDocumento equals d.Id
                               let EdadYear = DateTime.Now.Year - ((DateTime)p.FechaNacimiento!).Year
                               select new PersonaSelectDto
                               {
                                   Id = p.Id,
                                   Nombre = p.Nombre,
                                   Apellido = p.Apellido,
                                   NumeroDocumento = (long)p.NumeroDocumento!,
                                   TipoDeDocumento = d.Abreviatura,
                                   Genero = g.Nombre,
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
                        Genero = a.Genero,
                        FechaNacimiento = a.FechaNacimiento,
                        FechaCreacion = a.FechaCreacion,
                        FechaActualizacion = a.FechaActualizacion,
                        Edad = a.Edad,
                        Clasificacion = (
                        a.Edad <= 14 ? "Ni??o" :
                        a.Edad >= 15 && a.Edad <= 20 ? "Adolecente" :
                        a.Edad >= 21 && a.Edad <= 60 ? "Mayor de Edad" :
                        "Tercera Edad"
                        )
                    });
        }
    }

}