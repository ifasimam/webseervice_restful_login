SELECT		
sc.VAL,
sc.IDNO,		
sc.INPUT_DT,		
sc.JUDGE,		
sc.FORMAT_VAL,		
sc.FORMAT_START
FROM TB_R_SFN_CENTRAL sc
	inner join TB_R_PARTID_H h
		on sc.BDNO = h.BDNO
		and sc.IDNO = h.IDNO
		and h.STS = 'C'
where sc.BDNO = @BodyNo
and sc.IDNO = @IdNo
