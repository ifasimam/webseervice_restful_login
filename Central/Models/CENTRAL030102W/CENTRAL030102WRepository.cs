using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Central.Models.CENTRAL030102W;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Text;
namespace Central.Models.CENTRAL030102W
{
    public class CENTRAL030102WRepository
    {
        #region Singleton
        private static CENTRAL030102WRepository instance = null;
        public static CENTRAL030102WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL030102WRepository();
                }
                return instance;
            }
        }
       
        #endregion
        public IEnumerable<CENTRAL030102W> GetComboPartType()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030102W>("CENTRAL030102W/CENTRAL030102WGetComboPartType");
            List<CENTRAL030102W> dbTemp = new List<CENTRAL030102W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PART_CD).Select(p=>p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<CENTRAL030102W> GetComboFormat()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030102W>("CENTRAL030102W/CENTRAL030102WGetComboFormat");
            List<CENTRAL030102W> dbTemp = new List<CENTRAL030102W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.FORMAT_SEQ).Select(p=>p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<CENTRAL030102W> GetComboSuffix()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030102W>("CENTRAL030102W/CENTRAL030102WGetComboSuffix");
            List<CENTRAL030102W> dbTemp = new List<CENTRAL030102W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.SALES_SFX).Select(p=>p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<CENTRAL030102W> GetComboStatus()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL030102W>("CENTRAL030102W/CENTRAL030102WGetComboStatus");
            List<CENTRAL030102W> dbTemp = new List<CENTRAL030102W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.SYSTEM_VALUE_TXT).Select(p=>p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<SIMPLEGRID> GetSimpleGrid(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to, int Page, int length)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetSimpleGrid");
            List<SIMPLEGRID> NewList = new List<SIMPLEGRID>();
            NewList = result.ToList();
            //if (source != null || !String.IsNullOrEmpty(source))
            //{
            //    NewList = NewList.Where(p => p.STS.Trim() == source).ToList();

            //}
            if (frameNo != null || !String.IsNullOrEmpty(frameNo))
            {
                NewList = NewList.Where(p => p.ASD_VINNO.Trim() == frameNo).ToList();

            }

            if (katashik != null || !String.IsNullOrEmpty(katashik))
            {
                NewList = NewList.Where(p => p.KATASHIKI.Trim() == katashik).ToList();

            }
            if (suffix != null || !String.IsNullOrEmpty(suffix))
            {
                NewList = NewList.Where(p => p.SALES_SFX.Trim() == suffix).ToList();

            }
            if (parttype != null || !String.IsNullOrEmpty(parttype))
            {
                NewList = NewList.Where(p => p.PART_CD.Trim() == parttype).ToList();

            }
            if (partNo != null || !String.IsNullOrEmpty(partNo))
            {
                NewList = NewList.Where(p => p.VAL.Trim() == partNo).ToList();

            }
            if (prodFrom != null && prodTo!=null)
            {
                DateTime? date_fr = DateTime.ParseExact(prodFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime? date_to = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                NewList = NewList.Where(p => p.LO_DATE >= date_fr && p.LO_DATE <= date_to).ToList();

            }
            if (status != null || !String.IsNullOrEmpty(status))
            {
                NewList = NewList.Where(p => p.SYSTEM_VALUE_TXT.Trim() == status).ToList();

            }
            if (format != null || !String.IsNullOrEmpty(format))
            {
                NewList = NewList.Where(p => p.FR_CD.Trim() == format).ToList();

            }
            if (Page > 0 || length >0)
            {
                NewList = NewList.Where(p => p.ROW_NUM >= Page && p.ROW_NUM <= length).ToList();
            }
            db.Close();
            return NewList;
        }
        public IEnumerable<SIMPLEGRID> GetSimpleGrid2(string source, string frameNo, string katashik, string suffix, string parttype,
           string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to, int Page, int length)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetSimpleGrid2");
            List<SIMPLEGRID> NewList = new List<SIMPLEGRID>();
            NewList = result.ToList();
            //if (source != null || !String.IsNullOrEmpty(source))
            //{
            //    NewList = NewList.Where(p => p.STS.Trim() == source).ToList();

            //}
            if (frameNo != null || !String.IsNullOrEmpty(frameNo))
            {
                NewList = NewList.Where(p => p.ASD_VINNO.Trim() == frameNo).ToList();

            }

            if (katashik != null || !String.IsNullOrEmpty(katashik))
            {
                NewList = NewList.Where(p => p.KATASHIKI.Trim() == katashik).ToList();

            }
            if (suffix != null || !String.IsNullOrEmpty(suffix))
            {
                NewList = NewList.Where(p => p.SALES_SFX.Trim() == suffix).ToList();

            }
            if (parttype != null || !String.IsNullOrEmpty(parttype))
            {
                NewList = NewList.Where(p => p.PART_CD.Trim() == parttype).ToList();

            }
            if (partNo != null || !String.IsNullOrEmpty(partNo))
            {
                NewList = NewList.Where(p => p.VAL.Trim() == partNo).ToList();

            }
            if (prodFrom != null && prodTo != null)
            {
                DateTime? date_fr = DateTime.ParseExact(prodFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime? date_to = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                NewList = NewList.Where(p => p.LO_DATE >= date_fr && p.LO_DATE <= date_to).ToList();

            }
            if (status != null || !String.IsNullOrEmpty(status))
            {
                NewList = NewList.Where(p => p.SYSTEM_VALUE_TXT.Trim() == status).ToList();

            }
            if (format != null || !String.IsNullOrEmpty(format))
            {
                NewList = NewList.Where(p => p.FR_CD.Trim() == format).ToList();

            }
            if (Page > 0 || length > 0)
            {
                NewList = NewList.Where(p => p.ROW_NUM >= Page && p.ROW_NUM <= length).ToList();
            }
            db.Close();
            return NewList;
        }
        public int CountData(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to)
        {
            int ResCount=0;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetCountDataCentral");
            
            List<SIMPLEGRID> intCount = new List<SIMPLEGRID>();
            intCount = result.ToList();
        
           
            if (source != null || !String.IsNullOrEmpty(source))
            {
                intCount = intCount.Where(p => p.STS == katashik).ToList();
               

            }
            if (frameNo != null || !String.IsNullOrEmpty(frameNo))
            {

                intCount = intCount.Where(p => p.ASD_VINNO == frameNo).ToList();
             

            }

            if (katashik != null || !String.IsNullOrEmpty(katashik))
            {
                intCount = intCount.Where(p => p.KATASHIKI == katashik).ToList();
   

            }
            if (suffix != null || !String.IsNullOrEmpty(suffix))
            {
                intCount = intCount.Where(p => p.SALES_SFX == suffix).ToList();
             

            }
            if (parttype != null || !String.IsNullOrEmpty(parttype))
            {
                intCount = intCount.Where(p => p.PART_CD == parttype).ToList();
        

            }
            if (partNo != null || !String.IsNullOrEmpty(partNo))
            {
                intCount = intCount.Where(p => p.VAL == partNo).ToList();
               

            }
         
            if (prodFrom != null && prodTo != null)
            {
                
                    DateTime? date_fr = DateTime.ParseExact(prodFrom, "yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture);
                    DateTime? date_to = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime? date_LO = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                    intCount = intCount.Where(p => p.LO_DATE >= date_fr && p.LO_DATE <= date_to).ToList();
             

            }
            if (status != null || !String.IsNullOrEmpty(status))
            {
                intCount = intCount.Where(p => p.STS == status).ToList();
            

            }
            if (format != null || !String.IsNullOrEmpty(format))
            {
                intCount = intCount.Where(p => p.FR_CD == format).ToList();
               

            }
            ResCount = intCount.Count();
            db.Close();
            return ResCount;
        }
        public int CountData2(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to)
        {
            int ResCount = 0;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetCountDataCentral2");

            List<SIMPLEGRID> intCount = new List<SIMPLEGRID>();
            intCount = result.ToList();


            if (source != null || !String.IsNullOrEmpty(source))
            {
                intCount = intCount.Where(p => p.STS == katashik).ToList();


            }
            if (frameNo != null || !String.IsNullOrEmpty(frameNo))
            {

                intCount = intCount.Where(p => p.ASD_VINNO == frameNo).ToList();


            }

            if (katashik != null || !String.IsNullOrEmpty(katashik))
            {
                intCount = intCount.Where(p => p.KATASHIKI == katashik).ToList();


            }
            if (suffix != null || !String.IsNullOrEmpty(suffix))
            {
                intCount = intCount.Where(p => p.SALES_SFX == suffix).ToList();


            }
            if (parttype != null || !String.IsNullOrEmpty(parttype))
            {
                intCount = intCount.Where(p => p.PART_CD == parttype).ToList();


            }
            if (partNo != null || !String.IsNullOrEmpty(partNo))
            {
                intCount = intCount.Where(p => p.VAL == partNo).ToList();


            }

            if (prodFrom != null && prodTo != null)
            {

                DateTime? date_fr = DateTime.ParseExact(prodFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime? date_to = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime? date_LO = DateTime.ParseExact(prodTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                intCount = intCount.Where(p => p.LO_DATE >= date_fr && p.LO_DATE <= date_to).ToList();


            }
            if (status != null || !String.IsNullOrEmpty(status))
            {
                intCount = intCount.Where(p => p.STS == status).ToList();


            }
            if (format != null || !String.IsNullOrEmpty(format))
            {
                intCount = intCount.Where(p => p.FR_CD == format).ToList();


            }
            ResCount = intCount.Count();
            db.Close();
            return ResCount;
        }
    }
}
