﻿ declare @@TMCD varchar(50) = (SELECT TM_CD FROM TB_R_PARTID_H WHERE BDNO=@BDNO);
  SELECT * FROM TB_M_TERMINAL_FORMATTING WHERE  TM_CD=@@TMCD 
  --AND  VALID_FR >= GETDATE() and VALID_TO<=GETDATE()