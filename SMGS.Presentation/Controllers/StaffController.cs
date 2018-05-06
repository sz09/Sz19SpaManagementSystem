using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMGS.Presentation.Controllers.Staff
{
    public class StaffController : Controller
    {
        /// <summary>
        /// Get all staff
        /// </summary>
        /// <param name="staffInUse">
        /// Default: True - Get all staffes available
        /// False - Get All staffes
        /// </param>
        /// <returns>Show view</returns>
        public ActionResult GetAllStaffes(bool staffInUse = true)
        {
            return View();
        }
	}
}