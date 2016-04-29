
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using System.IO;
using Central.Models.CENTRAL020105W;
using Central.Models.Standards;
using Central.Controllers.Standards;
using NPOI.HSSF.UserModel;

using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;
namespace Central.Controllers
{
    public class CENTRAL020105WController : StandardsController
    {
        protected override void Startup()
        {
            Settings.Title = "Central";
            var ID = Request.QueryString["ID"];
            ViewData["ComboPlant"] = CENTRAL020105WRepository.Instance.GetComboPlant();
            ViewData["ComboTerminal"] = CENTRAL020105WRepository.Instance.GetComboTerminal();
            ViewData["ComboPartCode"] = CENTRAL020105WRepository.Instance.GetComboPartCode();
            ViewData["ConstrainNm1"] = CENTRAL020105WRepository.Instance.GetComboConstrainNm1();
            GetTermFromtting(null, null, null, null, 1, 10);

        }
        public ActionResult GetTermFromtting(string plant, string terminal, string partcode, string partdesc, int p_page, int p_length)
        {
            plant = plant == "" ? null : plant;
            terminal = terminal == "" ? null : terminal;
            partcode = partcode == "" ? null : partcode;
            partdesc = partdesc == "" ? null : partdesc;

            int CountData = CENTRAL020105WRepository.Instance.CountData(plant, terminal, partcode, partdesc);
            int TotalData = (int)Math.Ceiling((double)CountData / (double)p_length);
            int Page = 1;
            if (TotalData < p_page)
                Page = TotalData;
            else
                Page = p_page;
            ViewData["SimpleGrid"] = CENTRAL020105WRepository.Instance.GetSimpleGrid(plant, terminal, partcode, partdesc,((Page - 1) * p_length) + 1, Page * p_length);
            ViewData["CountData"] = CountIndex(Page, CountData, p_length, TotalData, this.Settings.ControllerName);

            return PartialView("CENTRAL020105WSimpleGrid");
        }
        public ActionResult SaveEditGrid(string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val, string format_length_val, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val)
        {
            string result = CENTRAL020105WRepository.Instance.SaveEditGridRepo(ID, PlanCD_val, termCD_val, partCD_val, PartDesc_val, ColV1_val, ConsVal1_val, ColVI2_val, ConsVal2_val, ColVI3_val, ConsVal3_val, format_No_val, format_val_val, format_start_val, format_length_val, valid_fr_val, valid_to_val, changeBy_val, changeDt_val, UpdateBy_val, UpdateDt_val);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDeleteGrid(string PlantID)
        {
            int result = CENTRAL020105WRepository.Instance.GetDeleteGrid(PlantID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
