SELECT BRIGADE_CODE, rd.ID as DEVICE_ID, PDT as USAGE_DATE 
FROM LISTCODE JOIN REF_DEVICE rd ON LiSTCODE.DEVICE_SERIAL_NUMBER = rd.SERIAL_NUMBER
WHERE PDT BETWEEN TO_DATE('2020-06-01', 'YYYY-MM-DD') AND TO_DATE('2020-06-30', 'YYYY-MM-DD')
MINUS 
SELECT BRIGADE_CODE, rd.ID as DEVICE_ID, USAGE_DATE
FROM DEVICE_USAGE du JOIN REF_DEVICE rd ON du.REF_DEVICE_ID = rd.ID
WHERE USAGE_DATE BETWEEN TO_DATE('2020-06-01', 'YYYY-MM-DD') AND TO_DATE('2020-06-30', 'YYYY-MM-DD');