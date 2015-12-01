using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MembersProject.Helpers;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Security.Providers;

namespace MembersProject.Extensions
{
    public class ExtendedMembershipProvider : MembersMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            var session = HttpContext.Current.Session;

            //check what login step it is
            var loginStep = session["loginStep"] != null ? session["loginStep"].ToString() : "1";

            if (loginStep == "1")
            {
                if (base.ValidateUser(username, password))
                {
                    LogHelper.Info<ExtendedMembershipProvider>("Member {0} logged in from {1}", () => username,
                        () => Helpers.NetworkHelper.GetVisitorIPAddress());

                    //member is validated so set logiStep to 2
                    session["loginStep"] = "2";

                    //generate PIN number and store it in Session object
                    var pin = SecurityHelper.GeneratePinNumber();
                    session["currentPin"] = pin;

                    // store member username for later use
                    session["currentMember"] = username;

                    //send PIN to member via Email or SMS, we will just output it to tracelog
                    LogHelper.Info<ExtendedMembershipProvider>("PIN number for member {0} is {1}", () => username,
                        () => pin);

                    //although Member is validated return false because PIN is not yet provided
                    return false;
                }
            }
            else
            {
                var currentPin = session["currentPin"] != null
                    ? session["currentPin"].ToString()
                    : string.Empty;

                //in this case password is PIN
                if (currentPin == password)
                {
                    // clear all values stored in session
                    session["currentPin"] = null;
                    session["loginStep"] = null;
                    session["currentMember"] = null;

                    return true;
                }
            }

            return false;
        }
    }
}