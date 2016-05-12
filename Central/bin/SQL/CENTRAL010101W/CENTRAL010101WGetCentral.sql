SELECT 
sc.MSG_NO,
sc.VAL,
sc.IDNO,
sc.BDNO,
sc.JUDGE,
sc.INPUT_DT,
sc.FORMAT_VAL,
sc.FORMAT_START,
sc.FORMAT_LENGHT,
sc.CREATED_BY,
sc.CREATED_DT,
sc.CHANGED_BY,
sc.CHANGED_DT,
h.STS
FROM TB_R_SFN_CENTRAL sc
LEFT JOIN TB_R_PARTID_H h
	on sc.IDNO = h.IDNO
WHERE ((sc.VAL = @Val) or (@Val is null))
and ((sc.BDNO = @BodyNo) or (@BodyNo is null))
and ((sc.IDNO = @IdNo) or (@IdNo is null))