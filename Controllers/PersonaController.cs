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
        private readonly BI_TESTGENContext _context;

        private readonly PersonaService _personaService;

        public PersonaController(PersonaService personaService, BI_TESTGENContext context)
        {

            _personaService = personaService;
            _context = context;
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
            var personas = await _personaService.GetAsyncId(id);

            if (personas == null)
            {
                return NotFound();
            }
            return personas;
        }

        // // PUT: api/Persona/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPersona(int id, Persona persona)
        // {
        //     if (id != persona.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(persona).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PersonaExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Persona
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(PersonaDto personaDto)
        {
            Console.WriteLine("result1.ToString()");
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
            // _context.Personas.Add(persona);
            // await _context.SaveChangesAsync();
            await _personaService.CreateAsync(persona);
            return CreatedAtAction(nameof(GetPersonas), new
            {
                id = persona.Id
            }, persona);
        }

        // // DELETE: api/Persona/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeletePersona(int id)
        // {
        //     if (_context.Personas == null)
        //     {
        //         return NotFound();
        //     }
        //     var persona = await _context.Personas.FindAsync(id);
        //     if (persona == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Personas.Remove(persona);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool PersonaExists(int id)
        // {
        //     return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        // }
    }
}
