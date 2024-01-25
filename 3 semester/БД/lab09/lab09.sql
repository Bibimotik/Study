--1 задание--
declare @charvariable char(10) = 'Hello'
declare @varcharvariable varchar(20) = 'World'

declare @datetimevariable datetime
declare @timevariable time
declare @intvariable int
declare @smallintvariable smallint
declare @tinyintvariable tinyint
declare @numericvariable numeric(12, 5)

set @datetimevariable = getdate()
set @timevariable = convert(time, getdate())
set @intvariable = 42
set @smallintvariable = 10
set @tinyintvariable = 3
set @numericvariable = 123.456

select @charvariable as charvariable, @varcharvariable as varcharvariable

print 'datetimevariable: ' + convert(varchar, @datetimevariable, 120)
print 'timevariable: ' + convert(varchar, @timevariable, 108)
print 'intvariable: ' + convert(varchar, @intvariable)
print 'smallintvariable: ' + convert(varchar, @smallintvariable)
print 'tinyintvariable: ' + convert(varchar, @tinyintvariable)
print 'numericvariable: ' + convert(varchar, @numericvariable)
--2 задание--
DECLARE @TotalCapacity INT
SELECT @TotalCapacity = SUM(AUDITORIUM_CAPACITY) FROM AUDITORIUM

IF @TotalCapacity > 200
BEGIN
    DECLARE @AuditoriumCount INT
    SELECT @AuditoriumCount = COUNT(*) FROM AUDITORIUM

    DECLARE @AverageCapacity FLOAT
    SELECT @AverageCapacity = AVG(AUDITORIUM_CAPACITY) FROM AUDITORIUM

    DECLARE @BelowAverageCount INT
    SELECT @BelowAverageCount = COUNT(*) FROM AUDITORIUM WHERE AUDITORIUM_CAPACITY < @AverageCapacity

    DECLARE @BelowAveragePercentage FLOAT
    SELECT @BelowAveragePercentage = (@BelowAverageCount * 100.0) / @AuditoriumCount

    PRINT 'Количество аудиторий: ' + CAST(@AuditoriumCount AS VARCHAR(10))
    PRINT 'Средняя вместимость аудиторий: ' + CAST(@AverageCapacity AS VARCHAR(10))
    PRINT 'Количество аудиторий с вместимостью меньше средней: ' + CAST(@BelowAverageCount AS VARCHAR(10))
    PRINT 'Процент аудиторий с вместимостью меньше средней: ' + CAST(@BelowAveragePercentage AS VARCHAR(10)) + '%'
END
ELSE
BEGIN
    PRINT 'Общая вместимость аудиторий: ' + CAST(@TotalCapacity AS VARCHAR(10))
END
--3 задание--
PRINT 'ROWCOUNT: ' + CONVERT(VARCHAR, @@ROWCOUNT)
PRINT 'VERSION: ' + @@VERSION
PRINT 'SPID: ' + CONVERT(VARCHAR, @@SPID)
PRINT 'ERROR: ' + CONVERT(VARCHAR, @@ERROR)
PRINT 'SERVERNAME: ' + @@SERVERNAME
PRINT 'TRANCOUNT: ' + CONVERT(VARCHAR, @@TRANCOUNT)
PRINT 'FETCH_STATUS: ' + CONVERT(VARCHAR, @@FETCH_STATUS)
PRINT 'NESTLEVEL: ' + CONVERT(VARCHAR, @@NESTLEVEL)
--4 задание--
DECLARE @x int, @t int, @z float;
SET @t = 5
SET @x = 5
IF(@t > @x) 
SET @z=POWER(sin(@t), 2)
ELSE IF (@t < @x) SET @z =4*(@t+@x);
ELSE SET @z=1- exp(@x-2);
PRINT 'z=' +  CONVERT(VARCHAR, @z)
---------------------------
DECLARE @name varchar, @surname varchar(10), @otchestvo varchar;
SET @name = 'Михаил';
SET @surname = 'Банкузов';
SET @otchestvo = 'Олегович';
PRINT 'ФИО:' + @surname + '.' + @name + '.' + @otchestvo + '.';
---------------------------
SELECT NAME, BDAY, IDSTUDENT, DATEDIFF(YEAR, BDAY, GETDATE()) AS age FROM STUDENT
WHERE MONTH(BDAY) = MONTH(DATEADD(MONTH, 1, GETDATE()))
---------------------------
SELECT STUDENT.IDSTUDENT, GROUPS.IDGROUP, PROGRESS.NOTE, PROGRESS.PDATE, PROGRESS.SUBJECT,
DATENAME(WEEKDAY, BDAY) AS den FROM PROGRESS JOIN STUDENT ON PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
JOIN GROUPS ON STUDENT.IDGROUP = GROUPS.IDGROUP
WHERE PROGRESS.SUBJECT = 'СУБД'
--5 задание--
DECLARE @AverageGrade FLOAT;
SET @AverageGrade = (SELECT AVG(NOTE) FROM PROGRESS);

IF @AverageGrade >= 9
BEGIN
    PRINT 'Отличный уровень успеваемости';
END
ELSE IF @AverageGrade >= 7
BEGIN
    PRINT 'Хороший уровень успеваемости';
END
ELSE IF @AverageGrade >= 5
BEGIN
    PRINT 'Удовлетворительный уровень успеваемости';
END
ELSE
BEGIN
    PRINT 'Низкий уровень успеваемости';
END
--6 задание--
SELECT
  S.NAME AS STUDENT_NAME,
  G.PROFESSION AS STUDENT_PROFESSION,
  P.SUBJECT AS COURSE,
  P.NOTE AS GRADE,
  CASE
    WHEN P.NOTE >= 9 THEN 'Отлично'
    WHEN P.NOTE >= 7 THEN 'Хорошо'
    WHEN P.NOTE >= 4 THEN 'Удовлетворительно'
    ELSE 'Неудовлетворительно'
  END AS GRADE_CATEGORY
FROM
  STUDENT S
  JOIN GROUPS G ON S.IDGROUP = G.IDGROUP
  JOIN PROGRESS P ON S.IDSTUDENT = P.IDSTUDENT
  JOIN FACULTY F ON G.FACULTY = F.FACULTY
WHERE
  F.FACULTY = 'ХТиТ';
--7 задание--
CREATE TABLE #TempTable (
  Column1 INT,
  Column2 VARCHAR(50),
  Column3 DATE
);

DECLARE @Counter INT = 1;

WHILE @Counter <= 10
BEGIN
  INSERT INTO #TempTable (Column1, Column2, Column3)
  VALUES (@Counter, 'Value ' + CAST(@Counter AS VARCHAR), GETDATE());

  SET @Counter = @Counter + 1;
END;

SELECT * FROM #TempTable;

DROP TABLE #TempTable;
--8 задание--
CREATE FUNCTION dbo.CalculateSum
(
    @a INT,
    @b INT
)
RETURNS INT
AS
BEGIN
    RETURN @a + @b;
END;
GO

SELECT dbo.CalculateSum(5, 3) AS SumResult;

drop FUNCTION  dbo.CalculateSum
--9 задание--
BEGIN TRY
    CREATE TABLE #TempTable (ID INT);

    INSERT INTO #TempTable (ID) VALUES (1);

END TRY
BEGIN CATCH
    SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_MESSAGE() AS ErrorMessage,
        ERROR_LINE() AS ErrorLine,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState;
END CATCH;