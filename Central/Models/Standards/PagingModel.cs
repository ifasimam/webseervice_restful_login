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
    public class PagingModel
    {
        public int CountData { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Length { get; set; }
        public List<int> IndexList { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public int NextPage { get; set; }
        public int PrevPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int MaxPageShow { get; set; }
        public string ControllerName { get; set; }

        /* Method to set default pages size item */
        public List<Int32> DefaultPageSizeItem()
        {
            return new List<Int32>() { 1, 10, 25, 50, 100 };
        }
    }
}