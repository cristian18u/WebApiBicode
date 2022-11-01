using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBicodeDAL.Models;
using ClassBicodeBLL.Services;
using ClassBicodeBLL.Dto;

namespace Bicode.V1.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PersonaController : ControllerBase
{
    private readonly PersonaService _personaService;

    public PersonaController(PersonaService personaService, BI_TESTGENContext context)
    {
        _personaService = personaService;
    }
    // GET: api/Persona
    [HttpGet]
    public async Task<ActionResult<List<PersonaSelectDto>>> GetPersonas()
    {
        var personas = await _personaService.GetAsync();

        if (personas == null)
        {
            return NotFound(new
            {
                Result = "{}",
                message = "No hay elementos en la base de dados",
                State = false
            });
        }
        return Ok(new
        {
            Result = personas,
            message = "Successfully",
            State = true
        });
    }

    // GET: api/Persona/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaSelectDto>> GetPersonas(int id)
    {
        var persona = await _personaService.GetAsyncId(id);

        if (persona == null)
        {
            return NotFound(new
            {
                Result = "{}",
                message = $"El Id: {id} no se encuentra en la base de datos",
                State = false
            });
        }
        return Ok(new
        {
            Result = persona,
            message = "Successfully",
            State = true
        });
    }
    // PUT: api/Persona/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(int id, PersonaUpdateDto personaUpdateDto)
    {

        if (id != personaUpdateDto.Id)
        {
            return BadRequest(new
            {
                Result = "{}",
                message = $"El Id: {id} no coincide con el Id enviado por body",
                State = false
            });
        }

        var personaDb = await _personaService.GetPersonaAsyncId(id);

        if (personaDb == null)
        {
            return NotFound(new
            {
                Result = "{}",
                message = $"El Id: {id} no se encuentra en la base de datos",
                State = false
            });
        }

        try
        {
            await _personaService.UpdateAsync(personaUpdateDto, personaDb);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return CreatedAtAction(nameof(GetPersonas),
        new
        {
            id = personaDb.Id
        },
        new
        {
            Result = personaDb,
            message = "Successfully",
            State = true
        });
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

        return CreatedAtAction(nameof(GetPersonas),
        new
        {
            id = persona.Id
        },
        new
        {
            Result = persona,
            message = "Successfully",
            State = true
        });
    }

    // DELETE: api/Persona/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var persona = await _personaService.GetPersonaAsyncId(id);

        if (persona == null)
        {
            return NotFound(new
            {
                Result = "{}",
                message = $"El Id: {id} no se encuentra en la base de datos",
                State = false
            });
        }
        await _personaService.RemoveAsync(persona);

        return Ok(new
        {
            Result = "{}",
            message = $"La persona con Id: {id} eliminada exitosamente de la base de datos",
            State = false
        });
    }
}
