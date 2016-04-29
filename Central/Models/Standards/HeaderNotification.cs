/************************************************************************************************************
 * Program History :                                                                                        *
 *                                                                                                          *
 * Project Name     : Corporate Information System                                                 *
 * Client Name      : PT. TMMIN (Toyota Motor Manufacturing Indonesia)                                      *
 * Function Id      : Paging                                                                                *
 * Function Name    : Paging                                                                                *
 * Function Group   : Paging                                                                                *
 * Program Id       : PagingModel                                                                           *
 * Program Name     : Paging Model                                                                          *
 * Program Type     : Model                                                                                 *
 * Description      :                                                                                       *
 * Environment      : .NET 4.0, ASP MVC 4.0                                                                 *
 * Author           : FID.Witan                                                                              *
 * Version          : 01.00.00                                                                              *
 * Creation Date    : 19/02/2016 14:38:40                                                                   *
 *                                                                                                          *
 * Update history		Re-fix date				Person in charge				Description					*
 *                                                                                                          *
 * Copyright(C) 2016 - Fujitsu Indonesia. All Rights Reserved                                               *
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.Standards
{
    public class HeaderNotification 
    {
        public String TITLE { get; set; }
        public int COUNTER { get; set; }
        public String URL { get; set; }
        public int NOTIF_ID { get; set; }
        public String CREATED_DT { get; set; }
    }
}
