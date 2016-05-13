	  select a.MSG_NO, a.VAL , b.FORMAT_VAL FROM TB_R_SFN_CENTRAL a inner join TB_M_TERMINAL_FORMATTING b
	  on a.MSG_NO =b.MSG_NO where a.BDNO = @BDNO