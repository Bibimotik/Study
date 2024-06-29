CREATE TABLE PNA_T1(
    ID NUMBER NOT NULL,
    STROK VARCHAR (30),
    PRIMARY KEY (ID)
);
INSERT INTO PNA_T1 (ID, STROK) VALUES (1, '������ 1');
INSERT INTO PNA_T1 (ID, STROK) VALUES (2, '������ 2');
INSERT INTO PNA_T1 (ID, STROK) VALUES (3, '������ 3');

-- 6. ������� (DROP) ������� XXX_T1.
DROP TABLE PNA_T1;
-- ��������� SELECT-������ � ������������� USER_RECYCLEBIN, �������� ���������.
SELECT * FROM USER_RECYCLEBIN;
-- 8. ������������ (FLASHBACK) ��������� �������.
FLASHBACK TABLE PNA_T1 TO BEFORE DROP;
-- 9. ��������� PL/SQL-������, ����������� ������� XXX_T1 ������� (10000 �����).
begin
    FOR loopIndex IN 0..9999
        LOOP
            INSERT INTO PNA_T1 VALUES (loopIndex + 20, 'STROK');
        END LOOP;
    COMMIT;
end;

SELECT COUNT(*) FROM PNA_T1;
-- 12. ���������� �������� ������������� RowId � ������� XXX_T1 � ������ ��������. �������� ������ � ������������� RowId.
-- 13. ���������� �������� ������������� RowSCN � ������� XXX_T1 � ������ ��������.
SELECT ID, ROWID, ORA_ROWSCN FROM PNA_T1
ORDER BY ID FETCH FIRST 10 ROWS ONLY;