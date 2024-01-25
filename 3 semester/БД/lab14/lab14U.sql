------------------------------------1.1------------------------------------
IF OBJECT_ID('dbo.COUNT_STUDENTS', 'FN') IS NOT NULL
    DROP FUNCTION dbo.COUNT_STUDENTS;
GO

CREATE FUNCTION dbo.COUNT_STUDENTS (@faculty VARCHAR(20))
RETURNS INT
AS 
BEGIN
    DECLARE @rc INT = 0;

    SET @rc = (
        SELECT COUNT(IDSTUDENT)
        FROM STUDENT S
        JOIN GROUPS G ON S.IDGROUP = G.IDGROUP
        JOIN FACULTY F ON G.FACULTY = F.FACULTY
        WHERE F.FACULTY = @faculty
    );

    RETURN @rc;
END;
GO

DECLARE @f INT = dbo.COUNT_STUDENTS('ХТиТ');
PRINT 'Количество студентов = ' + CAST(@f AS VARCHAR(4));
GO
------------------------------------1.2------------------------------------

IF OBJECT_ID('dbo.COUNT_STUDENTS', 'FN') IS NOT NULL
    DROP FUNCTION dbo.COUNT_STUDENTS;
GO

CREATE FUNCTION dbo.COUNT_STUDENTS (@faculty VARCHAR(20) = NULL, @prof VARCHAR(20) = NULL)
RETURNS INT
AS 
BEGIN
    DECLARE @rc INT = 0;

    IF @prof IS NULL
        SET @rc = (
            SELECT COUNT(IDSTUDENT)
            FROM STUDENT S
            JOIN GROUPS G ON S.IDGROUP = G.IDGROUP
            JOIN FACULTY F ON G.FACULTY = F.FACULTY
            WHERE F.FACULTY = @faculty
        );
    ELSE
        SET @rc = (
            SELECT COUNT(IDSTUDENT)
            FROM STUDENT S
            JOIN GROUPS G ON S.IDGROUP = G.IDGROUP
            WHERE G.PROFESSION = @prof
        );

    RETURN @rc;
END;
GO

DECLARE @fp INT = dbo.COUNT_STUDENTS(DEFAULT, '1-36 01 08');
PRINT ' = ' + CAST(@fp AS VARCHAR(4));
GO

------------------------------------2------------------------------------
IF OBJECT_ID('dbo.FSUBJECTS', 'FN') IS NOT NULL
    DROP FUNCTION dbo.FSUBJECTS;
GO

CREATE FUNCTION dbo.FSUBJECTS (@p VARCHAR(20))
RETURNS VARCHAR(300)
AS 
BEGIN 
    DECLARE @s CHAR(20);
    DECLARE @a VARCHAR(300) = 'дисциплины: ';
    DECLARE CSUBJECTS CURSOR LOCAL FOR
        SELECT S.SUBJECT
        FROM SUBJECT S
        WHERE S.PULPIT = @p;

    OPEN CSUBJECTS;
    FETCH CSUBJECTS INTO @s;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @a = @a + RTRIM(@s) + ',';
        FETCH CSUBJECTS INTO @s;
    END;

    CLOSE CSUBJECTS;
    DEALLOCATE CSUBJECTS;

    RETURN @a;
END;
GO

SELECT P.PULPIT, dbo.FSUBJECTS(P.PULPIT) AS 'SUBJECTS'
FROM PULPIT P;


------------------------------------3------------------------------------
IF OBJECT_ID('dbo.FFACPUL', 'IF') IS NOT NULL
    DROP FUNCTION dbo.FFACPUL;
GO

CREATE FUNCTION dbo.FFACPUL (@ff VARCHAR(20), @pp VARCHAR(20))
RETURNS TABLE
AS 
RETURN (
    SELECT F.FACULTY, P.PULPIT
    FROM FACULTY F
    LEFT JOIN PULPIT P ON P.FACULTY = F.FACULTY
    WHERE F.FACULTY = ISNULL(@ff, F.FACULTY)
        AND P.PULPIT = ISNULL(@pp, P.PULPIT)
);
GO

SELECT * FROM dbo.FFACPUL(NULL, NULL);
SELECT * FROM dbo.FFACPUL('ИТ', NULL);
SELECT * FROM dbo.FFACPUL(NULL, 'ИСиТ');
SELECT * FROM dbo.FFACPUL('ИТ', 'ИСиТ');

------------------------------------4------------------------------------
IF OBJECT_ID('dbo.FCTEACHER', 'FN') IS NOT NULL
    DROP FUNCTION dbo.FCTEACHER;
GO

CREATE FUNCTION dbo.FCTEACHER (@p VARCHAR(20))
RETURNS INT
AS 
BEGIN 
    DECLARE @rc INT = (
        SELECT COUNT(*)
        FROM TEACHER T
        WHERE PULPIT = ISNULL(@p, PULPIT)
    );

    RETURN @rc;
END;
GO

SELECT PULPIT, dbo.FCTEACHER(PULPIT) AS 'Кол-во преподавателей' FROM PULPIT;
SELECT dbo.FCTEACHER(NULL) AS 'Всего преподавателей';


GO
------------------------------------6------------------------------------
create function SIX_PULPITS(@f varchar(20)) returns int
as begin declare @rc int = 0;
set @rc = (select count(PULPIT) from PULPIT where PULPIT.FACULTY = @f);
return @rc; 
end;
go
create function SIX_GROUPS(@f varchar(20)) returns int
as begin declare @rc int = 0;
set @rc = (select count(GROUPS.FACULTY) from GROUPS where GROUPS.FACULTY = @f);
return @rc; 
end;
go
create function SIX_PROF(@f varchar(20)) returns int
as begin declare @rc int = 0;
set @rc = (select count(PROFESSION) from PROFESSION where FACULTY = @f );
return @rc; 
end;
go

	create function FACULTY_REPORT(@c int)
	returns @fr table
	                     ( [Факультет] varchar(50), [Количество кафедр] int, [Количество групп]  int, 
	                                                                 [Количество студентов] int, [Количество специальностей] int )

	as begin 
                 declare cc CURSOR static for 
	       select FACULTY from FACULTY 
                                                    where dbo.COUNT_STUDENTS(FACULTY, default) > @c; 
	       declare @f varchar(30);
	       open cc;  
                 fetch cc into @f;
	       while @@fetch_status = 0
	       begin
	            insert @fr values( @f, dbo.SIX_PULPITS(@f),
	            dbo.SIX_GROUPS(@f),   dbo.COUNT_STUDENTS(@f, default),
	              dbo.SIX_PROF(@f)); 
	            fetch cc into @f;  
	       end;   
                 return; 
	end;
go

select * from FACULTY_REPORT(17)
go


------------------------------------7------------------------------------
CREATE PROCEDURE PRINT_REPORTX @f CHAR(10) = NULL, @p CHAR(10) = NULL
as
begin
    SET NOCOUNT ON;

    DECLARE @FacultyName varchar(50)
    DECLARE @PulpitName varchar(100)
    DECLARE @TeacherCount int
    DECLARE @SubjectsList varchar(max)
    DECLARE @PrevFacultyName varchar(50) = ''
    DECLARE @TotalDepartments int = 0

    BEGIN TRY
        DECLARE ReportCursor CURSOR STATIC FOR
        SELECT 
            F.FACULTY, 
            P.PULPIT, 
            dbo.FCTEACHER(P.PULPIT) AS TeacherCount,
            dbo.FSUBJECTS(P.PULPIT) AS SubjectsList
        FROM dbo.FFACPUL(@f, @p) AS F
        JOIN PULPIT P ON F.FACULTY = P.FACULTY
        ORDER BY F.FACULTY, P.PULPIT;

        OPEN ReportCursor

        FETCH NEXT FROM ReportCursor INTO @FacultyName, @PulpitName, @TeacherCount, @SubjectsList

        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @PrevFacultyName != @FacultyName
            BEGIN
                PRINT 'Факультет: ' + @FacultyName
                SET @PrevFacultyName = @FacultyName
            END

            PRINT '  Кафедра: ' + @PulpitName
            PRINT '      Количество преподавателей: ' + CAST(@TeacherCount AS varchar(10))
            PRINT '      Дисциплины: ' + @SubjectsList
            PRINT ''

            SET @TotalDepartments += 1

            FETCH NEXT FROM ReportCursor INTO @FacultyName, @PulpitName, @TeacherCount, @SubjectsList
        END

        CLOSE ReportCursor
        DEALLOCATE ReportCursor
    END TRY
    BEGIN CATCH
        PRINT 'Произошла ошибка: ' + ERROR_MESSAGE()
    END CATCH

    RETURN @TotalDepartments
END


DECLARE @TotalDepartmentsCount INT
EXEC @TotalDepartmentsCount = PRINT_REPORTX @f = 'ИТ'

drop PROCEDURE PRINT_REPORTX
DROP FUNCTION dbo.FCTEACHER;
drop function COUNT_STUDENTS
DROP FUNCTION dbo.FSUBJECTS;