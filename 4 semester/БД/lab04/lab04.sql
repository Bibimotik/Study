-- 1 �������� ������ ���� ��������� ����������� (������������, ��������� � UNDO).
-- 2 �������� ������ ���� ������ ��������� ����������� (������������, ��������� � UNDO).
SELECT tablespace_name, contents FROM dba_tablespaces;

SELECT tablespace_name, file_name FROM dba_data_files;

SELECT tablespace_name, file_name FROM dba_temp_files;

SELECT tablespace_name FROM dba_tablespaces WHERE contents = 'UNDO';


-- 3 �������� �������� ���� ����� �������� �������. 
SELECT * FROM v$logfile;

SELECT group#,status FROM v$log WHERE status = 'CURRENT';

--4 �������� �������� ������ ���� �������� �������.
SELECT group#, member FROM v$logfile;

--5 � ������� ������������ �������� ������� �������� ������ ���� ������������. 
SELECT group#, sequence#, status, bytes, first_change# FROM v$log;

ALTER SYSTEM SWITCH LOGFILE;

--6 �������� �������������� ������ �������� ������� � ����� ������� �������. 
--7 ������� ��������� ������ �������� �������.
ALTER DATABASE ADD LOGFILE GROUP 5 ('D:\Oracle\redo05.log', 'D:\Oracle\redo051.log', 'D:\Oracle\redo052.log') SIZE 20m BLOCKSIZE 512;

SELECT * FROM V$LOGFILE;

ALTER DATABASE DROP LOGFILE MEMBER 'D:\Oracle\redo052.log';
ALTER DATABASE DROP LOGFILE MEMBER 'D:\Oracle\redo051.log';

ALTER SYSTEM CHECKPOINT;

ALTER DATABASE DROP LOGFILE GROUP 5;

--8 ����������, ����������� ��� ��� ������������� �������� �������
--9 ���������� ����� ���������� ������.
SELECT name, log_mode FROM v$database;

SELECT MAX(sequence#) AS last_archive_log FROM v$log_history;

--10 �������� �������������.
--11 ������������� �������� �������� ����. ���������� ��� �����. ���������� ��� �������������� � ��������� � ��� �������. 
--12 ��������� �������������. 
ALTER DATABASE ARCHIVELOG;

SELECT open_mode FROM v$database;

--ALTER SYSTEM SWITCH LOGFILE;

--ALTER SYSTEM ARCHIVE LOG CURRENT;

SELECT INSTANCE_NAME, ARCHIVER, ACTIVE_STATE FROM V$INSTANCE;
SELECT * FROM V$LOG;
SELECT * FROM V$ARCHIVED_LOG;



--13 �������� ������ ����������� ������.
--14 �������� ���������� ������������ �����. 
SELECT name FROM v$controlfile;
SELECT TYPE, RECORD_SIZE, RECORDS_TOTAL FROM V$CONTROLFILE_RECORD_SECTION;







