using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Models.CENTRAL020105W
{
    public class CENTRAL020105W
    {
        public string PLANT_CD { get; set; }
        public string TERM_CD { get; set; }
        public string PART_CD { get; set; }
        public string CONS_DESC { get; set; }
    }
    public class SIMPLEGRID
    {
        public string PLANT_CD { get; set; }

        public string TERM_CD { get; set; }
        public string PART_CD { get; set; }
        public string COL_VI1 { get; set; }
        public string CONS_VAL1 { get; set; }
        public string COL_VI2 { get; set; }
        public string CONS_VAL2 { get; set; }
        public string PART_DESC { get; set; }
        public string COL_VI3 { get; set; }
        public string CONS_VAL3 { get; set; }
        public int FORMAT_SEQ { get; set; }
        public string FORMAT_VAL { get; set; }
        public int FORMAT_START { get; set; }
        public int FORMAT_LENGTH { get; set; }
        public DateTime VALID_FR { get; set; }

        public DateTime VALID_TO { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DT { get; set; }
        public int ROW_NUM { get; set; }
    }
}
