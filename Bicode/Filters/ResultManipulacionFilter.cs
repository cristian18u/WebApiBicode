using Bicode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bicode.Filters;

public class ResultManipulationFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            context.Result = new NotFoundObjectResult(new ResponsePersonaDto
            {
                Message = "El View Model is invalid, Faltan Propiedades Requeridas",
                State = false
            });
        }
    }
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
