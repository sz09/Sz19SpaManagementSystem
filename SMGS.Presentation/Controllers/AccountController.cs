using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Business.IServices;
using System;
using System.Web.Mvc;
using log4net;
using Infrastructure.Logging;

namespace SMGS.Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Attributes
        private readonly IAccountServices _iAccountServices;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AccountController));
        #endregion

        #region Constructors
        public AccountController(IAccountServices iAccountServices)
        {
            logger.EnterMethod();
            this._iAccountServices = iAccountServices;
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        [AllowAnonymous]
        public ActionResult Index()
        {
            logger.EnterMethod();
            try
            {
                return PartialView("Login");
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return View("ErrorAdminPage");  
            }
            finally
            {
                logger.LeaveMethod();
            } 
        }

        /// <summary>
        ///  This function 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CheckSignIn(string username, string password)
        {
            logger.EnterMethod();
            try
            {
                var autoLogin = CheckExistingCookies();
                if (autoLogin.Item1 == true)
                {
                    string jsonString = "";
                    if (autoLogin.Item2 == "admin")
                        jsonString = @"{'check':true, 'role':'admin'}";
                    else if (autoLogin.Item2 == "customer")
                        jsonString = @"{'check':true, 'role':'customer'}";
                    else
                        jsonString = @"{'check':true, 'role':'otheruser'}";
                    return Json(JsonConvert.SerializeObject(JObject.Parse(jsonString)));
                }
                if (this._iAccountServices.CheckSignIn(username, password))
                {
                    var forType = ""; //this._iAccountServices.GetAccountByUsername(username).AccountMappingRoles.ForType;
                    string jsonString = "";
                    if (forType == "admin")
                        jsonString = @"{'check':true, 'role':'admin'}";
                    else if (forType == "customer")
                        jsonString = @"{'check':true, 'role':'customer'}";
                    else
                        jsonString = @"{'check':true, 'role':'otheruser'}";
                    logger.Info("Check sign in: [True] with username: [" + username + "] with type: [" + forType + "]");
                    return Json(JsonConvert.SerializeObject(JObject.Parse(jsonString)));
                }
                logger.Info("Check sign in: [Fail] with username: [" + username + "]");
                return Json("false");
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return View("ErrorAdminPage");  
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        private Tuple<bool, string> CheckExistingCookies()
        {
            bool check = false;
            string _role = "";
            // Get user and role in cookies
            string username = Request.Cookies["di_resu"] == null ? "" : Request.Cookies["di_resu"].Value;
            string role = Request.Cookies["di_elor"] == null ? "" : Request.Cookies["di_elor"].Value;
            string xs = Request.Cookies["xs"] == null ? "" : Request.Cookies["xs"].Value;
            if (!string.IsNullOrEmpty(xs) && !string.IsNullOrEmpty(username.Trim()) && !string.IsNullOrEmpty(_role))
            {
                var checkLogin = this._iAccountServices.AutoLoginGetRole(username, xs);
                if (checkLogin != null)
                {
                    check = checkLogin.Item1;
                    _role = Array.IndexOf(checkLogin.Item2, "admin") < 0 ? "" : "admin";
                }
            }
            return Tuple.Create(check, _role);
        }
        #endregion
    }
}