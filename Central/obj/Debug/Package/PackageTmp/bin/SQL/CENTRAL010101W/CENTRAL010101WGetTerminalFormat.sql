﻿SELECT
MSG_NO,
PLANT_CD,
TM_CD,
PART_CD,
COL_VI1,
COL_VI2,
COL_VI3,
CONS_VAL1,
CONS_VAL2,
CONS_VAL3,
FORMAT_VAL
FROM TB_M_TERMINAL_FORMATTING
WHERE TM_CD = @TermCd
AND SYSDATETIME() BETWEEN VALID_FR AND VALID_TO
