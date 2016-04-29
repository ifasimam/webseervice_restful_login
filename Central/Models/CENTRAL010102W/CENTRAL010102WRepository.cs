using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRAL010102W
{
    public class CENTRAL010102WRepository
    {
        #region Singleton
        private static CENTRAL010102WRepository instance = null;
        public static CENTRAL010102WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL010102WRepository();
                }
                return instance;
            }
        }

        public CENTRAL010102W ValidateInDB(string userID)
        {
            CENTRAL010102W result = null;

            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                var tmp = db.Fetch<CENTRAL010102W>("CENTRAL010102W/CENTRAL010102WValidateUser", new { UserID = userID});
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

        public CENTRAL010102W ValidateMac(string mac)
        {
            CENTRAL010102W result = null;

            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                var tmp = db.Fetch<CENTRAL010102W>("CENTRAL010102W/CENTRAL010102WValidateMac", new { Mac = mac });
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

        public int InsertLogin(string userID)
        {
            int result = 0;

            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                result = db.Execute("CENTRAL010102W/CENTRAL010102WInsertLogin", new { UserID = userID });
                db.Close();
            }
            catch
            {

            }
            return result;
        }
        #endregion
    }

}