using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRAL020106W
{
    public class CENTRAL020106WRepository  
    {
        #region Singleton
        private static CENTRAL020106WRepository instance = null;
        public static CENTRAL020106WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL020106WRepository();
                }
                return instance;
            }
        }
        
        #endregion
        public IEnumerable<SIMPLEGRID> GetTerminalEdit(string ID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL020106W/CENTRAL020106WTerminalFromatting", new { PLANT_CD=ID});
            List<SIMPLEGRID> dbTemp = new List<SIMPLEGRID>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PLANT_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<CENTRAL020106W> GetComboPlant()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020106W>("CENTRAL020106W/CENTRAL020106WGetComboPlant");
            List<CENTRAL020106W> dbTemp = new List<CENTRAL020106W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PLANT_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }
  
        public IEnumerable<CENTRAL020106W> GetComboTerminal(string PlanCD_val)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020106W>("CENTRAL020106W/CENTRAL020106WComboTerminal", new { PLANT_CD=PlanCD_val});
            List<CENTRAL020106W> dbTemp = new List<CENTRAL020106W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.TM_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }

        public IEnumerable<CENTRAL020106W> GetComboPartCode(string PlanCD_val)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020106W>("CENTRAL020106W/CENTRAL020106WComboPartCode", new { PLANT_CD=PlanCD_val});
            List<CENTRAL020106W> dbTemp = new List<CENTRAL020106W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PART_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }
         public IEnumerable<CENTRAL020106W> GetComboConstrainNm1()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020106W>("CENTRAL020106W/CENTRAL020106WComboConstrainNm");
            List<CENTRAL020106W> dbTemp = new List<CENTRAL020106W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.CONS_DESC).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }
          
         public string SaveEditGridRepo(string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val1, string format_start_val2, string format_start_val3, string LenCons1, string LenCons2, string LenCons3, string starter, string Len, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val)
         {
             IDBContext db = DatabaseManager.Instance.GetContext();
             if (!String.IsNullOrEmpty(valid_fr_val) || !String.IsNullOrEmpty(valid_to_val) || !String.IsNullOrEmpty(changeDt_val) || !String.IsNullOrEmpty(UpdateDt_val))
             {
                 DateTime val_from = DateTime.ParseExact(valid_fr_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 valid_fr_val = val_from.ToString("yyyy-MM-dd");
                 DateTime val_to = DateTime.ParseExact(valid_to_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 valid_to_val = val_to.ToString("yyyy-MM-dd");
                 changeDt_val = DateTime.Today.ToString("dd/MM/yyyy");
                 DateTime cr_DT = DateTime.ParseExact(changeDt_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 changeDt_val = cr_DT.ToString("yyyy-MM-dd");
                 UpdateDt_val = DateTime.Today.ToString("dd/MM/yyyy");
                 DateTime Up_DT = DateTime.ParseExact(UpdateDt_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 UpdateDt_val = Up_DT.ToString("yyyy-MM-dd");
             }

             dynamic args = new
             {
                 PLANT_CDID = ID,
                 PLANT_CD = PlanCD_val,
                 TERM_CD = termCD_val,
                 PART_CD = partCD_val,
                 PART_DESC = PartDesc_val,
                 COL_VI1 = ColV1_val,
                 CONS_VAL1 = ConsVal1_val,
                 COL_VI2 = ColVI2_val,
                 CONS_VAL2 = ConsVal2_val,
                 COL_VI3 = ColVI3_val,
                 CONS_VAL3 = ConsVal3_val,
                 FORMAT_SEQ = format_No_val,
                 FORMAT_VAL = format_val_val,
                 FORMAT_START = starter,
                 FORMAT_LENGTH = Len,
                 CONS_START1=format_start_val1,
                 CONS_START2 = format_start_val2,
                 CONS_START3 = format_start_val3,
                 CONS_LENGTH1=LenCons1,
                 CONS_LENGTH2 = LenCons2,
                 CONS_LENGTH3 = LenCons3,
                 VALID_FR = valid_fr_val,
                 VALID_TO = valid_to_val,
                 CREATED_BY = "Hart",
                 CREATED_DT = changeDt_val,
                 UPDATED_BY = "Hart",
                 UPDATED_DT = UpdateDt_val
             };

             try
             {
                 int result = db.Execute("CENTRAL020106W/CENTRAL020106WSaveEditGrid", args);
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

         public string SaveAddGridRepo(string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val1, string format_start_val2, string format_start_val3, string LenCons1, string LenCons2, string LenCons3, string starter, string Len, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val)
         {
             IDBContext db = DatabaseManager.Instance.GetContext();
             if (!String.IsNullOrEmpty(valid_fr_val) || !String.IsNullOrEmpty(valid_to_val) || !String.IsNullOrEmpty(changeDt_val) || !String.IsNullOrEmpty(UpdateDt_val))
             {
                 DateTime val_from = DateTime.ParseExact(valid_fr_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 valid_fr_val = val_from.ToString("yyyy-MM-dd");
                 DateTime val_to = DateTime.ParseExact(valid_to_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 valid_to_val = val_to.ToString("yyyy-MM-dd");
                 changeDt_val = DateTime.Today.ToString("dd/MM/yyyy");
                 DateTime cr_DT = DateTime.ParseExact(changeDt_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 changeDt_val = cr_DT.ToString("yyyy-MM-dd");
                 UpdateDt_val = DateTime.Today.ToString("dd/MM/yyyy");
                 DateTime Up_DT = DateTime.ParseExact(UpdateDt_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                 UpdateDt_val = Up_DT.ToString("yyyy-MM-dd");
             }

             dynamic args = new
             {
                 PLANT_CDID = ID,
                 PLANT_CD = PlanCD_val,
                 TERM_CD = termCD_val,
                 PART_CD = partCD_val,
                 PART_DESC = PartDesc_val,
                 COL_VI1 = ColV1_val,
                 CONS_VAL1 = ConsVal1_val,
                 COL_VI2 = ColVI2_val,
                 CONS_VAL2 = ConsVal2_val,
                 COL_VI3 = ColVI3_val,
                 CONS_VAL3 = ConsVal3_val,
                 FORMAT_SEQ = format_No_val,
                 FORMAT_VAL = format_val_val,
                 FORMAT_START = starter,
                 FORMAT_LENGTH = Len,
                 CONS_START1 = format_start_val1,
                 CONS_START2 = format_start_val2,
                 CONS_START3 = format_start_val3,
                 CONS_LENGTH1 = LenCons1,
                 CONS_LENGTH2 = LenCons2,
                 CONS_LENGTH3 = LenCons3,
                 VALID_FR = valid_fr_val,
                 VALID_TO = valid_to_val,
                 CREATED_BY = "Hart",
                 CREATED_DT = changeDt_val,
                 UPDATED_BY = "Hart",
                 UPDATED_DT = UpdateDt_val
             };

             try
             {
                 int result = db.Execute("CENTRAL020106W/CENTRAL020106WSaveAddGrid", args);
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

        public IEnumerable<SIMPLEGRID> GetSimpleGrid(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to, int Page, int length)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetSimpleGrid");
            List<SIMPLEGRID> NewList = new List<SIMPLEGRID>();
            NewList = result.ToList();
            if (katashik != null || !String.IsNullOrEmpty(katashik))
            {
                NewList = NewList.Where(p => p.COL_VI1 == katashik).ToList();

            }
            if (length != 0 || Page != 0)
            {
                NewList = NewList.Where(p => p.ROW_NUM >= Page && p.ROW_NUM <= length).ToList();
            }
            db.Close();
            return NewList;
        }
        public int CountData(string source, string frameNo, string katashik, string suffix, string parttype,
            string partNo, string prodFrom, string status, string format, string prodTo, string start, string len, string from, string to)
        {
            int ResCount = 0;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL030102W/CENTRAL030102WGetCountDataCentral");

            List<SIMPLEGRID> intCount = new List<SIMPLEGRID>();
            intCount = result.ToList();
            ResCount = intCount.Count();
            if (katashik != null)
            {
                ResCount = intCount.Where(p => p.COL_VI1 == katashik).Count();
            }
            db.Close();
            return ResCount;
        }
        public string GetPartDescRepo(string PlanCD_val)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.SingleOrDefault<string>("CENTRAL020106W/CENTRAL020106WGetPartDesc", new {PLANT_CD=PlanCD_val });
            db.Close();
            return result;
        }
    }
}
