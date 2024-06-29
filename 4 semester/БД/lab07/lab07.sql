--1.���������� ����� ������ ������� SGA.
select * from V$SGA;
select SUM(value) from v$sga;

--2.���������� ������� ������� �������� ����� SGA.
select COMPONENT, CURRENT_SIZE from v$sga_dynamic_components  where current_size > 0;

--3.���������� ������� ������� ��� ������� ����.
select component, granule_size from v$sga_dynamic_components  where current_size > 0;

--4.���������� ����� ��������� ��������� ������ � SGA.
select current_size from v$sga_dynamic_free_memory;

--5.���������� ������������ � ������� ������ ������� SGA.
SELECT value FROM v$parameter WHERE name = 'sga_max_size';
SELECT value FROM v$parameter WHERE name = 'sga_target';

--6.���������� ������� ����� ���P, DEFAULT � RECYCLE ��������� ����.
select component, current_size, min_size from v$sga_dynamic_components  where component='KEEP buffer cache' or component='DEFAULT buffer cache' or component='RECYCLE buffer cache';

--7.�������� �������, ������� ����� ���������� � ��� ���P. ����������������� ������� �������.
--8.�������� �������, ������� ����� ������������ � ���� default. ����������������� ������� �������. 
create table MyTable(x int) storage(buffer_pool keep);
create table MyTable2 (x int) cache;
select segment_name, segment_type, tablespace_name, buffer_pool from user_segments where lower(segment_name) like '%mytable%';
drop table MyTable;
drop table MyTable2;

--9.������� ������ ������ �������� �������.
show parameter log_buffer;

--10.������� ������ ��������� ������ � ������� ����.
select pool, name, bytes from v$sgastat where pool = 'large pool' and name = 'free memory';

--11.���������� ������ ������� ���������� � ��������� (dedicated, shared).
select distinct username, service_name, server from v$session;

--12.�������� ������ ������ ���������� � ��������� ����� ������� ���������.
select * from v$bgprocess where paddr != '00';

--13.�������� ������ ���������� � ��������� ����� ��������� ���������.
select * from v$process;

--14.����������, ������� ��������� DBWn �������� � ��������� ������.
SELECT COUNT(*) FROM V$BGPROCESS WHERE NAME LIKE 'DBWn%';

--15.���������� ������� (����� ����������� ����������).
select * from v$services;

--16.�������� ��������� ��� ��������� �����������.
select * from v$dispatcher;

--17.������� � ������ Windows-�������� ������, ����������� ������� LISTENER.
--18.����������������� � �������� ���������� ����� LISTENER.ORA. 
--20.�������� ������ ����� ��������, ������������� ��������� LISTENER. 
SELECT * from v$services where network_name = 'LISTENER';