using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NerdStoreEnterprise.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        public ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>() 
            { 
                { "messages", Errors.ToArray() } 
            }));
        }

        protected ActionResult CustomResult(ModelStateDictionary modelState)
        {
            foreach (var error in modelState.Values.SelectMany(m => m.Errors))
                AddError(error.ErrorMessage);

            return CustomResponse();
        }

        protected bool ValidOperation()
            => !Errors.Any();

        protected void AddError(string error)
            => Errors.Add(error);

        protected void ClearErrors()
            => Errors.Clear();
    }
}