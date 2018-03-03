using System.Web.Mvc;
using MyProfileTrail.Models;

namespace MyProfileTrail.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public JsonResult FacebookLogin(FacebookLoginModel model)
        {
            Session["uid"] = model.uid;
            Session["accessToken"] = model.accessToken;

            return Json(new { success = true });
        }

    }
}