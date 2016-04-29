select * from TB_R_SFN_CENTRAL a left join TB_R_PARTID_H b on a.BDNO = b.BDNO left join TB_M_MAPP_PART c on b.TM_CD=c.TM_CD
left join TB_M_PART d on c.PART_CD = d.PART_CD where a.BDNO=@BDNO