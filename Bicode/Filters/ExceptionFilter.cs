using Bicode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bicode.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public IWebHostEnvironment _webHostEnvironment;
    public IModelMetadataProvider _modelMetadataProvider;
    public ExceptionFilter(IWebHostEnvironment webHostEnvironment, IModelMetadataProvider modelMetadataProvider)
    {
        _webHostEnvironment = webHostEnvironment;
        _modelMetadataProvider = modelMetadataProvider;
    }
    public void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = 500;

        context.Result = new JsonResult(new ResponsePersonaDto
        {
            Message = $"ha habido una Exception del tipo {context.Exception.GetType()}",
            State = false
        }
        );
    }

}
