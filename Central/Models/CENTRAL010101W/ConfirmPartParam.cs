using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.CENTRAL010101W
{
    public class ConfirmPartParam
    {
        public string bodyNo { get; set; }
        public string termCd { get; set; }
        public int originalAmountPart { get; set; }
        public string userID { get; set; }
        public int roleId { get; set; }
        public List<string> partIDs { get; set; }

        public ConfirmPartParam(string bodyNo, string termCd, int originalAmountPart, string userID, int roleId, List<string> partIDs)
        {
            this.bodyNo = bodyNo;
            this.termCd = termCd;
            this.originalAmountPart = originalAmountPart;
            this.userID = userID;
            this.roleId = roleId;
            this.partIDs = partIDs;
        }
    }
}