/************************************************************************************************
 * Program History : 
 * 
 * Project Name     : VEHICLE ENTERPRISE ARCHITECTURE TRACEABILITY (CENTRAL)
 * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
 * Function Id      : CENTRAL010102W
 * Function Name    : Webservice for mobile user login
 * Function Group   : -
 * Program Id       : CENTRAL010102WController
 * Program Name     : User Login Screen for Mobile
 * Program Type     : Controller
 * Description      : Get parameter from Android device then validate with db result (Active Directory)
 * Environment      : .NET 4.0, ASP MVC 4.0
 * Author           : FID.Imam Ibnu
 * Version          : 01.00.00
 * Creation Date    : 23/04/2016   
 * 
 * Update history     Re-fix date       Person in charge      Description 
 * 1.1                2016/05/09        FID.Imam Ibnu         Update field message content & Add TM_DESC
 * Copyright(C) 2016 - . All Rights Reserved                                                                                              
 *************************************************************************************************/
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
            string shift = "Null"; //Result from active directory
            string role = "Null"; //Result from active directory
            string tmcd = "Null";
            string tmdesc = "Null";

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
                                tmdesc = result.TM_DESC;
                            }

                        }
                        else
                        {
                            message = CENTRALMessageRepository.Instance.getMessageContent("MCENT112202E", null).MSG_DESC;//Please enter your correct user id
                        }

                    }
                    else
                    {
                        //status = false;
                        message = CENTRALMessageRepository.Instance.getMessageContent("MCENT112203E", null).MSG_DESC;//User Not Yet Register
                    }
                }
                else
                {
                    message = CENTRALMessageRepository.Instance.getMessageContent("MCENSTD002E", null).MSG_DESC;//Username & Pass should not be empty
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
                terminalDesc = tmdesc,
                shift = shift,
                roleID = role,
                content = message,
                status = status
                  
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult forgotPass(string msgCD)
        {
           String message = CENTRALMessageRepository.Instance.getMessageContent(msgCD, null).MSG_DESC;//Please enter your correct user id

            return Json(new
            {
                userName = "",
                terminalCode = "",
                terminalDesc = "",
                shift = "",
                roleID = "",
                content = message,
                status = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
