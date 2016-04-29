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
using Central.Models.CENTRAL020106W;
using Central.Models.Standards;
using Central.Controllers.Standards;
using NPOI.HSSF.UserModel;

using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;
namespace CENTRAL020106W.Controllers
{
    public class CENTRAL020106WController : StandardsController
    {

        protected override void Startup()
        {
            Settings.Title = "Central";
            string ID = Request.QueryString["ID"];
            string FLAG = Request.QueryString["FLAG"];
            
            ViewData["PlantID"] = ID;

            
            if (FLAG == "Edit")
            {

                ViewData["GetEditData"] = CENTRAL020106WRepository.Instance.GetTerminalEdit(ID);
                ViewData["GetPartDescRepo"] = CENTRAL020106WRepository.Instance.GetPartDescRepo(ID);
                ViewData["ComboTerminal"] = CENTRAL020106WRepository.Instance.GetComboTerminal(ID);
                ViewData["ComboPartCode"] = CENTRAL020106WRepository.Instance.GetComboPartCode(ID);    
            }
           
            ViewData["ComboPlant"] = CENTRAL020106WRepository.Instance.GetComboPlant();
           ViewData["ConstrainNm1"] = CENTRAL020106WRepository.Instance.GetComboConstrainNm1();
        
          
        
            
            GetCentralList(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 10);

        }
        public ActionResult GetCentralList(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to, int p_page, int p_length)
        {
            source = source == "" ? null : source;
            frameNo = frameNo == "" ? null : frameNo;
            katashik = katashik == "" ? null : katashik;
            suffix = suffix == "" ? null : suffix;
            parttype = parttype == "" ? null : parttype;

            partNo = partNo == "" ? null : partNo;
            prodFrom = prodFrom == "" ? null : prodFrom;
            status = status == "" ? null : status;
            format = format == "" ? null : format;
            prodTo = prodTo == "" ? null : prodTo;

            start = start == "" ? null : start;
            len = len == "" ? null : len;
            from = from == "" ? null : from;
            to = to == "" ? null : to;

            if (!String.IsNullOrEmpty(prodFrom))
            {
                DateTime date_fr = DateTime.ParseExact(prodFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                prodFrom = date_fr.ToString("yyyy-MM-dd");
                DateTime date_to = DateTime.ParseExact(prodTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                prodTo = date_to.ToString("yyyy-MM-dd");
            }
            int CountData = CENTRAL020106WRepository.Instance.CountData(source, frameNo, katashik, suffix, parttype, partNo, prodFrom, status, format, prodTo, start, len, from, to);
            int TotalData = (int)Math.Ceiling((double)CountData / (double)p_length);
            int Page = 1;
            if (TotalData < p_page)
                Page = TotalData;
            else
                Page = p_page;
            ViewData["SimpleGrid"] = CENTRAL020106WRepository.Instance.GetSimpleGrid(source, frameNo, katashik, suffix, parttype, partNo, prodFrom, status, format, prodTo, start, len, from, to, ((Page - 1) * p_length) + 1, Page * p_length);
            ViewData["CountData"] = CountIndex(Page, CountData, p_length, TotalData, this.Settings.ControllerName);

            return PartialView("CENTRAL020106SimpleGrid");
        }

        public ActionResult SaveEditGrid(string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val1, string format_start_val2, string format_start_val3, string LenCons1, string LenCons2, string LenCons3,string starter, string Len, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val)
        {
            string result = CENTRAL020106WRepository.Instance.SaveEditGridRepo(ID, PlanCD_val, termCD_val, partCD_val, PartDesc_val, ColV1_val, ConsVal1_val, ColVI2_val, ConsVal2_val, ColVI3_val, ConsVal3_val, format_No_val, format_val_val, format_start_val1, format_start_val2, format_start_val3, LenCons1,LenCons2,LenCons3, starter, Len,valid_fr_val, valid_to_val, changeBy_val, changeDt_val, UpdateBy_val, UpdateDt_val);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveAddGrid(string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val1, string format_start_val2, string format_start_val3, string LenCons1, string LenCons2, string LenCons3, string starter, string Len, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val)
        {
            string result = CENTRAL020106WRepository.Instance.SaveAddGridRepo(ID, PlanCD_val, termCD_val, partCD_val, PartDesc_val, ColV1_val, ConsVal1_val, ColVI2_val, ConsVal2_val, ColVI3_val, ConsVal3_val, format_No_val, format_val_val, format_start_val1, format_start_val2, format_start_val3, LenCons1, LenCons2, LenCons3, starter, Len, valid_fr_val, valid_to_val, changeBy_val, changeDt_val, UpdateBy_val, UpdateDt_val);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComboTerm(string PlanCD_val)
        {
            var result = CENTRAL020106WRepository.Instance.GetComboTerminal(PlanCD_val);
            var TempData = from data in result
                           select new
                           {
                               value = data.TM_CD,
                               text = data.TM_CD
                           };
            var dataList = TempData.ToArray();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartDesc(string PlanCD_val)
        {
            var result = CENTRAL020106WRepository.Instance.GetPartDescRepo(PlanCD_val);
            return Json(result.ToString(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComboPartCode(string PlanCD_val)
        {
            var result = CENTRAL020106WRepository.Instance.GetComboPartCode(PlanCD_val);
            var TempData = from data in result
                           select new
                           {
                               value = data.PART_CD,
                               text = data.PART_CD
                           };
            var dataList = TempData.ToArray();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
    }

}
