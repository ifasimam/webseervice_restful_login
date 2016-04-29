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

        public string getMessageContent(string msgID, string[] param)
        {
            string result = "No message defined";
            try
            {
                CENTRALMessage message = getMessage(msgID);
                if (message != null && message.MSG_DESC!=null)
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
                    }
                }
            }
            catch
            {

            }
            return result;
        }

        public CENTRALMessage getMessage(string msgID)
        {
            CENTRALMessage result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                var tmp = db.Fetch<CENTRALMessage>("CENTRALMessage/CENTRALMessageGetMessage", new { msgID = msgID });
                if (tmp.Count > 0)
                {
                    result = tmp[0];
                }
                db.Close();
            }
            catch
            {

            }
            return result;
        }
    }
}