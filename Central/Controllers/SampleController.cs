using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Controllers
{
    public class SampleController : Controller
    {
        [HttpGet]
        public JsonResult ValidasiUser(string userID, string pass, int shift)
        {
            bool result = false;
            if (userID == "imam" && pass == "imam" && shift == 1)
            {
                result = true;
            }
            return Json(new
            {
                result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
