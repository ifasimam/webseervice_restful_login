/************************************************************************************************
    * Program History :
    *
    * Project Name     : CORPORATE INFORMATION SYSTEM
    * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
    * Function Id      : CIS040104
    * Function Name    : Data Gathering Proposal (With Notification)
    * Function Group   : 03 Data Gathering & Lobbying Activities
    * Program Id       : CIS040104
    * Program Name     : CIS040104Controller
    * Program Type     : View
    * Description      :
    * Environment      : .NET 4.0, ASP MVC 4.0
    * Author           : FID.Hartono
    * Version          : 01.00.00
    * Creation Date    : 02/03/2016 09:35:40
    *
    * Update history       Re-fix date         Person in charge      Description
    *                     
    * Copyright(C) 2016 - . All Rights Reserved
    *************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using System.IO;
using Central.Models.CENTRAL010301W;
using Central.Models.Standards;
using Central.Controllers.Standards;
using NPOI.HSSF.UserModel;

using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;
using Toyota.Common.Database;
using System.Reflection;
namespace CENTRAL010301W.Controllers
{
    public class CENTRAL010301WController : StandardsController
    {

        protected override void Startup()
        {
            Settings.Title = "Central";

            //GetPartIDList(null, 1, 10);

        }

        public ActionResult GetPartIDList(string BDNO, int p_page, int p_length)
        {
            BDNO = BDNO == "" ? null : BDNO;
     
            int CountData = CENTRAL010301WRepository.Instance.CountData(BDNO);
            int TotalData = (int)Math.Ceiling((double)CountData / (double)p_length);
            int Page = 1;
            if (TotalData < p_page)
                Page = TotalData;
            else
                Page = p_page;
            ViewData["SimpleGrid"] = CENTRAL010301WRepository.Instance.GetSimpleGrid(BDNO, ((Page - 1) * p_length) + 1, Page * p_length);
            ViewData["CountData"] = CountIndex(Page, CountData, p_length, TotalData, this.Settings.ControllerName);

            return PartialView("CENTRAL010301WSimpleGrid");
    
        }
        public ActionResult GetConfirmData(string BdIDS)
        {
            string result = CENTRAL010301WRepository.Instance.SaveConfirmation(BdIDS);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CheckBody(string BDNO, string FR_CD, string MSG_NO)
        {
            BDNO = BDNO == "" ? null : BDNO;
            FR_CD = FR_CD == "" ? null : FR_CD;
            MSG_NO = MSG_NO == "" ? null : MSG_NO;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result2 = db.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WCheckBody");
            List<SIMPLEGRID> newListCheck = new List<SIMPLEGRID>();
            newListCheck = result2.ToList();
            int result = 0;
            if (FR_CD != null && BDNO != null)
            {
                newListCheck = newListCheck.Where(p => p.BDNO == BDNO).ToList();
            }
            result = newListCheck.Count();
            db.Close();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CheckBodyNo(string BDNO, string FR_CD, string MSG_NO)
        {
            IDBContext db2 = DatabaseManager.Instance.GetContext();
            var result2 = db2.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WCheckBodyNo", new { BDNO = BDNO });
            List<SIMPLEGRID> newListCheck2 = new List<SIMPLEGRID>();
            newListCheck2 = result2.ToList();
            string result = "";
            if (newListCheck2.Where(p => p.STS == "C").Count() > 0)
            {
                result = "C";
            }
            if (newListCheck2.Where(p => p.STS == "R").Count() > 0)
            {
                result = "RC";
            }
            if (newListCheck2.Count()<=0)
            {
                result = "R";
            }

            if (newListCheck2.Where(p => p.STS ==null ||p.STS=="").Count() <= 0)
            {
                result = "RC";
            }

            db2.Close();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckinSFNCentral(string BDNO, string FR_CD, string MSG_NO)
        {
            IDBContext db3 = DatabaseManager.Instance.GetContext();
            var result3 = db3.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WCheckinSFNCentral", new { BDNO = BDNO, FR_CD = FR_CD, MSG_NO = MSG_NO });
            List<SIMPLEGRID> newListCheck3 = new List<SIMPLEGRID>();
            newListCheck3 = result3.ToList();
            if (newListCheck3.Count() > 0)
            {
                IDBContext db4 = DatabaseManager.Instance.GetContext();
                db4.Execute("CENTRAL010301W/CENTRAL010301WUpdateSFNCntral", new { BDNO = BDNO, MSG_NO = MSG_NO, FR_CD = FR_CD });
                db4.Close();
                //UPDATE TB_R_LOG_H AND TB_R_LOG_D

            }
            if (newListCheck3.Count() <= 0)
            {
                IDBContext db4 = DatabaseManager.Instance.GetContext();
                db4.Execute("CENTRAL010301W/CENTRAL010301WUpdateSFNCntralNG", new { BDNO = BDNO, MSG_NO = MSG_NO, FR_CD = FR_CD });
                db4.Close();
                //UPDATE TB_R_LOG_H AND TB_R_LOG_D
            }
            db3.Close();
            return Json(result3, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTblFormatting(string BDNO, string FR_CD, string MSG_NO)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result7 = db.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WGetTblFtmttng", new { BDNO = BDNO });
            db.Close();
            return Json(result7, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLoopSfnLog(string result5, string BDNO, string MSG_NO)
        {
            string flag = "";
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result7 = db.Fetch<GETDATA>("CENTRAL010301W/CENTRAL010301WGetLoopSFNLog",new{VAL=result5,BDNO=BDNO});
            db.Close();
            if (result7.Count() > 0)
            {
                db.Execute("CENTRAL010301W/CENTRAL010301WUpdateSFNCntralOK", new { BDNO = BDNO, MSG_NO = MSG_NO });
                db.Close();
                //Insert to log monitoring (TB_R_LOG_H and TB_R_LOG_D)
                flag = "valid";
            }



            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartIDSFNPart(string BDNO)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result6 = db.Fetch<GETDATA>("CENTRAL010301W/CENTRAL010301WSelectLeftJoinTbl", new { BDNO = BDNO });
            db.Close();
            List<GETDATA> newList = new List<GETDATA>();
            newList = result6.ToList();
            string flag = "";
            if (newList.Select(p => p.STS).FirstOrDefault() == "C" && newList.Select(p => p.JUDGE).FirstOrDefault() == "0" || newList.Select(p => p.JUDGE).FirstOrDefault() == "1")
            {
                flag = "1";
                //Insert to log monitoring (TB_R_LOG_H and TB_R_LOG_D)

            }
            if (newList.Select(p => p.STS).FirstOrDefault() == "R" || newList.Select(p => p.STS).FirstOrDefault() == null && newList.Select(p => p.JUDGE).FirstOrDefault() == "0" || newList.Select(p => p.JUDGE).FirstOrDefault() == "1")
            {
                flag = "2";

            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBDNoSFBase(string COL_VI1, string COL_VI2, string COL_VI3,string CONS_VAL1, string CONS_VAL2,string CONS_VAL3, string BDNO,string FR_CD)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<GETDATA>("CENTRAL010301W/CENTRAL010301WGetSFNBaseData", new { BDNO = BDNO, FR_CD = FR_CD });
            int index = 0;
            foreach (GETDATA Item in result)
            {
                Type type = Item.GetType();
                PropertyInfo propInfo = type.GetProperty(COL_VI1);
                string value = (string)propInfo.GetValue(Item, null);
                if (value.Trim().Contains(CONS_VAL1.Trim()))
                {
                    index++;
                }

                propInfo = type.GetProperty(COL_VI2);
                 value = (string)propInfo.GetValue(Item, null);
                 if (value.Trim().Contains(CONS_VAL2.Trim()))
                 {
                     index++;
                 }

                 propInfo = type.GetProperty(COL_VI3);
                 value = (string)propInfo.GetValue(Item, null);
                 if (value.Trim().Contains(CONS_VAL3.Trim()))
                 {
                     index++;
                 }
            }

            return Json(index, JsonRequestBehavior.AllowGet);
             
        }
        public ActionResult GetPartID(string BDNO, string MSG_NO)//besok
        {
            string flag = "";
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WLooingFormatVal", new { BDNO=@BDNO});
            List<SIMPLEGRID> listSTR = new List<SIMPLEGRID>();
            listSTR = result.ToList();

            List<string> newstrVal = new List<string>();
            newstrVal = listSTR.Select(p => p.FORMAT_VAL).ToList();

            List<string> newstrVal2 = new List<string>();
            newstrVal2 = listSTR.Select(p => p.VAL).ToList();


            
                for (int i = 0; i < newstrVal.Count(); i++)
                {
                    if (newstrVal[i].Length == newstrVal2[i].Length)
                    {
                        List<string> format = getFormatList(newstrVal[i]);
                        if (validationFormat(newstrVal2[i], format))
                        {
                            db.Execute("CENTRAL010301W/CENTRAL010301WUpdateSFNCntralOK", new { BDNO = BDNO, MSG_NO = MSG_NO });
                            db.Close();
                            flag =  newstrVal2[i].ToString();

                            //Insert to log monitoring (TB_R_LOG_H and TB_R_LOG_D)

                        }
                        if (!validationFormat(newstrVal2[i], format))
                        {
                            db.Execute("CENTRAL010301W/CENTRAL010301WUpdateNG", new { BDNO = BDNO, MSG_NO = MSG_NO });
                            db.Close();
                          
                            //Insert to log monitoring (TB_R_LOG_H and TB_R_LOG_D)
                        }
                        else
                        {
                            flag = newstrVal2[i].ToString();
                        }

                    }
                    else
                    {
                        db.Execute("CENTRAL010301W/CENTRAL010301WUpdateNG", new { BDNO = BDNO, MSG_NO = MSG_NO });
                        db.Close();
                     
                        //Insert to log monitoring (TB_R_LOG_H and TB_R_LOG_D)
                    }
                }
            
            db.Close();
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        private bool validationFormat(string values, List<string> formats)
        {
            bool result = true;
            int index = 0;
            foreach(string format in formats )
            {
                if(format.Substring(1,1)=="N")
                {
                    string value = values.Substring(index,format.Length);
                    int temp = 0;
                    if(int.TryParse(value,out temp)==false)
                    {
                        result = false;
                        break;
                    }

                }
                index += format.Length;
            }
            return result;
        }

        private List<string> getFormatList(string p)
        {
            List<string> result = new List<string>();
            string temp = null;
            int index = 0;
            for (int i = 0; i < p.Length; i++)
            {
                string value = p.Substring(i,1);
                if (result.Count == 0)
                {
                    result.Add(value);
                    
                }
                else if (result[index].Contains(value))
                {
                    result[index] = result[index] + value;
                }
                else if (!result[index].Contains(value))
                {
                    result.Add(value);
                    ++index;
                }
                
            }
            return result;
        }
    }

}
