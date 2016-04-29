using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Models.CENTRAL030102W
{
    public class CENTRAL030102W 
    {
        public string PART_CD { get; set; }
        public int FORMAT_SEQ { get; set; }
        public string SYSTEM_VALUE_TXT { get; set; }
        public string SALES_SFX { get; set; }
    }
    public class SIMPLEGRID
    {
        public string IDNO { get; set; }
        public string PART_CD { get; set; }
        public string ASD_VINNO { get; set; }
        public string KATASHIKI { get; set; }
        public string PRDCTN_SFX { get; set; }
        public string SALES_SFX { get; set; }
        public string EXT_CD { get; set; }
        public string DEST { get; set; }
        public DateTime LO_DATE { get; set; }
        public string STS { get; set; }
        public string VAL { get; set; }
        public string FR_CD { get; set; }
        public string SUMM_STS { get; set; }
        public string SYSTEM_VALUE_TXT { get; set; }
        public int ROW_NUM { get; set; }
    }
}
