-- 1 Получите список всех табличных пространств (перманентных, временных и UNDO).
-- 2 Получите список всех файлов табличных пространств (перманентных, временных и UNDO).
SELECT tablespace_name, contents FROM dba_tablespaces;

SELECT tablespace_name, file_name FROM dba_data_files;

SELECT tablespace_name, file_name FROM dba_temp_files;

SELECT tablespace_name FROM dba_tablespaces WHERE contents = 'UNDO';


-- 3 Получите перечень всех групп журналов повтора. 
SELECT * FROM v$logfile;

SELECT group#,status FROM v$log WHERE status = 'CURRENT';

--4 Получите перечень файлов всех журналов повтора.
SELECT group#, member FROM v$logfile;

--5 С помощью переключения журналов повтора пройдите полный цикл переключений. 
SELECT group#, sequence#, status, bytes, first_change# FROM v$log;

ALTER SYSTEM SWITCH LOGFILE;

--6 Создайте дополнительную группу журналов повтора с тремя файлами журнала. 
--7 Удалите созданную группу журналов повтора.
ALTER DATABASE ADD LOGFILE GROUP 5 ('D:\Oracle\redo05.log', 'D:\Oracle\redo051.log', 'D:\Oracle\redo052.log') SIZE 20m BLOCKSIZE 512;

SELECT * FROM V$LOGFILE;

ALTER DATABASE DROP LOGFILE MEMBER 'D:\Oracle\redo052.log';
ALTER DATABASE DROP LOGFILE MEMBER 'D:\Oracle\redo051.log';

ALTER SYSTEM CHECKPOINT;

ALTER DATABASE DROP LOGFILE GROUP 5;

--8 Определите, выполняется или нет архивирование журналов повтора
--9 Определите номер последнего архива.
SELECT name, log_mode FROM v$database;

SELECT MAX(sequence#) AS last_archive_log FROM v$log_history;

--10 Включите архивирование.
--11 Принудительно создайте архивный файл. Определите его номер. Определите его местоположение и убедитесь в его наличии. 
--12 Отключите архивирование. 
ALTER DATABASE ARCHIVELOG;

SELECT open_mode FROM v$database;

--ALTER SYSTEM SWITCH LOGFILE;

--ALTER SYSTEM ARCHIVE LOG CURRENT;

SELECT INSTANCE_NAME, ARCHIVER, ACTIVE_STATE FROM V$INSTANCE;
SELECT * FROM V$LOG;
SELECT * FROM V$ARCHIVED_LOG;



--13 Получите список управляющих файлов.
--14 Получите содержимое управляющего файла. 
SELECT name FROM v$controlfile;
SELECT TYPE, RECORD_SIZE, RECORDS_TOTAL FROM V$CONTROLFILE_RECORD_SECTION;







