using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMGS.Presentation.Controllers
{
    [AllowAnonymous]
    public class WebUserController : Controller
    {
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}