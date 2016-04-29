using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;

namespace Central
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : WebApplication
    {
        public MvcApplication()
        {
            ApplicationSettings.Instance.Name = "Central";
            ApplicationSettings.Instance.Alias = "APP";
            ApplicationSettings.Instance.OwnerName = "Toyota Motor Manufacturing Indonesia";
            ApplicationSettings.Instance.OwnerAlias = "TMMIN";
            ApplicationSettings.Instance.OwnerEmail = "tdk@toyota.co.id";
            ApplicationSettings.Instance.Menu.Enabled = true;
            ApplicationSettings.Instance.Runtime.HomeController = "Home"; //uncomment this to setting default page

            ApplicationSettings.Instance.Security.EnableAuthentication = false;
            ApplicationSettings.Instance.Security.IgnoreAuthorization = true;

        }

        protected override void Startup()
        {
            ProviderRegistry.Instance.Register<IUserProvider>(typeof(UserProvider), DatabaseManager.Instance, "SecurityCenter");
        }
    }
}