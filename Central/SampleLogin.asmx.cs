using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Central
{
    /// <summary>
    /// Summary description for SampleLogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SampleLogin : System.Web.Services.WebService
    {

        [WebMethod]
        public bool AuthenticateUser(string userID, string pass, int shift)
        {
            bool result = false;
            if (userID == "imam" && pass == "imam" && shift == 1)
            {
                result = true;
            }
            return result;
        }
    }
}
