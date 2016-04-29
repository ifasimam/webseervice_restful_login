﻿   DECLARE @@MsqNo varchar (50);
  set @@MsqNo=( select distinct (MSG_NO) FROM TB_M_TERMINAL_FORMATTING WHERE PLANT_CD =@PLANT_CD);
 insert into TB_M_TERMINAL_FORMATTING (PLANT_CD,TERM_CD,MSG_NO,PART_CD,COL_VI1,CONS_VAL1,COL_VI2,CONS_VAL2,COL_VI3,CONS_VAL3,FORMAT_SEQ,FORMAT_VAL,FORMAT_START,FORMAT_LENGTH,
 CONS_START1,CONS_START2,CONS_START3,CONS_LENGTH1,CONS_LENGTH2,CONS_LENGTH3,VALID_FR,VALID_TO,CREATED_BY,CREATED_DT,UPDATED_BY,UPDATED_DT)
 values
 (
  @PLANT_CD,
  @TERM_CD,
  @@MsqNo,
  @PART_CD,
 @COL_VI1, 
@CONS_VAL1,
@COL_VI2,
 @CONS_VAL2,
 @COL_VI3,
 @CONS_VAL3,
 @FORMAT_SEQ,
 @FORMAT_VAL,
 @FORMAT_START,
 @FORMAT_LENGTH,
 @CONS_START1,
  @CONS_START2,
   @CONS_START3,
   @CONS_LENGTH1,
      @CONS_LENGTH2,
	     @CONS_LENGTH3,
 @VALID_FR,
 @VALID_TO,
 @CREATED_BY,
 @CREATED_DT,
 @UPDATED_BY,
 @UPDATED_DT
 
 )
 

 