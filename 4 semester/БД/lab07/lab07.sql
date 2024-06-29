--1.Определите общий размер области SGA.
select * from V$SGA;
select SUM(value) from v$sga;

--2.Определите текущие размеры основных пулов SGA.
select COMPONENT, CURRENT_SIZE from v$sga_dynamic_components  where current_size > 0;

--3.Определите размеры гранулы для каждого пула.
select component, granule_size from v$sga_dynamic_components  where current_size > 0;

--4.Определите объем доступной свободной памяти в SGA.
select current_size from v$sga_dynamic_free_memory;

--5.Определите максимальный и целевой размер области SGA.
SELECT value FROM v$parameter WHERE name = 'sga_max_size';
SELECT value FROM v$parameter WHERE name = 'sga_target';

--6.Определите размеры пулов КЕЕP, DEFAULT и RECYCLE буферного кэша.
select component, current_size, min_size from v$sga_dynamic_components  where component='KEEP buffer cache' or component='DEFAULT buffer cache' or component='RECYCLE buffer cache';

--7.Создайте таблицу, которая будет помещаться в пул КЕЕP. Продемонстрируйте сегмент таблицы.
--8.Создайте таблицу, которая будет кэшироваться в пуле default. Продемонстрируйте сегмент таблицы. 
create table MyTable(x int) storage(buffer_pool keep);
create table MyTable2 (x int) cache;
select segment_name, segment_type, tablespace_name, buffer_pool from user_segments where lower(segment_name) like '%mytable%';
drop table MyTable;
drop table MyTable2;

--9.Найдите размер буфера журналов повтора.
show parameter log_buffer;

--10.Найдите размер свободной памяти в большом пуле.
select pool, name, bytes from v$sgastat where pool = 'large pool' and name = 'free memory';

--11.Определите режимы текущих соединений с инстансом (dedicated, shared).
select distinct username, service_name, server from v$session;

--12.Получите полный список работающих в настоящее время фоновых процессов.
select * from v$bgprocess where paddr != '00';

--13.Получите список работающих в настоящее время серверных процессов.
select * from v$process;

--14.Определите, сколько процессов DBWn работает в настоящий момент.
SELECT COUNT(*) FROM V$BGPROCESS WHERE NAME LIKE 'DBWn%';

--15.Определите сервисы (точки подключения экземпляра).
select * from v$services;

--16.Получите известные вам параметры диспетчеров.
select * from v$dispatcher;

--17.Укажите в списке Windows-сервисов сервис, реализующий процесс LISTENER.
--18.Продемонстрируйте и поясните содержимое файла LISTENER.ORA. 
--20.Получите список служб инстанса, обслуживаемых процессом LISTENER. 
SELECT * from v$services where network_name = 'LISTENER';