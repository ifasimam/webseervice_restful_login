using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Central.Models.CENTRAL030102W;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Text;
namespace Central.Models.CENTRAL010301W
{
    public class CENTRAL010301WRepository
    {
        #region Singleton
        private static CENTRAL010301WRepository instance = null;
        public static CENTRAL010301WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL010301WRepository();
                }
                return instance;
            }
        }

        #endregion
 
        public IEnumerable<SIMPLEGRID> GetSimpleGrid(string BDNO, int p_page, int p_length)
        {
            #region old
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WGetSimpleGrid");
            List<SIMPLEGRID> NewList = new List<SIMPLEGRID>();
            NewList = result.ToList();
            if (BDNO != null || !String.IsNullOrEmpty(BDNO))
            {
                NewList = NewList.Where(p => p.BDNO.Trim() == BDNO.Trim()).ToList();

            }
            //if (FR_CD != null || !String.IsNullOrEmpty(FR_CD))
            //{
            //    NewList = NewList.Where(p => p.ASD_VINNO.Trim() == FR_CD.Trim()).ToList();

            //}
            //if (MSG_NO != null || !String.IsNullOrEmpty(MSG_NO))
            //{
            //    NewList = NewList.Where(p => p.MSG_NO.Trim() == MSG_NO.Trim()).ToList();

            //}
            if (p_page > 0 || p_length > 0)
            {
                NewList = NewList.Where(p => p.ROW_NUM >= p_page && p.ROW_NUM <= p_length).ToList();
            }
            db.Close();
            return NewList;
            #endregion
        }
        public int CountData(string BDNO)
        {
            int ResCount = 0;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL010301W/CENTRAL010301WGetCountDataCentral");
          
            List<SIMPLEGRID> intCount = new List<SIMPLEGRID>();
            intCount = result.ToList();
            if (BDNO != null)
            {
                intCount = intCount.Where(p => p.BDNO.Trim() == BDNO.Trim()).ToList();
            }

            ResCount = intCount.Count();
            db.Close();
            return ResCount;
        }
        public string SaveConfirmation(string BdIDS)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
         
            try
            {
                var result = db.Execute("CENTRAL010301W/CENTRAL010301WSaveConfirmation", new { BDNO=BdIDS});
                db.Close();
                if (result > 0)
                {
                    return "Edit Finished Successfully.";
                }
                else
                {
                    return "Edit Failed.";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
    }
}
