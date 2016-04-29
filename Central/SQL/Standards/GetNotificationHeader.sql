/************************************************************************************************
 * Program History : 
 * 
 * Project Name     : CORPORATE INFORMATION SYSTEM
 * Client Name      : PT. TMMIN (Toyota Manufacturing Motor Indonesia)
 * Function Id      : Get Notification Header
 * Function Name    : Notification
 * Function Group   : Notification
 * Program Id       : GetNotificationHeader
 * Program Name     : GetNotificationHeader
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

--EXEC [dbo].[sp_hr_notif] @NOREG

SELECT SYS_VAL as TITLE,
	NOTIF_ID,
	SYS_REMARK as URL,
	CONVERT(varchar(100),B.CREATED_DT,103) as CREATED_DT
	FROM TB_M_SYSTEM A JOIN TB_R_NOTIFICATION B ON A.SYS_CD=B.SYS_CD_NOTIF 
	WHERE a.SYS_SUB_CAT='NOTIFICATION' AND B.NOREG=@NOREG AND B.STATUS_NOTIF = '1' ORDER BY B.CREATED_DT DESC

--SELECT  TITLE,
--		URL,
--		COUNT(TITLE) as COUNTER,
--		SYS_CD_NOTIF as NOTIF_ID
--		FROM (SELECT SYS_VAL as TITLE,
--					 SYS_REMARK as URL,
--					 SYS_CD_NOTIF
--					 FROM TB_M_SYSTEM A JOIN TB_R_NOTIFICATION B ON A.SYS_CD=B.SYS_CD_NOTIF 
--					 WHERE a.SYS_SUB_CAT='NOTIFICATION' AND B.NOREG=@NOREG AND B.STATUS_NOTIF = '1') A
--	GROUP BY TITLE,URL,SYS_CD_NOTIF