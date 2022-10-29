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

        public PersonaController(PersonaService personaService)
        {
            _personaService = personaService;
        }
        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
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
        public async Task<ActionResult<Persona>> GetPersonas(int id)
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
        public async Task<IActionResult> PutPersona(int id, PersonaDto personaDto)
        {

            if (id != personaDto.Id)
            {
                return BadRequest();
            }

            var personaDb = await _personaService.GetAsyncId(id);

            if (personaDb == null)
            {
                return NotFound();
            }

            try
            {
                await _personaService.UpdateAsync(personaDto, personaDb);
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
            var persona = await _personaService.GetAsyncId(id);

            if (persona == null)
            {
                return NotFound();
            }
            await _personaService.RemoveAsync(persona);

            return NoContent();
        }
    }
}
