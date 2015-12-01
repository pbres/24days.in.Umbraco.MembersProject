using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using Umbraco.Web;

namespace MembersProject.Controllers
{
    public class MembersController : SurfaceController
    {
        [HttpPost]
        public ActionResult HandleLogin(string username, string password)
        {
            var loginStep = Session["loginStep"];

            // if it's second step get username from session as we are getting only PIN
            if (loginStep != null && loginStep.ToString() == "2")
            {
                username = Session["currentMember"] != null ? Session["currentMember"].ToString() : string.Empty;
            }
            if (!Members.Login(username, password))
            {
                // if we are still on the same step and Member validation failed that means we need to dispaly login error
                if (loginStep == Session["loginStep"])
                {
                    TempData["status"] = "Login failed";
                }
            }
            return RedirectToCurrentUmbracoUrl();
        }
    }
}