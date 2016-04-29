using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace Central.Models.CENTRAL020105W
{
    public class CENTRAL020105WRepository  
    {
        #region Singleton
        private static CENTRAL020105WRepository instance = null;
        public static CENTRAL020105WRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CENTRAL020105WRepository();
                }
                return instance;
            }
        }

        #endregion
        public IEnumerable<CENTRAL020105W> GetComboPlant()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020105W>("CENTRAL020105W/CENTRAL020105WGetComboPlant");
            List<CENTRAL020105W> dbTemp = new List<CENTRAL020105W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PLANT_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }
        public IEnumerable<CENTRAL020105W> GetComboTerminal()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020105W>("CENTRAL020105W/CENTRAL020105WComboTerminal");
            List<CENTRAL020105W> dbTemp = new List<CENTRAL020105W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.TERM_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }

        public IEnumerable<CENTRAL020105W> GetComboPartCode()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020105W>("CENTRAL020105W/CENTRAL020105WComboPartCode");
            List<CENTRAL020105W> dbTemp = new List<CENTRAL020105W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.PART_CD).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }


        public IEnumerable<CENTRAL020105W> GetComboConstrainNm1()
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<CENTRAL020105W>("CENTRAL020105W/CENTRAL020105WComboConstrainNm");
            List<CENTRAL020105W> dbTemp = new List<CENTRAL020105W>();
            dbTemp = result.ToList();
            dbTemp = dbTemp.GroupBy(p => p.CONS_DESC).Select(p => p.First()).ToList();
            db.Close();
            return dbTemp;
        }

        public IEnumerable<SIMPLEGRID> GetSimpleGrid(string plant, string terminal, string partcode, string partdesc, int p_page, int p_length)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL020105W/CENTRAL020105WGetSimpleGrid");
            List<SIMPLEGRID> NewList = new List<SIMPLEGRID>();
            NewList = result.ToList();
            if (plant != null || !String.IsNullOrEmpty(plant))
            {
                NewList = NewList.Where(p => p.PLANT_CD == plant).ToList();

            }
            if (terminal != null || !String.IsNullOrEmpty(terminal))
            {
                NewList = NewList.Where(p => p.TERM_CD == terminal).ToList();

            }
            if (partcode != null || !String.IsNullOrEmpty(partcode))
            {
                NewList = NewList.Where(p => p.PART_CD == partcode).ToList();

            }
            if (partdesc != null || !String.IsNullOrEmpty(partdesc))
            {
                NewList = NewList.Where(p => p.PART_DESC == partdesc).ToList();

            }

            if (p_length != 0 || p_page != 0)
            {
                NewList = NewList.Where(p => p.ROW_NUM >= p_page && p.ROW_NUM <= p_length).ToList();
            }
            db.Close();
            return NewList;
        }
 
          public string SaveEditGridRepo (string ID, string PlanCD_val, string termCD_val, string partCD_val, string PartDesc_val, string ColV1_val, string ConsVal1_val, string ColVI2_val, string ConsVal2_val, string ColVI3_val, string ConsVal3_val, string format_No_val, string format_val_val, string format_start_val, string format_length_val, string valid_fr_val, string valid_to_val, string changeBy_val, string changeDt_val, string UpdateBy_val, string UpdateDt_val){
            IDBContext db = DatabaseManager.Instance.GetContext();
            if (!String.IsNullOrEmpty(valid_fr_val) || !String.IsNullOrEmpty(valid_to_val) || !String.IsNullOrEmpty(changeDt_val) || !String.IsNullOrEmpty(UpdateDt_val))
            {
                DateTime val_from = DateTime.ParseExact(valid_fr_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                valid_fr_val = val_from.ToString("yyyy-MM-dd");
                DateTime val_to = DateTime.ParseExact(valid_to_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                valid_to_val = val_to.ToString("yyyy-MM-dd");
                DateTime cr_DT = DateTime.ParseExact(changeDt_val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                changeDt_val = cr_DT.ToString("yyyy-MM-dd");

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
                FORMAT_START = format_start_val,
                FORMAT_LENGTH = format_length_val,
                VALID_FR = valid_fr_val,
                VALID_TO = valid_to_val,
                CREATED_BY = changeBy_val,
                CREATED_DT = changeDt_val,
                UPDATED_BY = UpdateBy_val,
                UPDATED_DT = UpdateDt_val
            };

            try
            {
                int result = db.Execute("CENTRAL020105W/CENTRAL020105SaveEditGrid", args);
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
        public int CountData(string plant, string terminal, string partcode, string partdesc)
        {
            int ResCount = 0;
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Fetch<SIMPLEGRID>("CENTRAL020105W/CENTRAL020105WGetCountDataCentral");

            List<SIMPLEGRID> intCount = new List<SIMPLEGRID>();
            intCount = result.ToList();

            if (plant != null || !String.IsNullOrEmpty(plant))
            {
                intCount = intCount.Where(p => p.PLANT_CD == plant).ToList();


            }
            if (terminal != null || !String.IsNullOrEmpty(terminal))
            {

                intCount = intCount.Where(p => p.TERM_CD == terminal).ToList();


            }

            if (partcode != null || !String.IsNullOrEmpty(partcode))
            {
                intCount = intCount.Where(p => p.PART_CD == partcode).ToList();


            }
            if (partdesc != null || !String.IsNullOrEmpty(partdesc))
            {
                intCount = intCount.Where(p => p.PART_DESC == partdesc).ToList();


            }
            ResCount = intCount.Count();
            db.Close();
            return ResCount;
        }
        public int GetDeleteGrid(string PlantID)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var result = db.Execute("CENTRAL020105W/CENTRAL020105WGetDeleteGrid", new { PLANT_CD = PlantID });
            db.Close();
            return result;
        }
    }
}
