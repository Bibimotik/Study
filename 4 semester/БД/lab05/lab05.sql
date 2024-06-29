ALTER SESSION SET "_oracle_script" = TRUE;
-- 1. Получите список всех табличных пространств.
select * from SYS.DBA_TABLESPACES;
-- 2. Создайте табличное пространство с именем XXX_QDATA (10 m).
-- При создании установите его в состояние offline.
-- Затем переведите табличное пространство в состояние online.
-- Выделите пользователю XXX квоту 2 m в пространстве XXX_QDATA.
-- От имени XXX в пространстве XXX_QDATA создайте таблицу XXX_T1 из двух столбцов,
-- один из которых будет являться первичным ключом. В таблицу добавьте 3 строки.
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

-- 3. Получите список сегментов табличного пространства XXX_QDATA.
-- 4. Определите сегмент таблицы XXX_T1.
-- 5. Определите остальные сегменты.
-- 7. Получите список сегментов табличного пространства XXX_QDATA.
SELECT * FROM DBA_SEGMENTS WHERE SEGMENT_NAME = 'PNA_T1';

SELECT * FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';

SELECT SEGMENT_NAME, EXTENTS, BLOCKS, BYTES FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';

SELECT * FROM DBA_SEGMENTS;


--table




-- 10. Определите сколько в сегменте таблицы XXX_T1 экстентов, их размер в блоках и байтах.
SELECT * FROM DBA_SEGMENTS WHERE SEGMENT_NAME = 'PNA_T1';
SELECT SEGMENT_NAME, EXTENTS, BLOCKS, BYTES FROM DBA_SEGMENTS WHERE TABLESPACE_NAME = 'PNA_QDATA';
-- 11. Получите перечень всех экстентов в базе данных.
SELECT owner, segment_name, segment_type, extent_id, file_id, block_id, blocks FROM dba_extents;