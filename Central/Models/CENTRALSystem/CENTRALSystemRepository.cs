using Central.Models.CENTRALSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRALSystem
{
    public class CENTRALSystemRepository
    {
        #region Singleton
        private static CENTRALSystemRepository instance = null;
        public static CENTRALSystemRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRALSystemRepository();
                }
                return instance;
            }
        }
        #endregion

        public string getSystemValue(string systemType, string systemCD)
        {
            string result = null;
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                IList<CentralSystem> retrieved = db.Fetch<CentralSystem>("CENTRAL010101W/CENTRALSystemGetTextValue", new { SYSTEM_TYPE = systemType, SYSTEM_CD = systemCD });
                db.Close();
                if (retrieved.Count > 0)
                {
                    result = retrieved[0].SYSTEM_VALUE_TXT;
                }
            }
            catch
            {

            }
            return result;
        }
    }
}