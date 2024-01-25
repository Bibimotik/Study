use UNIVER
exec SP_HELPINDEX 'AUDITORIUM_TYPE'
exec SP_HELPINDEX 'AUDITORIUM'
exec SP_HELPINDEX 'FACULTY'
exec SP_HELPINDEX 'PROFESSION'
exec SP_HELPINDEX 'PULPIT'
exec SP_HELPINDEX 'TEACHER'
exec SP_HELPINDEX 'SUBJECT'
exec SP_HELPINDEX 'GROUPS'
exec SP_HELPINDEX 'STUDENT'
exec SP_HELPINDEX 'PROGRESS'

CREATE TABLE #Table1
(
    ID INT IDENTITY(1,1),
    Simbols1 VARCHAR(50),
    Numbers1 INT
)

SET nocount on;
DECLARE @i INT = 1
WHILE @i <= 1000
BEGIN
    INSERT  #Table1 (Simbols1, Numbers1)
    VALUES ('таблица', @i);
    SET @i = @i + 1;
END

SELECT * FROM #Table1 WHERE Numbers1 between 1 and 100 order by Numbers1

checkpoint;
DBCC DROPCLEANBUFFERS;

CREATE CLUSTERED INDEX #EXPLRE_ID on #Table1(ID)

DROP TABLE #Table1
-----------------------------------------------------------------------
CREATE TABLE #Table2
(
	ID INT IDENTITY(1,1),
	Simbols1 VARCHAR(50),
    Numbers1 INT
)

SET nocount on
DECLARE @b INT = 1

WHILE @b <= 10000
BEGIN
    INSERT  #Table2 (Simbols1, Numbers1)
    VALUES ('таблица' + convert(varchar(10), @b), @b);
    SET @b = @b + 1;
END

SET STATISTICS TIME ON
SELECT * FROM #Table2 WHERE Numbers1 = 9000
SET STATISTICS TIME OFF

CREATE NONCLUSTERED INDEX IDX_TEMP_NONCLUSTERED ON #Table2(Simbols1, Numbers1)
SELECT * from  #Table2 where  ID > 9000 and  Numbers1 < 10000
-----------------------------------------------------------------------
DROP INDEX IDX_TEMP_NONCLUSTERED ON #Table2
CREATE NONCLUSTERED INDEX idx_Simbols1_Includes_Numbers1 ON #Table2(Simbols1) INCLUDE (Numbers1);
SELECT Numbers1 FROM #Table2 WHERE Simbols1 = 'таблица9320';
DROP INDEX idx_Simbols1_Includes_Numbers1 ON #Table2
-----------------------------------------------------------------------
CREATE NONCLUSTERED INDEX idx_Simbols1_StartsWith ON #Table2(Simbols1) 
WHERE Simbols1 = 'таблица9320';

DROP INDEX idx_Simbols1_StartsWith ON #Table2
SELECT * FROM #Table2 WHERE Simbols1 = 'таблица9320';

DROP TABLE #Table2
-----------------------------------------------------------------------
create table  #ex
(    
  id int,
  ff int identity(0,2)
);
declare 
@step int = 0;
while @step<1000
begin
insert #ex(id)
values(@step);
if(@step!=1000)
set @step+=1;
end

create index #ex_id on #ex(id);
INSERT top(4000) #EX(id) select id from #EX;
INSERT top(4000) #EX(id) select id from #EX;
INSERT top(4000) #EX(id) select id from #EX;


select name[индекс], avg_fragmentation_in_percent[фрагментация(%)]
from sys.dm_db_index_physical_stats(db_id(N'tempdb'),
object_id(N'#ex'), null, null, null)ss
join sys.indexes ii on ss.object_id = ii.object_id and ss.index_id=ii.index_id
where name is not null;

ALTER index #ex_id on #ex reorganize;
ALTER index #ex_id on #ex rebuild;
drop table #ex
-----------------------------------------------------------------------
create table #rt(
INTNUMBER int,
STRING nvarchar(15)
);

set nocount on;
declare @iii6    int=0;
while @iii6 < 1000
begin 
 insert #rt(INTNUMBER, STRING) 
 values(FLOOR(100000*rand()),replicate('ap','3'))
 if (@iii6 % 100 = 0)
	print @iii6;
 set @iii6 = @iii6 + 1;
end;

CREATE index #EX_TKEY on #rt(INTNUMBER)
                                 with (fillfactor = 65);

INSERT top(50)percent INTO #rt(INTNUMBER, STRING)
                                              SELECT INTNUMBER, STRING  FROM #rt;
INSERT top(50)percent INTO #rt(INTNUMBER, STRING)
                                              SELECT INTNUMBER, STRING  FROM #rt;
INSERT top(50)percent INTO #rt(INTNUMBER, STRING)
                                              SELECT INTNUMBER, STRING  FROM #rt;
SELECT name [Индекс], avg_fragmentation_in_percent [Фрагментация (%)]
FROM sys.dm_db_index_physical_stats(DB_ID(N'TEMPDB'),    
OBJECT_ID(N'#Lab_09_6'), NULL, NULL, NULL) s6  JOIN sys.indexes i6 
ON s6.object_id = i6.object_id and s6.index_id = i6.index_id  WHERE name is not null;

drop table #rt