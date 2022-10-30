using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bicode.Models;
using Bicode.Services;
using Bicode.Models.Dto;

namespace Bicode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService _personaService;
        private readonly BI_TESTGENContext _context;

        public PersonaController(PersonaService personaService, BI_TESTGENContext context)
        {
            _personaService = personaService;
            _context = context;
        }
        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<List<PersonaSelectDto>>> GetPersonas()
        {
            var personas = await _personaService.GetAsync();

            if (personas == null)
            {
                return NotFound();
            }
            return personas;
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaSelectDto>> GetPersonas(int id)
        {
            var persona = await _personaService.GetAsyncId(id);

            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }
        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, PersonaUpdateDto personaUpdateDto)
        {

            if (id != personaUpdateDto.Id)
            {
                return BadRequest();
            }

            var personaDb = await _personaService.GetPersonaAsyncId(id);

            if (personaDb == null)
            {
                return NotFound();
            }

            try
            {
                await _personaService.UpdateAsync(personaUpdateDto, personaDb);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetPersonas), new
            {
                id = personaDb.Id
            }, personaDb);
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(PersonaDto personaDto)
        {
            //falta manejo de excepciones
            var persona = new Persona
            {
                IdDocumento = personaDto.IdDocumento,
                IdGenero = personaDto.IdGenero,
                Nombre = personaDto.Nombre,
                Apellido = personaDto.Apellido,
                NumeroDocumento = personaDto.NumeroDocumento,
                FechaNacimiento = personaDto.FechaNacimiento,
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now
            };
            await _personaService.CreateAsync(persona);

            return CreatedAtAction(nameof(GetPersonas), new
            {
                id = persona.Id
            }, persona);
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _personaService.GetPersonaAsyncId(id);

            if (persona == null)
            {
                return NotFound();
            }
            await _personaService.RemoveAsync(persona);

            return NoContent();
        }

        [HttpGet("test")]
        public async Task<ActionResult<List<PersonaSelectDto>>> GetSql()
        {
            List<PersonaSelectDto> value = await (
                                                from p in _context.Personas
                                                join g in _context.Generos
                                                on p.IdGenero equals g.Id
                                                select new PersonaSelectDto
                                                {
                                                    Nombre = p.Nombre,
                                                    Apellido = p.Apellido,
                                                    NumeroDocumento = p.NumeroDocumento,
                                                    Genero = g.Nombre
                                                }).ToListAsync();
            return value;
        }
        [HttpGet("test2")]
        public async Task<ActionResult<List<PersonaSelectDto>>> GetSqlClasificacion()
        {
            List<PersonaSelectDto> value = await (
                                                from a in (from p in _context.Personas
                                                           join g in _context.Generos on p.IdGenero equals g.Id
                                                           join d in _context.Documentos on p.IdDocumento equals d.Id
                                                           select new PersonaSelectDto
                                                           {
                                                               Id = p.Id,
                                                               Nombre = p.Nombre,
                                                               Apellido = p.Apellido,
                                                               NumeroDocumento = p.NumeroDocumento,
                                                               TipoDeDocumento = d.Abreviatura,
                                                               Genero = g.Nombre,
                                                               FechaNacimiento = p.FechaNacimiento,
                                                               FechaCreacion = p.FechaCreacion,
                                                               FechaActualizacion = p.FechaActualizacion,
                                                               Edad = DateTime.Now.Year - p.FechaNacimiento.Year
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
                                                }).ToListAsync();
            return value;
        }
    }
}
