using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.CENTRALMessage
{
    public class CENTRALLogMonitoring
    {
        public string seqID { get; set; }
        public string processID { get; set; }
        public string userID { get; set; }
        public string subsystemID { get; set; }
        public string remark { get; set; }
        public string functionID { get; set; }
        public string processName { get; set; }
        public string type
        {
            get
            {
                if (message.MSG_TYPE.Equals("E"))
                {
                    return "ERR";
                }
                else if (message.MSG_TYPE.Equals("I"))
                {
                    return "INF";
                }
                else
                {
                    return "WAR";
                }
            }            
        }
        public CENTRALMessageDomain message { get; set; }
    }
}