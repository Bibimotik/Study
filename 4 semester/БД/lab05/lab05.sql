ALTER SESSION SET "_oracle_script" = TRUE;
-- 1. �������� ������ ���� ��������� �����������.
select * from SYS.DBA_TABLESPACES;
-- 2. �������� ��������� ������������ � ������ XXX_QDATA (10 m).
-- ��� �������� ���������� ��� � ��������� offline.
-- ����� ���������� ��������� ������������ � ��������� online.
-- �������� ������������ XXX ����� 2 m � ������������ XXX_QDATA.
-- �� ����� XXX � ������������ XXX_QDATA �������� ������� XXX_T1 �� ���� ��������,
-- ���� �� ������� ����� �������� ��������� ������. � ������� �������� 3 ������.
create tablespace PNA_QDATA DATAFILE
    'PNA_QDATA.dbf'
    SIZE 10m
    offline;
alter tablespace PNA_QDATA online ;
SELECT * FROM DBA_DATA_FILES;

CREATE USER PNA_QDATA
  IDENTIFIED BY NewPassword
  DEFAULT TABLESPACE PNA_QDATA
  ACCOUNT UNLOCK;

GRANT CREATE SESSION,
    CREATE TABLE
    TO PNA_QDATA;

ALTER USER PNA_QDATA QUOTA 5M ON PNA_QDATA;

-- 3. �������� ������ ��������� ���������� ������������ XXX_QDATA.
-- 4. ���������� ������� ������� XXX_T1.
-- 5. ���������� ��������� ��������.
-- 7. �������� ������ ��������� ���������� ������������ XXX_QDATA.
SELECT * FROM DBA_SEGMENTS WHERE SEGMENT_NAME = 'PNA_T1';

SELECT * FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';

SELECT SEGMENT_NAME, EXTENTS, BLOCKS, BYTES FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';

SELECT * FROM DBA_SEGMENTS;


--table




-- 10. ���������� ������� � �������� ������� XXX_T1 ���������, �� ������ � ������ � ������.
SELECT * FROM DBA_SEGMENTS WHERE SEGMENT_NAME = 'PNA_T1';
SELECT SEGMENT_NAME, EXTENTS, BLOCKS, BYTES FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';
-- 11. �������� �������� ���� ��������� � ���� ������.
SELECT owner, segment_name, segment_type, extent_id, file_id, block_id, blocks FROM dba_extents;