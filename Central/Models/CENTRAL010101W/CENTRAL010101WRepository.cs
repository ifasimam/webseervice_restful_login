using Central.Models.CENTRALMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRAL010101W
{
    public class CENTRAL010101WRepository
    {
        #region Singleton
        private static CENTRAL010101WRepository instance = null;
        public static CENTRAL010101WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL010101WRepository();
                }
                return instance;
            }
        }
        #endregion

        public bool IsExistInSFBase(string bodyNo, string userID, string processID, string process_name, string functionID, out string message, out IList<SFBase> sfBases)
        {
            bool result = true;
            message = "";
            sfBases = null;
            
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                sfBases = db.Fetch<SFBase>("CENTRAL010101W/CENTRAL010101WGetBodyByBodyNo", new { BodyNo = bodyNo });

                if (sfBases.Count == 0)
                {
                    CENTRALMessageDomain cm = CENTRALMessageRepository.Instance.getMessageContent("MCENSTD052E", new string[] { bodyNo, "SF Base table" });
                    message = CENTRALMessageRepository.Instance.getMessageContentAndLog("MCENSTD052E", userID, processID, process_name, functionID, "", new string[] { bodyNo, "SF Base table" });                  
                    result = false;
                }
                //else if (sfBases[0].STS == null)
                //{
                //    message = "Body No.already scanned. Do you want to overwrite?";
                //    retrieved = false;
                //}
                //else if (sfBases[0].STS.ToUpper() == "C")
                //{
                //    message = "Body No. already confirmed";
                //    retrieved = false;
                //}
                db.Close();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return result;
        }
        
        public IList<TerminalFormat> GetTerminalFormats(string termCd)
        {
            IList<TerminalFormat> result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Fetch<TerminalFormat>("CENTRAL010101W/CENTRAL010101WGetTerminalFormat", new { TermCd = termCd });
                db.Close();
            }
            catch
            {
                
            }
            return result;
        }

        public IList<CentralLog> GetCentralLog(string Val)
        {
            IList<CentralLog> result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Fetch<CentralLog>("CENTRAL010101W/CENTRAL010101WGetCentralLog", new { Val = Val });
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public IList<Central> GetCentral(string Val, string BodyNo, string IdNo)
        {
            IList<Central> result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Fetch<Central>("CENTRAL010101W/CENTRAL010101WGetCentral", new { Val = Val, BodyNo = BodyNo, IdNo = IdNo });
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public IList<JudgeStatus> GetJudgeStatus(string IdNo, string BodyNo, string PartID)
        {
            IList<JudgeStatus> result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Fetch<JudgeStatus>("CENTRAL010101W/CENTRAL010101WGetCentral", new { IdNo = IdNo, BodyNo = BodyNo, Val = PartID });
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public IList<CentralHeader> GetCentralHeader(string BodyNo)
        {
            IList<CentralHeader> result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Fetch<CentralHeader>("CENTRAL010101W/CENTRAL010101WGetCentralHeader", new { BodyNo = BodyNo });
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool DeleteCentral(string IdNo)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WDeleteCentral", new { IdNo = IdNo }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool InsertCentral(string MSG_NO, string VAL, string IDNO, string BDNO, string FORMAT_VAL, string FORMAT_START, string FORMAT_LENGHT, string USER)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WInsertCentral", 
                    new { 
                        MSG_NO = MSG_NO, 
                        VAL = VAL, 
                        IDNO = IDNO, 
                        BDNO = BDNO, 
                        FORMAT_VAL = FORMAT_VAL, 
                        FORMAT_START = FORMAT_START, 
                        FORMAT_LENGHT = FORMAT_LENGHT, 
                        USER = USER }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool InsertCentralLog(string ASD_VINNO, string MSG_NO, string BDNO, string IDNO, string VAL, string FORMAT_VAL, string FORMAT_START, string FORMAT_LENGHT, string USER)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WInsertCentralLog",
                    new
                    {
                        ASD_VINNO = ASD_VINNO,
                        MSG_NO = MSG_NO,
                        BDNO = BDNO,
                        IDNO = IDNO,
                        VAL = VAL,
                        FORMAT_VAL = FORMAT_VAL,
                        FORMAT_START = FORMAT_START,
                        FORMAT_LENGHT = FORMAT_LENGHT,
                        USER = USER
                    }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool InsertCentralHeader(string BDNO, string IDNO, int COUNTING_PARTID, string TM_CD, string SUMM_STS, string STS, string USER)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WInsertCentralHeader",
                    new
                    {
                        BDNO = BDNO,
                        IDNO = IDNO,
                        COUNTING_PARTID = COUNTING_PARTID,
                        TM_CD = TM_CD,
                        SUMM_STS = SUMM_STS,
                        STS = STS,
                        USER = USER
                    }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool UpdateCentral(string UserID, string IdNo, string PartID)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WUpdateCentral",
                    new
                    {
                        UserID = UserID,
                        IdNo = IdNo,
                        PartID = PartID
                    }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public bool InsertPartScan(string BD_NO, string PART_ID, int COUNTING_PARTID, string USER_ID)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010101W/CENTRAL010101WInsertPartScan",
                    new
                    {
                        BD_NO = BD_NO,
                        PART_ID = PART_ID,
                        COUNTING_PARTID = COUNTING_PARTID,
                        USER_ID = USER_ID
                    }) > 0;
                db.Close();
            }
            catch
            {

            }
            return result;
        }       
    }
}