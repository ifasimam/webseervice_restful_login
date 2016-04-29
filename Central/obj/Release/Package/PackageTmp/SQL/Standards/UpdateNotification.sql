/************************************************************************************************
 * Program History : 
 * 
 * Project Name     : CORPORATE INFORMATION SYSTEM
 * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
 * Function Id      : Get Notification Header
 * Function Name    : Notification
 * Function Group   : Notification
 * Program Id       : UpdateNotificationHeader
 * Program Name     : UpdateNotificationHeader
 * Program Type     : SQL
 * Description      : 
 * Environment      : .NET 4.0, ASP MVC 4.0
 * Author           : FID.Witan
 * Version          : 01.00.00
 * Creation Date    : 17/02/2016 14:20:00
 * 
 * Update history     Re-fix date       Person in charge      Description 
 * 
 *
 * Copyright(C) 2016 - . All Rights Reserved                                                                                              
 *************************************************************************************************/

UPDATE TB_R_NOTIFICATION SET [STATUS_NOTIF]='2' WHERE NOTIF_ID = @NOTIF_ID AND NOREG = @NOREG