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


        public bool IsExistInSFBase(string bodyNo, out string message)
        {
            bool result = true;
            message = "Data found";
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                var sfbases = db.Fetch<SFBase>("CENTRAL010101W/CENTRAL010101WGetBodyByBodyNo", new { BodyNo = bodyNo });

                if (sfbases.Count == 0)
                {
                    message = "Data not found";
                    result = false;
                }
                db.Close();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return result;
        }
        #endregion
    }
}