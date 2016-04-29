/************************************************************************************************************
 * Program History :                                                                                        *
 *                                                                                                          *
 * Project Name     : Corporate Information System                                                          *
 * Client Name      : PT. TMMIN (Toyota Motor Manufacturing Indonesia)                                      *
 * Function Id      : Standards                                                                             *
 * Function Name    : Standards                                                                             *
 * Function Group   : Standards                                                                             *
 * Program Id       : StandardsController                                                                   *
 * Program Name     : Standards Controller                                                                  *
 * Program Type     : Controller                                                                            *
 * Description      :                                                                                       *
 * Environment      : .NET 4.0, ASP MVC 4.0                                                                 *
 * Author           : FID.Arri                                                                              *
 * Version          : 01.00.00                                                                              *
 * Creation Date    : 19/02/2016 15:01:40                                                                   *
 *                                                                                                          *
 * Update history		Re-fix date				Person in charge				Description					*
 *                                                                                                          *
 * Copyright(C) 2016 - Fujitsu Indonesia. All Rights Reserved                                               *
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using Central.Models.Standards;

namespace Central.Controllers.Standards
{
    public class StandardsController : PageController
    {
        #region Singleton
        private static StandardsController instance = null;
        public static StandardsController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StandardsController();
                }
                return instance;
            }
        }
        #endregion

        #region Paging Function
        public PagingModel CountIndex(int currentpage, int count, int length, int Total, string ControllerName)
        {
            PagingModel pg = new PagingModel();
            List<int> index = new List<int>();
            pg.Length = count;
            pg.CountData = count;
            pg.CurrentPage = currentpage;
            pg.CurrentPageSize = length;

            Double total = (Double)Total;
            pg.Length = (int)total;
            int startPage = 1;
            int CurrentLastPage = (int)total;
            int MaxPageShow = 5;
            if ((int)total > MaxPageShow)
            {
                int midlePageNumber = ((int)Math.Ceiling(MaxPageShow / 2.0)) - 1;
                int startPageView = currentpage - midlePageNumber;
                int lastPageView = currentpage + midlePageNumber;
                if (startPageView < midlePageNumber)
                {
                    lastPageView = MaxPageShow;
                    startPageView = 1;
                }
                else if (lastPageView > ((int)total - midlePageNumber))
                {
                    lastPageView = (int)total;
                    startPageView = (int)total - MaxPageShow;
                }
                startPage = startPageView;
                CurrentLastPage = lastPageView;
                for (int i = startPage; i < CurrentLastPage + 1; i++)
                {
                    index.Add(i);
                }
            }
            else
            {
                for (int i = startPage; i < CurrentLastPage + 1; i++)
                {
                    index.Add(i);
                }
            }
            pg.IndexList = index;
            if (currentpage > (int)total)
            {
                pg.Start = 1;
            }
            else
            {
                pg.Start = ((currentpage - 1) * length + 1);
            }

            if (count - (currentpage - 1) * length < length)
            {
                pg.End = (currentpage - 1) * length + (count - (currentpage - 1) * length);
            }
            else
            {
                pg.End = currentpage * length;
            }
            pg.FirstPage = 1;
            if ((int)total == 1)
            {
                pg.PrevPage = (int)total;
                pg.NextPage = (int)total;
            }
            else
            {
                if (currentpage <= 1)
                {
                    pg.PrevPage = 1;
                    pg.NextPage = currentpage + 1;
                }
                else if (currentpage >= (int)total)
                {
                    pg.NextPage = (int)total;
                    pg.PrevPage = currentpage - 1;
                }
                else
                {
                    pg.NextPage = currentpage + 1;
                    pg.PrevPage = currentpage - 1;
                }
            }
            pg.LastPage = (int)total;
            pg.ControllerName = ControllerName;
            return pg;
        }
        #endregion

        #region Notification
        //public ActionResult GetNotificationHeader()
        //{
        //    Toyota.Common.Credential.User user = Lookup.Get<Toyota.Common.Credential.User>();
        //    //List<HeaderNotification> h = new List<HeaderNotification>();
        //    IEnumerable<HeaderNotification> h = StandardsRepository.Instance.GetHeaderNotification(user.RegistrationNumber);
        //    if (user != null)
        //    {
        //        //h = StandardsRepository.Instance.GetHeaderNotification(user.RegistrationNumber);
        //         h = StandardsRepository.Instance.GetHeaderNotification(user.RegistrationNumber);
        //    }
        //    if (h.Count() != 0)
        //    {

        //        return Json(h, JsonRequestBehavior.AllowGet);
        //    }
        //    else 
        //    {
        //        string data = "empty";
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //    //return Json(h);
        //}
        [HttpPost]
        public string HideNotification(string data)
        {
            string result = "";
            Toyota.Common.Credential.User user = Lookup.Get<Toyota.Common.Credential.User>();
            result = StandardsRepository.Instance.doUpdateNotif(data,user.RegistrationNumber);

            return result;
        }
        #endregion

    }
}
