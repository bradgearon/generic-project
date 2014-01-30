using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GenericProject.Core.Model;
using GenericProject.Core.Services;
using GenericProject.Web.ViewModels;
using Newtonsoft.Json;

namespace GenericProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index() { return View(); }
    }        
}