using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using System.IO;
using Central.Models.CENTRAL030101W;
using Central.Models.Standards;
using Central.Controllers.Standards;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Threading.Tasks;


 
namespace Central.Controllers
{
    public class CENTRAL030101WController : StandardsController
    {
        #region GetComboIDNO
        protected override void Startup()
        {
            string ID = Request.QueryString["ID"];
            string FLAG = Request.QueryString["FLAG"];
            Settings.Title = "Central";
            Settings.ControllerName = "CENTRAL030101W";
            if (FLAG != "Detail2")
            {
                ViewData["GetComboIDNO"] = CENTRAL030101WRepository.Instance.GetComboIDNO(ID);
                ViewData["GetComboPart"] = CENTRAL030101WRepository.Instance.GetComboPart(ID);
                ViewData["GetComboIDNO1"] = CENTRAL030101WRepository.Instance.GetComboIDNO1(ID);
                ViewData["GetNameImg"] = CENTRAL030101WRepository.Instance.GetSystem();
            }
            else
            {
                ViewData["GetComboIDNO2"] = CENTRAL030101WRepository.Instance.GetComboIDNO2(ID);
                ViewData["GetComboPart2"] = CENTRAL030101WRepository.Instance.GetComboPart2(ID);
                ViewData["GetComboIDNO12"] = CENTRAL030101WRepository.Instance.GetComboIDNO12(ID);
                ViewData["GetNameImg2"] = CENTRAL030101WRepository.Instance.GetSystem2();
            }
        }
        #endregion

        
        }
    }
