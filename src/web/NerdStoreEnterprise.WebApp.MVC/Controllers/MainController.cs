using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.MVC.Models;
using System.Linq;

namespace NerdStoreEnterprise.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var mensagem in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}