using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Central.Models.CENTRAL030101W;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Text;
namespace Central.Models.CENTRAL030101W
{
    public class CENTRAL030101WRepository
    {
        #region Singleton
        private static CENTRAL030101WRepository instance = null;
        public static CENTRAL030101WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL030101WRepository();
                }
                return instance;
            }
        }

        #endregion
        
        #region Central
        public IEnumerable<CENTRAL030101W> GetComboIDNO(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboIDNO", new { IDNO=ID});
            db.Close();

            return result;
        }
        public IEnumerable<CENTRAL030101W> GetComboIDNO2(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboIDNO2", new { IDNO = ID });
            db.Close();

            return result;
        }
        #endregion
        #region Central1
        public IEnumerable<CENTRAL030101W> GetComboPart(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboPart", new{IDNO=ID});
            db.Close();

            return result;
        }
        public IEnumerable<CENTRAL030101W> GetComboPart2(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboPart2", new { IDNO = ID });
            db.Close();

            return result;
        }
        #endregion
        #region CentralDetail
        public IEnumerable<CENTRAL030101W> GetComboIDNO1(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboIDNO", new { IDNO = ID });
            db.Close();

            return result;
        }
        public IEnumerable<CENTRAL030101W> GetComboIDNO12(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101GetComboIDNO2", new { IDNO = ID });
            db.Close();

            return result;
        }
        public IEnumerable<CENTRAL030101W> GetSystem()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101WGetSystemImg");
            db.Close();

            return result;
        }
        public IEnumerable<CENTRAL030101W> GetSystem2()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030101W>("CENTRAL030101W/CENTRAL030101WGetSystemImg2");
            db.Close();

            return result;
        }
        #endregion
    }
}
