using Bicode.Filters;
using Bicode.Middleware;
using ClassBicodeBLL.Services;
using ClassBicodeDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<PersonaService>();
builder.Services.AddDbContext<BI_TESTGENContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Connection")
    ));
builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

builder.Services.AddControllers(options =>
{
    //filter ViewModel invalid
    options.Filters.Add<ResultManipulationFilter>();

    //Filter para el manejo de Exception
    options.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

//Middelware Para las paginas no existentes
app.UseMiddleware<MiddlewarePageNoFound>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
