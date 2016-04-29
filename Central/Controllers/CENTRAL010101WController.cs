using Central.Models.CENTRAL010101W;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Controllers
{
    public class CENTRAL010101WController : Controller
    {
        [HttpGet]
        public JsonResult BodyValidation(string bodyNo)
        {
            bool result = true;
            string message = "";

            try 
            {
                result = CENTRAL010101WRepository.Instance.IsExistInSFBase(bodyNo, out message);
                if (result == false)
                {
                    throw new Exception();
                }
            }
            catch
            {
                result = false;
            }

            return Json(new
            {
                status = result,
                content = message
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
