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
using Central.Models.CENTRAL030102W;
using Central.Models.Standards;
using Central.Controllers.Standards;
using NPOI.HSSF.UserModel;
 
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;
namespace CENTRAL030102W.Controllers
{
    public class CENTRAL030102WController : StandardsController
    {
      
        protected override void Startup()
        {
            Settings.Title = "Central";
            string ID = Request.QueryString["ID"];
            ViewData["ComboPartType"] = CENTRAL030102WRepository.Instance.GetComboPartType();
            ViewData["ComboFormat"] = CENTRAL030102WRepository.Instance.GetComboFormat();
            ViewData["ComboStatus"] = CENTRAL030102WRepository.Instance.GetComboStatus();
            ViewData["ComboSuffix"] = CENTRAL030102WRepository.Instance.GetComboSuffix();
         
            GetCentralList1(null,null,null,null,null,null,null,null,null,null,null,null,null,null,1,10);
          
            GetCentralList2(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 10);
      
        }
        public ActionResult GetCentralList1(string sourceCmb, string frameNo, string katashik, string suffix, string partType,
            string partNo, string ProdFrom, string status, string Formatt, string ProdTo, string startt, string Lenn, string Fromm, string Too, int p_page, int p_length)
        {
            sourceCmb = sourceCmb == "" ? null : sourceCmb;
            frameNo = frameNo == "" ? null : frameNo;
            katashik = katashik == "" ? null : katashik;
            suffix = suffix == "" ? null : suffix;
            partType = partType == "" ? null : partType;

            partNo = partNo == "" ? null : partNo;
            ProdFrom = ProdFrom == "" ? null : ProdFrom;
            status = status == "" ? null : status;
            Formatt = Formatt == "" ? null : Formatt;
            ProdTo = ProdTo == "" ? null : ProdTo;

            startt = startt == "" ? null : startt;
            Lenn = Lenn == "" ? null : Lenn;
            Fromm = Fromm == "" ? null : Fromm;
            Too = Too == "" ? null : Too;

            if (!String.IsNullOrEmpty(ProdFrom) && !String.IsNullOrEmpty(ProdTo))
            {
                DateTime date_fr = DateTime.ParseExact(ProdFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ProdFrom = date_fr.ToString("yyyy-MM-dd");
                DateTime date_to = DateTime.ParseExact(ProdTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ProdTo = date_to.ToString("yyyy-MM-dd");
            }
            int CountData = CENTRAL030102WRepository.Instance.CountData(sourceCmb, frameNo, katashik, suffix, partType, partNo, ProdFrom, status, Formatt, ProdTo, startt, Lenn, Fromm, Too);
            int TotalData = (int)Math.Ceiling((double)CountData / (double)p_length);
            int Page = 1;
            if (TotalData < p_page)
                Page = TotalData;
            else
                Page = p_page;
            ViewData["SimpleGrid"] = CENTRAL030102WRepository.Instance.GetSimpleGrid(sourceCmb, frameNo, katashik, suffix, partType, partNo, ProdFrom, status, Formatt, ProdTo, startt, Lenn, Fromm, Too, ((Page - 1) * p_length) + 1, Page * p_length);
            ViewData["CountData"] = CountIndex(Page, CountData, p_length, TotalData, this.Settings.ControllerName);

         
            return PartialView("CENTRAL030102WSimpleGrid");
        }
        public ActionResult GetCentralList2(string sourceCmb, string frameNo, string katashik, string suffix, string partType,
           string partNo, string ProdFrom, string status, string Formatt, string ProdTo, string startt, string Lenn, string Fromm, string Too, int p_page, int p_length)
        {
            sourceCmb = sourceCmb == "" ? null : sourceCmb;
            frameNo = frameNo == "" ? null : frameNo;
            katashik = katashik == "" ? null : katashik;
            suffix = suffix == "" ? null : suffix;
            partType = partType == "" ? null : partType;

            partNo = partNo == "" ? null : partNo;
            ProdFrom = ProdFrom == "" ? null : ProdFrom;
            status = status == "" ? null : status;
            Formatt = Formatt == "" ? null : Formatt;
            ProdTo = ProdTo == "" ? null : ProdTo;

            startt = startt == "" ? null : startt;
            Lenn = Lenn == "" ? null : Lenn;
            Fromm = Fromm == "" ? null : Fromm;
            Too = Too == "" ? null : Too;

            if (!String.IsNullOrEmpty(ProdFrom) && !String.IsNullOrEmpty(ProdTo))
            {
                DateTime date_fr = DateTime.ParseExact(ProdFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ProdFrom = date_fr.ToString("yyyy-MM-dd");
                DateTime date_to = DateTime.ParseExact(ProdTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ProdTo = date_to.ToString("yyyy-MM-dd");
            }
            int CountData = CENTRAL030102WRepository.Instance.CountData2(sourceCmb, frameNo, katashik, suffix, partType, partNo, ProdFrom, status, Formatt, ProdTo, startt, Lenn, Fromm, Too);
            int TotalData = (int)Math.Ceiling((double)CountData / (double)p_length);
            int Page = 1;
            if (TotalData < p_page)
                Page = TotalData;
            else
                Page = p_page;
           
            ViewData["SimpleGrid2"] = CENTRAL030102WRepository.Instance.GetSimpleGrid2(sourceCmb, frameNo, katashik, suffix, partType, partNo, ProdFrom, status, Formatt, ProdTo, startt, Lenn, Fromm, Too, ((Page - 1) * p_length) + 1, Page * p_length);
            ViewData["CountData"] = CountIndex(Page, CountData, p_length, TotalData, this.Settings.ControllerName);

        
            return PartialView("CENTRAL030102WSimpleGridH");
        }
    }

}
