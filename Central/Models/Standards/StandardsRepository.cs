/************************************************************************************************
 * Program History : 
 * 
 * Project Name     : CORPORATE INFORMATION SYSTEM
 * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
 * Function Id      : Standards
 * Function Name    : Standards Repository
 * Function Group   : Standards
 * Program Id       : StandardsRepository
 * Program Name     : Standards Repository Model
 * Program Type     : Model
 * Description      : 
 * Environment      : .NET 4.0, ASP MVC 4.0
 * Author           : FID.Witan
 * Version          : 01.00.00
 * Creation Date    : 17/02/2016 14:00:40
 * 
 * Update history     Re-fix date       Person in charge      Description 
 *
 * Copyright(C) 2016 - . All Rights Reserved                                                                                              
 *************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using Central.Models.Standards;

namespace Central.Models.Standards
{
    public class StandardsRepository
    {
        #region Singleton
        private static StandardsRepository instance = null;
        public static StandardsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StandardsRepository();
                }
                return instance;
            }
        }
        #endregion

        #region Get Counter Notification
        //public List<HeaderNotification> GetHeaderNotification(string noreg)
        //{

        //    IDBContext db = DatabaseManager.Instance.GetContext();
        //    return db.Query<HeaderNotification>("Standards/GetNotificationHeader", new { NOREG = noreg }).ToList();

        //}

        //public IEnumerable<HeaderNotification> GetHeaderNotification(string noreg) 
        //{
        //    IDBContext db = DatabaseManager.Instance.GetContext();
        //    return db.Fetch<HeaderNotification>("Standards/GetNotificationHeader", new { NOREG = noreg }).ToList();
        //}

        public string doUpdateNotif(string data,string noreg)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            string result = "1";
            db.Execute("Standards/UpdateNotification", new { NOTIF_ID = data, NOREG = noreg });
            db.Close();

            return result;
        }
        #endregion
    }
}
