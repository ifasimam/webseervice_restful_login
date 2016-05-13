using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRALMail
{
    public class CENTRAL010101WMailRepository
    {
        #region Singleton
        private static CENTRAL010101WMailRepository instance = null;
        public static CENTRAL010101WMailRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL010101WMailRepository();
                }
                return instance;
            }
        }

        #endregion

        public bool sendEmail(string processID, string subject, string to, string cc, string bcc)
        {
            bool result = false;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRALMail/CENTRALSendEmail",
                    new
                    {
                        PROCESS_ID = processID,
                        SUBJECT = subject,
                        TO = to,
                        CC = cc,
                        BCC = bcc
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