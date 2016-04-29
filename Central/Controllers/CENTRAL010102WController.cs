using Central.Models.CENTRAL010102W;
using Central.Models.CENTRALMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Controllers
{
    public class CENTRAL010102WController : Controller
    {
        [HttpGet]
        public JsonResult ValidateUser(string userID, string pass, string mac)
        {
            CENTRAL010102W result = null;
            bool status = false;
            string message = "Null";
            string shift = "Null";
            string role = "Null";
            string tmcd = "Null";

            try
            {
                if (userID != "" && pass != "")
                {
                    result = CENTRAL010102WRepository.Instance.ValidateInDB(userID);
                    if (result != null)
                    {
                        if (result.PASSWORD == pass)
                        {
                            shift = result.SHIFT;
                            role = result.ROLE;
                            result = CENTRAL010102WRepository.Instance.ValidateMac(mac);
                            if (result != null)
                            {
                                CENTRAL010102WRepository.Instance.InsertLogin(userID);
                                status = true;
                                message = "User Registered";
                                //userid = result.USER_ID;                            
                                tmcd = result.TM_CD;
                            }

                        }
                        else
                        {
                            message = CENTRALMessageRepository.Instance.getMessageContent("MCENT112202E", null);//Please enter your correct user id
                        }

                    }
                    else
                    {
                        //status = false;
                        message = CENTRALMessageRepository.Instance.getMessageContent("MCENT112203E", null);//User Not Yet Register
                    }
                }
                else
                {
                    message = CENTRALMessageRepository.Instance.getMessageContent("MCENSTD002E", null);//{0} should not be empty
                }
                
                //

            }
            catch
            {
                
            }

            return Json(new
            {
                userName = userID,
                terminalCode = tmcd,
                shift = shift,
                roleID = role,
                content = message,
                status = status
                  
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult forgotPass(string msgCD)
        {
           String message = CENTRALMessageRepository.Instance.getMessageContent(msgCD, null);//Please enter your correct user id

            return Json(new
            {
                userName = "",
                terminalCode = "",
                shift = "",
                roleID = "",
                content = message,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
