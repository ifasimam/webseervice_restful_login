using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRALMessage
{
    public class CENTRALMessageRepository
    {
        #region Singleton
        private static CENTRALMessageRepository instance = null;
        public static CENTRALMessageRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRALMessageRepository();
                }
                return instance;
            }
        }
        #endregion

        public CENTRALMessageDomain getMessageContent(string msgID, string[] param)
        {
            CENTRALMessageDomain message = new CENTRALMessageDomain();
            string result = "No message defined";
            try
            {
                message = getMessage(msgID);
                if (message != null && message.MSG_DESC != null)
                {
                    result = message.MSG_DESC;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            result = result
                                .Replace("{" + i.ToString() + "}", param[i])
                                .Replace("[" + i.ToString() + "]", param[i]);
                        }
                        message.MSG_DESC = result;
                    }
                }
            }
            catch
            {

            }
            return message;
        }

        public string getMessageContentAndLog(string msgID, string userID, string processID, string processName, string functionID, string remark, string[] param)
        {
            string result = "No message defined";
            try
            {
                CENTRALMessageDomain message = getMessage(msgID);
                if (message != null && message.MSG_DESC != null)
                {
                    result = message.MSG_DESC;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            result = result
                                .Replace("{" + i.ToString() + "}", param[i])
                                .Replace("[" + i.ToString() + "]", param[i]);
                        }
                        message.MSG_DESC = result;
                    }
                    CENTRALLogMonitoring par = new CENTRALLogMonitoring();
                    par.seqID = CENTRALMessageRepository.Instance.getLogDetailSequence(par.processID);
                    par.message = message;
                    par.processID = processID;
                    par.functionID = functionID;
                    par.remark = remark;
                    par.processName = processName;
                    par.userID = userID;
                    doLog(par);
                }
            }
            catch(Exception e)
            {
                string msg = "";
                if (e.Message == null)
                {
                    msg = e.InnerException.Message;
                }
                else
                {
                    msg = e.Message;
                }
                result = result + " error "+msg;
            }

            return result;
        }

        private void doLog(CENTRALLogMonitoring param)
        {
            try
            {
                param.seqID = getLogDetailSequence(param.processID);
                if (param.seqID.Equals("1"))
                {
                    insertLogHeader(param);
                }
                insertLogDetail(param);
            }
            catch (Exception d)
            {
                throw d;
            }
        }

        public CENTRALMessageDomain getMessage(string msgID)
        {
            CENTRALMessageDomain result = new CENTRALMessageDomain();
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                var temp = db.Fetch<CENTRALMessageDomain>("CENTRALMessage/CENTRALMessageGetMessage", new { msgID = msgID });
                if (temp.Count > 0)
                {
                    result = temp[0];
                }
                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public string getProcessID()
        {
            string result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.ExecuteScalar<string>("CENTRALMessage/CENTRALMessageGetProcessID", null);

                db.Close();
            }
            catch
            {

            }
            return result;
        }

        public string getLogDetailSequence(string PROCESS_ID)
        {
            string result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.ExecuteScalar<string>("CENTRALMessage/CENTRALMessageGetLogDetailSeq", new { PROCESS_ID = PROCESS_ID });

                db.Close();
            }
            catch
            {

            }
            return result;
        }

        private bool insertLogHeader(CENTRALLogMonitoring param)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRALMessage/CENTRALMessageInsertLogHeader", new
                {
                    PROCESS_ID = param.processID,
                    USER_ID = param.userID,
                    SUB_SYSTEMID = "",
                    FUNCTION_ID = param.functionID,
                    STS = param.message.MSG_TYPE
                }) > 0;

                db.Close();
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }

        private bool insertLogDetail(CENTRALLogMonitoring param)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRALMessage/CENTRALMessageInsertLogDetail", new
                {
                    LOG_SEQ = param.seqID,
                    PROCESS_ID = param.processID,
                    MSG_ID = param.message.MSG_ID,
                    LOCATION = param.functionID,
                    MSG_DESC = param.message.MSG_DESC,
                    CREATED_BY = param.userID
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