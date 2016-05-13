using Central.Models.CENTRAL010101W;
using Central.Models.CENTRALMail;
using Central.Models.CENTRALMessage;
using Central.Models.CENTRALSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Central.Controllers
{
    public class CENTRAL010101WController : Controller
    {
        private const string FUNCTION_ID = "CENTRAL010101W";
        private const string BODY_VALIDATION = "Body Validation";

        private const string MSG = "MESSAGE";
        private const string MSG_CD = "MESSAGECD";
        private const string RSLT = "RESULT";
        private const string SFBASE = "SFBASE";
        private const string AMT = "AMOUNT PART";

        private const string NG_NV = "[NG-VA] data not valid";//1
        private const string NG_NV_VAL = "Not Valid";
        private const string NG_BC = "[NG-BC] part id blocked";//2
        private const string NG_BC_VAL = "BC";
        private const string NG_EX = " [NG-EX] data already confirmed";//3
        private const string NG_EX_VAL = "EX";
        private const string NG_ER = " [NG-RL]";//4
        private const string NG_ER_VAL = "[NG-RL] Error User Role";
        private const string OK_N = "[OK-N]";//5
        private const string OK_N_VAL = "[OK-N] New Part";
        private const string OK_R = "[OK-R]";//6
        private const string OK_R_VAL = "[OK-R] Replacement";


        [HttpPost]
        public JsonResult CheckDuplicationPart(string partId, string parts)
        {

            bool result = true;
            string message = "Error ";

            try
            {
                if (parts.Split(',').ToList<string>().Where(i => i.Contains(partId)).ToList().Count == 0)
                {
                    message = "nothing ";
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message + " : Same Part ID scan more than one values=" + parts;
            }

            return Json(new
            {
                status = result,
                content = message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMessageAndLog(string messageID, string userID, string area, string functionID, string processID)
        {
            bool result = true;
            string message = message = CENTRALMessageRepository.Instance.getMessageContentAndLog(messageID, userID, processID, area, functionID, "", null); ;

            return Json(new
            {
                status = result,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BodyValidation(string bodyNo, string termCd, int roleId, string userID)
        {
            string processID = CENTRALMessageRepository.Instance.getProcessID();

            Dictionary<string, object> resultMap = GetBodyValidationResult(bodyNo, termCd, roleId, userID, processID);
            bool result = (bool)resultMap[CENTRAL010101WController.RSLT];
            string message = (string)resultMap[CENTRAL010101WController.MSG];
            string messageCd = (string)resultMap[CENTRAL010101WController.MSG_CD];
            int amountPart = (int)resultMap[CENTRAL010101WController.AMT];

            return Json(new
            {
                status = result,
                amountPart = amountPart,
                content_cd = messageCd,
                content = message
            }, JsonRequestBehavior.AllowGet);
        }

        public string bodyNo { get; set; }
        public string termCd { get; set; }
        public int originalAmountPart { get; set; }
        public string userID { get; set; }
        public int roleId { get; set; }
        public List<string> partIDs { get; set; }

        [HttpGet]
        public JsonResult ConfirmPartValidation(string bodyNo, string termCd, int originalAmountPart, string userID, int roleId, List<string> partIDs)
        {            
            this.bodyNo = bodyNo;
            this.termCd = termCd;
            this.originalAmountPart = originalAmountPart;
            this.userID = userID;
            this.roleId = roleId;
            this.partIDs = partIDs;

            Thread th = new Thread(new ThreadStart(this.ConfirmationProcess));
            th.Start();

            string message = CENTRALMessageRepository.Instance.getMessage("MCENT111207I").MSG_DESC;
            return Json(new
            {
                status = true,
                content = message
            }, JsonRequestBehavior.AllowGet);
        }

        private void ConfirmationProcess()
        {
            ConfirmValidation(bodyNo, termCd, originalAmountPart, userID, roleId, partIDs);
        }

        private void ConfirmValidation(string bodyNo, string termCd, int originalAmountPart, string userID, int roleId, List<string> partIDs)
        {
            string processID = CENTRALMessageRepository.Instance.getProcessID();
            List<SparePart> parts = new List<SparePart>();
            Dictionary<string, int> rekap = new Dictionary<string, int>();
            Dictionary<string, object> resultMap = GetBodyValidationResult(bodyNo, termCd, roleId, userID, processID);
            string message = (string)resultMap[CENTRAL010101WController.MSG];
            int amountPart = (int)resultMap[CENTRAL010101WController.AMT];
            IList<SFBase> filteredSfBases = (IList<SFBase>)resultMap[CENTRAL010101WController.SFBASE];
            bool result = (bool)resultMap[CENTRAL010101WController.RSLT];

            if (result)
            {
                result = amountPart == originalAmountPart;

                if (result)
                {
                    int value = 0;
                    string NG = "NOT GOOD";
                    string KEY = "";
                    result = (bool)resultMap[CENTRAL010101WController.RSLT];

                    if (result)
                    {
                        rekap[NG] = 0;
                        rekap[CENTRAL010101WController.OK_N] = 0;
                        rekap[CENTRAL010101WController.OK_R] = 0;
                        foreach (string partID in partIDs)
                        {
                            parts.Add(GetSparePart(processID, partID, bodyNo, roleId, filteredSfBases));
                        }

                        foreach (SparePart part in parts)
                        {
                            if (part.StatusCD.Contains("OK"))
                            {
                                KEY = part.StatusCD;
                            }
                            else
                            {
                                KEY = NG;
                            }

                            if (rekap.TryGetValue(KEY, out value))
                            {
                                rekap[KEY] = value + 1;
                            }
                            else
                            {
                                rekap[KEY] = 1;
                            }
                        }

                        if (rekap[NG] == 0)
                        {
                            result = true;
                            foreach (SparePart part in parts)
                            {
                                SFBase sfBases = filteredSfBases.Where(i => i.validFormat.PART_CD.Equals(part.PartID)).FirstOrDefault();

                                if (rekap[CENTRAL010101WController.OK_R] == 0)
                                {
                                    CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
                                        sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
                                        sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
                                    CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
                                }
                                else
                                {
                                    CENTRAL010101WRepository.Instance.InsertCentralLog(sfBases.ASD_VINNO, sfBases.validFormat.MSG_NO,
                                    sfBases.BDNO, sfBases.IDNO, part.PartID, sfBases.validFormat.FORMAT_VAL,
                                    sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);

                                    CENTRAL010101WRepository.Instance.DeleteCentral(sfBases.IDNO);
                                    CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
                                        sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
                                        sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
                                    CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
                                    if (part.StatusCD.Equals(CENTRAL010101WController.OK_R))
                                    {
                                        CENTRAL010101WRepository.Instance.UpdateCentral(userID, sfBases.IDNO, part.PartID);
                                    }
                                }
                            }
                            message = CENTRALMessageRepository.Instance.getMessageContent("MCENT111207I", null).MSG_DESC; //"Part ID successfully stored";
                        }
                        else
                        {
                            result = false;
                            //message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111216E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                            //message = "Result of validation: ";
                            //foreach (string key in rekap.Keys)
                            //{
                            //    message = message + key;
                            //}
                            //message = message + " Data not successfully stored. Click Clear button to scan";
                        }
                    }
                }
                else
                {
                    result = false;
                    message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111215E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                }
            }

            if (result == false)
            {
                string subject = CENTRALSystemRepository.Instance.getSystemValue("EMAIL_SUBJECT", "CENTRAL010101W");
                string to = CENTRALSystemRepository.Instance.getSystemValue("EMAIL_TO", "CENTRAL010101W");
                string cc = CENTRALSystemRepository.Instance.getSystemValue("EMAIL_CC", "CENTRAL010101W");
                string bcc = CENTRALSystemRepository.Instance.getSystemValue("EMAIL_BCC", "CENTRAL010101W");

                CENTRAL010101WMailRepository.Instance.sendEmail(processID, subject, to, cc, bcc);
            }
        }

        [HttpGet]
        public JsonResult ConfirmFormat(string bodyNo, string termCd, int originalAmountPart, string userID, int roleId)
        {
            string processID = CENTRALMessageRepository.Instance.getProcessID();
            List<CentralFormat> formats = new List<CentralFormat>();
            string message = "";
            Dictionary<string, object> resultMap = GetBodyValidationResult(bodyNo, termCd, roleId, userID, processID);
            int amountPart = (int)resultMap[CENTRAL010101WController.AMT];
            bool result = amountPart == originalAmountPart;

            if (result == false)
            {
                message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111215E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
            }
            else
            {
                IList<SFBase> filteredSfBases = (IList<SFBase>)resultMap[CENTRAL010101WController.SFBASE];
                if (filteredSfBases.Count > 0)
                {
                    foreach (var item in filteredSfBases)
                    {
                        string format = item.validFormat.FORMAT_VAL;
                        string partID = item.validFormat.PART_CD;
                        formats.Add(new CentralFormat(partID, format));
                    }
                }
            }

            return Json(
                new
                {
                    status = result,
                    content = message,
                    result = formats,
                    processID = processID,
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ConfirmValidationManual(string bodyNo, string termCd, string userID, int roleId, string processID, List<string> partIDs)
        {
            bool result = false;
            List<SparePart> parts = new List<SparePart>();
            Dictionary<string, int> rekap = new Dictionary<string, int>();
            Dictionary<string, object> resultMap = GetBodyValidationResult(bodyNo, termCd, roleId, userID, processID);
            string message = (string)resultMap[CENTRAL010101WController.MSG];
            IList<SFBase> filteredSfBases = (IList<SFBase>)resultMap[CENTRAL010101WController.SFBASE];

            int value = 0;
            string NG = "NOT GOOD";
            string KEY = "";
            result = (bool)resultMap[CENTRAL010101WController.RSLT];

            if (result)
            {
                rekap[NG] = 0;
                rekap[CENTRAL010101WController.OK_N] = 0;
                rekap[CENTRAL010101WController.OK_R] = 0;
                foreach (string partID in partIDs)
                {
                    parts.Add(GetSparePartManual(partID, bodyNo, roleId, filteredSfBases));
                }

                foreach (SparePart part in parts)
                {
                    if (part.StatusCD.Contains("OK"))
                    {
                        KEY = part.StatusCD;
                    }
                    else
                    {
                        KEY = NG;
                    }

                    if (rekap.TryGetValue(KEY, out value))
                    {
                        rekap[KEY] = value + 1;
                    }
                    else
                    {
                        rekap[KEY] = 1;
                    }
                }

                if (rekap[NG] == 0)
                {
                    result = true;
                    foreach (SparePart part in parts)
                    {
                        SFBase sfBases = filteredSfBases.Where(i => i.validFormat.PART_CD.Equals(part.PartID)).FirstOrDefault();

                        if (rekap[CENTRAL010101WController.OK_R] == 0)
                        {
                            CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
                                sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
                                sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
                            CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
                        }
                        else
                        {
                            CENTRAL010101WRepository.Instance.InsertCentralLog(sfBases.ASD_VINNO, sfBases.validFormat.MSG_NO,
                            sfBases.BDNO, sfBases.IDNO, part.PartID, sfBases.validFormat.FORMAT_VAL,
                            sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);

                            CENTRAL010101WRepository.Instance.DeleteCentral(sfBases.IDNO);
                            CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
                                sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
                                sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
                            CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
                            if (part.StatusCD.Equals(CENTRAL010101WController.OK_R))
                            {
                                CENTRAL010101WRepository.Instance.UpdateCentral(userID, sfBases.IDNO, part.PartID);
                            }
                        }
                    }
                    message = CENTRALMessageRepository.Instance.getMessageContent("MCENT111207I", null).MSG_DESC; //"Part ID successfully stored";
                }
                else
                {
                    result = false;
                    message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111216E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                    //message = "Result of validation: ";
                    //foreach (string key in rekap.Keys)
                    //{
                    //    message = message + key;
                    //}
                    //message = message + " Data not successfully stored. Click Clear button to scan";
                }
            }

            return Json(new
            {
                status = result,
                content = message,
                result = parts
            }, JsonRequestBehavior.AllowGet);
        }

        private SparePart GetSparePart(string partID, string bodyNo, int roleID, IList<SFBase> filteredSfBases)
        {
            string statusCd = "O";
            string statusName = "OK";
            try
            {
                #region b.1 Format Checking
                try
                {
                    filteredSfBases = filteredSfBases.Where(i => i.validFormat.PART_CD == partID).ToList();
                    if (filteredSfBases.Count > 0)
                    {
                        SFBase sfBase = filteredSfBases[0];
                        if (partID.Length == sfBase.validFormat.FORMAT_VAL.Length)
                        {
                            int startIdx = 0;
                            List<string> digitFormat = GetDigitFormat(sfBase.validFormat.FORMAT_VAL);
                            foreach (string format in digitFormat)
                            {
                                if (format.Contains("N"))
                                {
                                    int test = 0;
                                    if (int.TryParse(partID.Substring(startIdx, format.Length), out test) == false)
                                    {
                                        throw new Exception();
                                    }
                                    else
                                    {
                                        startIdx = format.Length;
                                    }
                                }
                                else
                                {
                                    startIdx = format.Length;
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    statusCd = CENTRAL010101WController.NG_NV;
                    statusName = CENTRAL010101WController.NG_NV_VAL;
                    throw new Exception();
                }
                #endregion
                #region b.2 SFN Central Log Checking
                try
                {
                    IList<CentralLog> logs = CENTRAL010101WRepository.Instance.GetCentralLog(partID);
                    if (logs != null && logs.Count > 0)
                    {
                        new Exception();
                    }
                }
                catch
                {
                    statusCd = CENTRAL010101WController.NG_BC;
                    statusName = CENTRAL010101WController.NG_BC_VAL;
                    throw new Exception();
                }
                #endregion
                #region b.3 SFN Central Checking
                IList<Central.Models.CENTRAL010101W.Central> central = null;
                try
                {
                    central = CENTRAL010101WRepository.Instance.GetCentral(partID, null, null);
                    if (central == null || central.Count == 0)
                    {
                        statusCd = CENTRAL010101WController.OK_N;
                        statusName = CENTRAL010101WController.OK_N_VAL;
                        new Exception();
                    }
                    else
                    {
                        if (central[0].BDNO.Equals(bodyNo))
                        {
                            #region b.5 part is installed in the right body
                            if (roleID != 1)
                            {
                                statusCd = CENTRAL010101WController.NG_ER;
                                statusName = CENTRAL010101WController.NG_ER_VAL;
                            }
                            else
                            {
                                statusCd = CENTRAL010101WController.OK_R;
                                statusName = CENTRAL010101WController.OK_R_VAL;
                            }
                            #endregion
                        }
                        else
                        {
                            #region b.4 part is not installed in the right body
                            try
                            {
                                IList<JudgeStatus> js = CENTRAL010101WRepository.Instance.GetJudgeStatus(null, bodyNo, partID);
                                if (js != null && js.Count > 1)
                                {
                                    if (js.Where(i => i.STS.Equals("C")).Any()
                                        && (js.Where(i => i.JUDGE.Equals("OK")).Any() ||
                                        js.Where(i => i.JUDGE.Equals("NG")).Any()))
                                    {
                                        throw new Exception();
                                    }
                                    else if (js.Where(i => i.STS.Equals("R")).Any() ||
                                        (js.Where(i => i.STS.Equals("")).Any() ||
                                        js.Where(i => i.STS == null).Any()))
                                    {
                                        if (roleID != 1)
                                        {
                                            statusCd = CENTRAL010101WController.NG_ER;
                                            statusName = CENTRAL010101WController.NG_ER_VAL;
                                        }
                                        else
                                        {
                                            statusCd = CENTRAL010101WController.OK_R;
                                            statusName = CENTRAL010101WController.OK_R_VAL;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                statusCd = CENTRAL010101WController.NG_EX;
                                statusName = CENTRAL010101WController.NG_EX_VAL;
                            }
                            #endregion
                        }
                    }
                }
                catch
                {
                    throw new Exception();
                }
                #endregion
            }
            catch { }

            SparePart part = new SparePart();
            part.PartID = partID;
            part.StatusCD = statusCd;
            part.StatusName = statusName;
            return part;
        }

        private SparePart GetSparePart(string processID, string partID, string bodyNo, int roleID, IList<SFBase> filteredSfBases)
        {
            string statusCd = "O";
            string statusName = "OK";
            try
            {
                #region b.1 Format Checking
                try
                {
                    filteredSfBases = filteredSfBases.Where(i => i.validFormat.PART_CD == partID).ToList();
                    if (filteredSfBases.Count > 0)
                    {
                        SFBase sfBase = filteredSfBases[0];
                        if (partID.Length == sfBase.validFormat.FORMAT_VAL.Length)
                        {
                            int startIdx = 0;
                            List<string> digitFormat = GetDigitFormat(sfBase.validFormat.FORMAT_VAL);
                            foreach (string format in digitFormat)
                            {
                                if (format.Contains("N"))
                                {
                                    int test = 0;
                                    if (int.TryParse(partID.Substring(startIdx, format.Length), out test) == false)
                                    {
                                        throw new Exception();
                                    }
                                    else
                                    {
                                        startIdx = format.Length;
                                    }
                                }
                                else
                                {
                                    startIdx = format.Length;
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    statusCd = CENTRAL010101WController.NG_NV;
                    statusName = CENTRAL010101WController.NG_NV_VAL;
                    CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111216E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", new string[]{partID, bodyNo});
                    throw new Exception();
                }
                #endregion
                #region b.2 SFN Central Log Checking
                try
                {
                    IList<CentralLog> logs = CENTRAL010101WRepository.Instance.GetCentralLog(partID);
                    if (logs != null && logs.Count > 0)
                    {
                        new Exception();
                    }
                }
                catch
                {
                    statusCd = CENTRAL010101WController.NG_BC;
                    statusName = CENTRAL010101WController.NG_BC_VAL;
                    CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111230E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", new string[] { partID, bodyNo });
                    throw new Exception();
                }
                #endregion
                #region b.3 SFN Central Checking
                IList<Central.Models.CENTRAL010101W.Central> central = null;
                try
                {
                    central = CENTRAL010101WRepository.Instance.GetCentral(partID, null, null);
                    if (central == null || central.Count == 0)
                    {
                        statusCd = CENTRAL010101WController.OK_N;
                        statusName = CENTRAL010101WController.OK_N_VAL;
                        new Exception();
                    }
                    else
                    {
                        if (central[0].BDNO.Equals(bodyNo))
                        {
                            #region b.5 part is installed in the right body
                            if (roleID != 1)
                            {
                                statusCd = CENTRAL010101WController.NG_ER;
                                statusName = CENTRAL010101WController.NG_ER_VAL;
                                CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111231E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", new string[] { partID, bodyNo });
                            }
                            else
                            {
                                statusCd = CENTRAL010101WController.OK_R;
                                statusName = CENTRAL010101WController.OK_R_VAL;
                            }
                            #endregion
                        }
                        else
                        {
                            #region b.4 part is not installed in the right body
                            try
                            {
                                IList<JudgeStatus> js = CENTRAL010101WRepository.Instance.GetJudgeStatus(null, bodyNo, partID);
                                if (js != null && js.Count > 1)
                                {
                                    if (js.Where(i => i.STS.Equals("C")).Any()
                                        && (js.Where(i => i.JUDGE.Equals("OK")).Any() ||
                                        js.Where(i => i.JUDGE.Equals("NG")).Any()))
                                    {
                                        throw new Exception();
                                    }
                                    else if (js.Where(i => i.STS.Equals("R")).Any() ||
                                        (js.Where(i => i.STS.Equals("")).Any() ||
                                        js.Where(i => i.STS == null).Any()))
                                    {
                                        if (roleID != 1)
                                        {
                                            statusCd = CENTRAL010101WController.NG_ER;
                                            statusName = CENTRAL010101WController.NG_ER_VAL;
                                            CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111231E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", new string[] { partID, bodyNo });
                                        }
                                        else
                                        {
                                            statusCd = CENTRAL010101WController.OK_R;
                                            statusName = CENTRAL010101WController.OK_R_VAL;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                statusCd = CENTRAL010101WController.NG_EX;
                                statusName = CENTRAL010101WController.NG_EX_VAL;
                                CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111229E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", new string[] { partID, bodyNo });
                            }
                            #endregion
                        }
                    }
                }
                catch
                {
                    throw new Exception();
                }
                #endregion
            }
            catch { }

            SparePart part = new SparePart();
            part.PartID = partID;
            part.StatusCD = statusCd;
            part.StatusName = statusName;
            return part;
        }
        
        private SparePart GetSparePartManual(string partID, string bodyNo, int roleID, IList<SFBase> filteredSfBases)
        {
            string statusCd = "O";
            string statusName = "OK";
            try
            {
                #region b.1 Format Checking
                //try
                //{
                //    filteredSfBases = filteredSfBases.Where(i => i.validFormat.PART_CD == partID).ToList();
                //    if (filteredSfBases.Count > 0)
                //    {
                //        SFBase sfBase = filteredSfBases[0];
                //        if (partID.Length == sfBase.validFormat.FORMAT_VAL.Length)
                //        {
                //            int startIdx = 0;
                //            List<string> digitFormat = GetDigitFormat(sfBase.validFormat.FORMAT_VAL);
                //            foreach (string format in digitFormat)
                //            {
                //                if (format.Contains("N"))
                //                {
                //                    int test = 0;
                //                    if (int.TryParse(partID.Substring(startIdx, format.Length), out test) == false)
                //                    {
                //                        throw new Exception();
                //                    }
                //                    else
                //                    {
                //                        startIdx = format.Length;
                //                    }
                //                }
                //                else
                //                {
                //                    startIdx = format.Length;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception();
                //        }
                //    }
                //    else
                //    {
                //        throw new Exception();
                //    }
                //}
                //catch
                //{
                //    statusCd = CENTRAL010101WController.NG_NV;
                //    statusName = CENTRAL010101WController.NG_NV_VAL;
                //    throw new Exception();
                //}
                #endregion
                #region b.2 SFN Central Log Checking
                try
                {
                    IList<CentralLog> logs = CENTRAL010101WRepository.Instance.GetCentralLog(partID);
                    if (logs != null && logs.Count > 0)
                    {
                        new Exception();
                    }
                }
                catch
                {
                    statusCd = CENTRAL010101WController.NG_BC;
                    statusName = CENTRAL010101WController.NG_BC_VAL;
                    throw new Exception();
                }
                #endregion
                #region b.3 SFN Central Checking
                IList<Central.Models.CENTRAL010101W.Central> central = null;
                try
                {
                    central = CENTRAL010101WRepository.Instance.GetCentral(partID, null, null);
                    if (central == null || central.Count == 0)
                    {
                        statusCd = CENTRAL010101WController.OK_N;
                        statusName = CENTRAL010101WController.OK_N_VAL;
                        new Exception();
                    }
                    else
                    {
                        if (central[0].BDNO.Equals(bodyNo))
                        {
                            #region b.5 part is installed in the right body
                            if (roleID != 1)
                            {
                                statusCd = CENTRAL010101WController.NG_ER;
                                statusName = CENTRAL010101WController.NG_ER_VAL;
                            }
                            else
                            {
                                statusCd = CENTRAL010101WController.OK_R;
                                statusName = CENTRAL010101WController.OK_R_VAL;
                            }
                            #endregion
                        }
                        else
                        {
                            #region b.4 part is not installed in the right body
                            try
                            {
                                IList<JudgeStatus> js = CENTRAL010101WRepository.Instance.GetJudgeStatus(null, bodyNo, partID);
                                if (js != null && js.Count > 1)
                                {
                                    if (js[0].STS.Equals("C") && (js[0].JUDGE.Equals("OK") || js[0].JUDGE.Equals("NG")))
                                    {
                                        throw new Exception();
                                    }
                                    else if (js[0].STS.Equals("R") || (js[0].STS.Trim().Equals("") || js[0].STS == null))
                                    {
                                        if (roleID != 1)
                                        {
                                            statusCd = CENTRAL010101WController.NG_ER;
                                            statusName = CENTRAL010101WController.NG_ER_VAL;
                                        }
                                        else
                                        {
                                            statusCd = CENTRAL010101WController.OK_R;
                                            statusName = CENTRAL010101WController.OK_R_VAL;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                statusCd = CENTRAL010101WController.NG_EX;
                                statusName = CENTRAL010101WController.NG_EX_VAL;
                            }
                            #endregion
                        }
                    }
                }
                catch
                {
                    throw new Exception();
                }
                #endregion
            }
            catch { }

            SparePart part = new SparePart();
            part.PartID = partID;
            part.StatusCD = statusCd;
            part.StatusName = statusName;
            return part;
        }

        private List<string> GetDigitFormat(string format)
        {
            List<string> formats = new List<string>();
            string temp = null;
            int index = 0;
            for (int i = 0; i < format.Length; i++)
            {
                if (temp == null)
                {
                    temp = format.Substring(i, 1);
                    formats.Add(temp);
                }
                else if (temp.Equals(format.Substring(i, 1)))
                {
                    formats[index] = formats[index] + temp;
                }
                else
                {
                    index = index + 1;
                    temp = format.Substring(i, 1);
                    formats.Add(temp);
                }
            }
            return formats;
        }

        private Dictionary<string, object> GetBodyValidationResult(string bodyNo, string termCd, int roleID, string userID, string processID)
        {
            Dictionary<string, object> resultMap = new Dictionary<string, object>();
            IList<TerminalFormat> formats = null;
            IList<SFBase> sfBases = null;
            IList<SFBase> filteredSfBases = new List<SFBase>();
            int index = 0;
            bool result = true;
            string message = "";
            string message_cd = "I";
            int amountPart = 0;
            try
            {
                if (bodyNo.Length > 5)
                {
                    bodyNo = bodyNo.Substring(0, 5);
                }
                result = CENTRAL010101WRepository.Instance.IsExistInSFBase(bodyNo, userID, processID, BODY_VALIDATION, FUNCTION_ID, out message, out sfBases);
                if (result == false)
                {
                    throw new Exception();
                }
                formats = CENTRAL010101WRepository.Instance.GetTerminalFormats(termCd);
                if (formats.Count > 0)
                {
                    foreach (TerminalFormat format in formats)
                    {
                        try
                        {
                            var bodies = sfBases.Where(i => ((string)i.GetType().GetProperty(format.COL_VI1.Trim()).GetValue(i, null)).Contains(format.CONS_VAL1.Trim()));
                            bodies = bodies.Where(i => ((string)i.GetType().GetProperty(format.COL_VI2.Trim()).GetValue(i, null)).Contains(format.CONS_VAL2.Trim()));
                            bodies = bodies.Where(i => ((string)i.GetType().GetProperty(format.COL_VI3.Trim()).GetValue(i, null)).Contains(format.CONS_VAL3.Trim()));
                            foreach (SFBase bodyPart in bodies)
                            {
                                index++;
                                bodyPart.COUNTING_PARTID = index;
                                bodyPart.validFormat = format;
                                filteredSfBases.Add(bodyPart);
                            }
                        }
                        catch { }
                    }

                    amountPart = filteredSfBases.Count();
                    IList<CentralHeader> pf = CENTRAL010101WRepository.Instance.GetCentralHeader(bodyNo, null);
                    if (pf != null && pf.Count() > 0)
                    {
                        //0: Operator
                        //1: Team leader
                        if (roleID == 1)
                        {
                            if (pf.Where(i => i.STS == "C").Any())
                            {
                                message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111205E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                                throw new Exception();
                            }
                            else if (pf.Where(i => i.STS == "R").Any())
                            {
                                message_cd = "W";
                                message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111204C", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                            }
                            else
                            {
                                if (amountPart > 0)
                                {
                                    message = "Body No.already exist, please continue to rescan";
                                }
                                else
                                {
                                    message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111213E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                                    throw new Exception();
                                }
                            }
                        }
                        else
                        {
                            message_cd = "E";
                            message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111214E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                            //message = "You are not authorized to execute rescanning existing,";
                            //message = message + "\nBody No. Please contact Administrator for access.";
                            throw new Exception();
                        }
                    }
                    else
                    {
                        if (amountPart > 0)
                        {
                            message = "Body No.already exist, please continue to rescan";
                        }
                        else
                        {
                            message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111213E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111213E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
                    throw new Exception();
                }

            }
            catch
            {
                result = false;
            }

            resultMap[CENTRAL010101WController.MSG_CD] = message_cd;
            resultMap[CENTRAL010101WController.MSG] = message;
            resultMap[CENTRAL010101WController.AMT] = amountPart;
            resultMap[CENTRAL010101WController.RSLT] = result;
            resultMap[CENTRAL010101WController.SFBASE] = filteredSfBases;
            return resultMap;
        }

        #region backup previous
        //[HttpGet]
        //public JsonResult ConfirmValidation(string bodyNo, string termCd, int originalAmountPart, string userID, int roleId, List<string> partIDs)
        //{
        //    string processID = CENTRALMessageRepository.Instance.getProcessID();
        //    List<SparePart> parts = new List<SparePart>();
        //    Dictionary<string, int> rekap = new Dictionary<string, int>();
        //    Dictionary<string, object> resultMap = GetBodyValidationResult(bodyNo, termCd, roleId, userID, processID);
        //    string message = (string)resultMap[CENTRAL010101WController.MSG];
        //    int amountPart = (int)resultMap[CENTRAL010101WController.AMT];
        //    IList<SFBase> filteredSfBases = (IList<SFBase>)resultMap[CENTRAL010101WController.SFBASE];

        //    bool retrieved = amountPart == originalAmountPart;

        //    if (retrieved)
        //    {
        //        int value = 0;
        //        string NG = "NOT GOOD";
        //        string KEY = "";
        //        retrieved = (bool)resultMap[CENTRAL010101WController.RSLT];

        //        if (retrieved)
        //        {
        //            rekap[NG] = 0;
        //            rekap[CENTRAL010101WController.OK_N] = 0;
        //            rekap[CENTRAL010101WController.OK_R] = 0;
        //            foreach (string partID in partIDs)
        //            {
        //                parts.Add(GetSparePart(partID, bodyNo, roleId, filteredSfBases));
        //            }

        //            foreach (SparePart part in parts)
        //            {
        //                if (part.StatusCD.Contains("OK"))
        //                {
        //                    KEY = part.StatusCD;
        //                }
        //                else
        //                {
        //                    KEY = NG;
        //                }

        //                if (rekap.TryGetValue(KEY, out value))
        //                {
        //                    rekap[KEY] = value + 1;
        //                }
        //                else
        //                {
        //                    rekap[KEY] = 1;
        //                }
        //            }

        //            if (rekap[NG] == 0)
        //            {
        //                retrieved = true;
        //                foreach (SparePart part in parts)
        //                {
        //                    SFBase sfBases = filteredSfBases.Where(i => i.validFormat.PART_CD.Equals(part.PartID)).FirstOrDefault();

        //                    if (rekap[CENTRAL010101WController.OK_R] == 0)
        //                    {
        //                        CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
        //                            sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
        //                            sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
        //                        CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
        //                    }
        //                    else
        //                    {
        //                        CENTRAL010101WRepository.Instance.InsertCentralLog(sfBases.ASD_VINNO, sfBases.validFormat.MSG_NO,
        //                        sfBases.BDNO, sfBases.IDNO, part.PartID, sfBases.validFormat.FORMAT_VAL,
        //                        sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);

        //                        CENTRAL010101WRepository.Instance.DeleteCentral(sfBases.IDNO);
        //                        CENTRAL010101WRepository.Instance.InsertCentral(sfBases.validFormat.MSG_NO, part.PartID,
        //                            sfBases.IDNO, sfBases.BDNO, sfBases.validFormat.FORMAT_VAL,
        //                            sfBases.validFormat.FORMAT_START, sfBases.validFormat.FORMAT_LENGTH, userID);
        //                        CENTRAL010101WRepository.Instance.InsertCentralHeader(sfBases.BDNO, sfBases.IDNO, sfBases.COUNTING_PARTID, termCd, "0", null, userID);
        //                        if (part.StatusCD.Equals(CENTRAL010101WController.OK_R))
        //                        {
        //                            CENTRAL010101WRepository.Instance.UpdateCentral(userID, sfBases.IDNO, part.PartID);
        //                        }
        //                    }
        //                }
        //                message = CENTRALMessageRepository.Instance.getMessageContent("MCENT111207I", null).MSG_DESC; //"Part ID successfully stored";
        //            }
        //            else
        //            {
        //                retrieved = false;
        //                message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111216E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
        //                //message = "Result of validation: ";
        //                //foreach (string key in rekap.Keys)
        //                //{
        //                //    message = message + key;
        //                //}
        //                //message = message + " Data not successfully stored. Click Clear button to scan";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        retrieved = false;
        //        message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENT111215E", userID, processID, BODY_VALIDATION, FUNCTION_ID, "", null);
        //    }

        //    return Json(new
        //    {
        //        status = retrieved,
        //        content = message,
        //        retrieved = parts
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}
