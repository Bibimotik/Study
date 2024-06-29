--1. ���������� �������������� ����� ���������� ��������.
SHOW PARAMETER SPFILE;

--3. ����������� PFILE � ������ XXX_PFILE.ORA.
CREATE PFILE = 'BMO_PFILE.ORA' FROM SPFILE;

--4. �������� �����-���� �������� � ����� PFILE.

--5. �������� ����� SPFILE.
CREATE SPFILE='D:\Oracle\dbhomeXE\database\SPFILE.ORA' FROM PFILE='D:\Oracle\dbhomeXE\database\BMO_PFILE.ORA';

--6. ��������� ������� � ������ �����������.

--7. ��������� � ������� ��������� ���������� ������ ��������. 

--8. �������� ������ ����������� ������.
SELECT NAME FROM V$CONTROLFILE;

--9. �������� ������ ��� ��������� ������������ �����.
ALTER DATABASE BACKUP CONTROLFILE TO TRACE;

ALTER SYSTEM SET CONTROL_FILES = 'D:\Oracle\oradata\XE\CONTROL01.CTL', 'D:\Oracle\oradata\XE\CONTROL02.CTL' SCOPE = SPFILE;

--10. ���������� �������������� ����� ������� ��������.
SELECT * FROM V$PASSWORDFILE_INFO;

--11. ��������� � ������� ����� ����� � ������������ �������. 

--12. �������� �������� ����������� ��� ������ ��������� � �����������. 
SELECT * FROM V$DIAG_INFO;

--13. ������� � ���������� ���������� ��������� ������ �������� (LOG.XML), ������� � ��� ������� ������������ �������� ������� �� ���������.
--D:\Oracle\diag\rdbms\xe\xe\alert\log.xml

--14. ������� � ���������� ���������� ������, � ������� �� �������� ����������� ����.
SELECT * FROM v$diag_info WHERE name = 'Diag Trace';