using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBicodeDAL.Models;
using ClassBicodeBLL.Services;
using ClassBicodeBLL.Dto;
using Bicode.Models;

namespace Bicode.Controllers;

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
        List<PersonaSelectDto>? personas = await _personaService.GetAsync();

        if (personas == null)
        {
            return NotFound(new ResponsePersonaDto
            {
                Message = "No hay elementos en la base de dados",
                State = false
            });
        }
        return Ok(new
        {
            Result = personas,
            Message = "Successfully nueva publicacion",
            State = true
        });
    }

    // GET: api/Persona/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaSelectDto>> GetPersonas(int id)
    {
        PersonaSelectDto? persona = await _personaService.GetAsyncId(id);

        if (persona == null)
        {
            return NotFound(new ResponsePersonaDto
            {
                Message = $"El Id: {id} no se encuentra en la base de datos",
                State = false
            });
        }
        return Ok(new
        {
            Result = persona,
            Message = "Successfully, nueva publicacion",
            State = true
        });
    }
    // PUT: api/Persona/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(int id, PersonaUpdateDto personaUpdateDto)
    {

        if (id != personaUpdateDto.Id)
        {
            return BadRequest(new ResponsePersonaDto
            {
                Message = $"El Id: {id} no coincide con el Id enviado por body",
                State = false
            });
        }

        var personaDb = await _personaService.GetPersonaAsyncId(id);

        if (personaDb == null)
        {
            return NotFound(new ResponsePersonaDto
            {
                Message = $"El Id: {id} no se encuentra en la base de datos",
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
            Message = "Successfully",
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
            Message = "Successfully",
            State = true
        });
    }

    // DELETE: api/Persona/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        Persona? persona = await _personaService.GetPersonaAsyncId(id);

        if (persona == null)
        {
            return NotFound(new ResponsePersonaDto
            {
                Message = $"El Id: {id} no se encuentra en la base de datos",
                State = false
            });
        }
        await _personaService.RemoveAsync(persona);

        return Ok(new ResponsePersonaDto
        {
            Message = $"La persona con Id: {id} eliminada exitosamente de la base de datos",
            State = false
        });
    }
    [HttpGet("test")]
    public IActionResult TestException()
    {
        throw new Exception("Esta es una Exception");
    }
}
