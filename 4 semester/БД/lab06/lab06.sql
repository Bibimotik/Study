--1. Определите местоположение файла параметров инстанса.
SHOW PARAMETER SPFILE;

--3. Сформируйте PFILE с именем XXX_PFILE.ORA.
CREATE PFILE = 'BMO_PFILE.ORA' FROM SPFILE;

--4. Измените какой-либо параметр в файле PFILE.

--5. Создайте новый SPFILE.
CREATE SPFILE='D:\Oracle\dbhomeXE\database\SPFILE.ORA' FROM PFILE='D:\Oracle\dbhomeXE\database\BMO_PFILE.ORA';

--6. Запустите инстанс с новыми параметрами.

--7. Вернитесь к прежним значениям параметров другим способом. 

--8. Получите список управляющих файлов.
SELECT NAME FROM V$CONTROLFILE;

--9. Создайте скрипт для изменения управляющего файла.
ALTER DATABASE BACKUP CONTROLFILE TO TRACE;

ALTER SYSTEM SET CONTROL_FILES = 'D:\Oracle\oradata\XE\CONTROL01.CTL', 'D:\Oracle\oradata\XE\CONTROL02.CTL' SCOPE = SPFILE;

--10. Определите местоположение файла паролей инстанса.
SELECT * FROM V$PASSWORDFILE_INFO;

--11. Убедитесь в наличии этого файла в операционной системе. 

--12. Получите перечень директориев для файлов сообщений и диагностики. 
SELECT * FROM V$DIAG_INFO;

--13. Найдите и исследуйте содержимое протокола работы инстанса (LOG.XML), найдите в нем команды переключения журналов которые вы выполняли.
--D:\Oracle\diag\rdbms\xe\xe\alert\log.xml

--14. Найдите и исследуйте содержимое трейса, в который вы сбросили управляющий файл.
SELECT * FROM v$diag_info WHERE name = 'Diag Trace';