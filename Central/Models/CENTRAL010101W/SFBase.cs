using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.CENTRAL010101W
{
    public class SFBase
    {
        public int COUNTING_PARTID { get; set; }
        public string IDNO { get; set; }
        public string ASD_VINNO { get; set; }
        public string BDNO { get; set; }
        public string FR_CD { get; set; }
        public string FR_SEQ { get; set; }
        public string REFNO { get; set; }
        public string VIN_TYPE { get; set; }
        public string VIN_WMI { get; set; }
        public string VIN_VDS { get; set; }
        public string VIN_KATA { get; set; }
        public string VIN_FR68 { get; set; }
        public string VIN_MY { get; set; }
        public string MAKER { get; set; }
        public string SNO { get; set; }
        public string ODR_TYPE { get; set; }
        public string DFSC { get; set; }
        public string LO_DATE { get; set; }
        public string CAR_DESC { get; set; }
        public string DEST_TYPE { get; set; }
        public string INT_CD { get; set; }
        public string EXT_CD { get; set; }
        public string DEST_CD { get; set; }
        public string DEST { get; set; }
        public string PSC { get; set; }
        public string PLANT_CD { get; set; }
        public string PRDCTN_SFX { get; set; }
        public string SALES_SFX { get; set; }
        public string IMPORT_DUTY { get; set; }
        public string KATA_CD { get; set; }
        public string KATASHIKI { get; set; }
        public string CTLKATA { get; set; }
        public string LO_KATACODE { get; set; }
        public string LO_KATA { get; set; }
        public string ENG_B_KATA { get; set; }
        public string MOT_B_KATA { get; set; }
        public string MOT_B_KATARR { get; set; }
        public string RECEIPT_TYPE { get; set; }
        public string KD_LOT { get; set; }
        public string ASD_FRMSTAMP { get; set; }
        public string CAR_FAMILY { get; set; }
        public string PRDREQ_MONTH { get; set; }
        public string BRAND { get; set; }
        public string KATASHIKI2 { get; set; }
        public string DUM_V { get; set; }
        public string SPEC200 { get; set; }
        public string MAXWEIGHT { get; set; }
        public string MAXCONBI { get; set; }
        public string FR_AXLELOAD { get; set; }
        public string RR_AXLELOAD { get; set; }
        public string FR_TIRESIZE { get; set; }
        public string RR_TIRESIZE { get; set; }
        public string FR_RIMSIZE { get; set; }
        public string RR_RIMSIZE { get; set; }
        public string FR_TIREPRSR { get; set; }
        public string RR_TIREPRSR { get; set; }
        public string TRANS { get; set; }
        public string AXLE { get; set; }
        public string ENGINE_KATA { get; set; }
        public string ENGINE_DISP { get; set; }
        public string CATEGORY { get; set; }
        public string PRINTNO { get; set; }
        public string MAXWEIGHT2 { get; set; }
        public string MAXCONBI2 { get; set; }
        public string FR_AXLELOAD2 { get; set; }
        public string RR_AXLELOAD2 { get; set; }
        public string FR_TIREPRSR2 { get; set; }
        public string RR_TIREPRSR2 { get; set; }
        public string PLATE_KATA { get; set; }
        public string EMITN_CD { get; set; }
        public string PROC_DT_A0 { get; set; }
        public string PROC_TM_A0 { get; set; }
        public string PROC_DT_Q0 { get; set; }
        public string PROG_TM_Q0 { get; set; }
        public string STS { get; set; }
        public TerminalFormat validFormat { get; set; }
    }
}