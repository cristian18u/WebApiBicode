using Bicode.Models;
using Bicode.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bicode.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly PersonaService _personaService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, PersonaService personaService)
    {
        _logger = logger;
        _personaService = personaService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    // [HttpGet("test")]
    // public String Gettest()
    // {
    //     return _personaService.Hola();
    // }
    // [HttpGet("test2")]
    // public async Task<List<Persona>> Gettest2()
    // {
    //     return await _personaService.GetAsync();
    // }
}
