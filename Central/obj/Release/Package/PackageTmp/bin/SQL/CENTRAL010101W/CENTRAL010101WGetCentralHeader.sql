SELECT 
BDNO,
STS
FROM TB_R_PARTID_H
WHERE ((BDNO = @BodyNo) or (@BodyNo is null))
and ((IDNO = @IdNo) or (@IdNo is null))