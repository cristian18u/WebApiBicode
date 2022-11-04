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
            List<string> message = new List<string>();
            foreach (var a in context.ModelState.ToList())
            {
                message.Add(a.Value!.Errors[0].ErrorMessage);
            }
            context.Result = new BadRequestObjectResult(new
            ResponseDataAnnotationsCustom
            {
                Message = message,
                State = false
            });
        }
    }
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
