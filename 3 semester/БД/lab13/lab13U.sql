----------------------------1111111111----------------------------------
create procedure PSUBJECT
as 
begin
	declare @k int = (select count(*) from SUBJECT);
	select SUBJECT [код], SUBJECT_NAME [дисциплина], PULPIT [кафедра] from SUBJECT;
	return @k;
end

declare @d int = 0;
exec @d = PSUBJECT;
PRINT @d ;
----------------------------2222222222----------------------------------
go
alter procedure PSUBJECT @p varchar(20), @c int output
as 
begin
	declare @k int = (select count(*) from SUBJECT);
	select SUBJECT [код], SUBJECT_NAME [дисциплина], PULPIT [кафедра] from SUBJECT;
	select SUBJECT [код], SUBJECT_NAME [дисциплина], PULPIT [кафедра] from SUBJECT where SUBJECT.PULPIT = @p;
	set @c  =  @@ROWCOUNT;
	return @k;
end
go
declare @k int = 0, @r int = 0;
exec @k = PSUBJECT @p= N'ИСиТ', @c = @r output;
print N'k = ' + cast(@k as varchar(3));
print N'c ' + '= ' + cast(@r as varchar(30));
----------------------------3333333333----------------------------------
create table #SUBJECT (
	SUBJECT nchar(10),
	SUBJECT_NAME nvarchar(100),
	PULPIT nchar(20)
);
go
ALTER procedure PSUBJECT @p nvarchar(20)
AS
begin
	declare @k int = (select count(*) from  SUBJECT);
	select * from SUBJECT where PULPIT = @p;
end;
GO
insert #SUBJECT exec PSUBJECT @p= N'ИСиТ'
insert #SUBJECT exec PSUBJECT @p= N'ЛУ'
select * from #SUBJECT 
drop table #SUBJECT
drop procedure PSUBJECT
----------------------------4444444444----------------------------------
go
create procedure PAUDITORIUM_INSERT @a char(20), @t char(10), @n varchar(50), @c  int = 0
as 

begin try
	insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME,   AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) 
	values (@a, @t, @n, @c)
	return 1;
end try
begin catch
		print N'Номер Ошибки: ' + cast(error_number() as varchar(6));
		print N'сообщение: ' + error_message()
		print N'уровень: ' + cast(error_severity() as varchar(6));
		print N'метка: ' + cast(error_state() as varchar(8));
		print N'номер строки: ' + cast(error_line() as varchar(8));
		if ERROR_PROCEDURE() is not null
		print N'Имя процедуры:' + error_procedure();
		if @@TRANCOUNT > 0 rollback;
		return -1;
end catch

declare @rc int;

exec @rc = PAUDITORIUM_INSERT @a = '113-3', @t = N'113-3', @n = 'ЛБ-К', @c = 15
print N'код ошибки: ' + CAST(@rc as varchar(3));
select * from AUDITORIUM
----------------------------5555555555----------------------------------
go
create procedure SUBJECT_REPORT @p nchar(20)
as
declare @rc int = 0;
begin try
	declare @tv nchar(20), @t nchar(300) = '';
	declare SUBREP cursor for 
	select SUBJECT from SUBJECT where PULPIT = @p;
	IF not exists (select SUBJECT from SUBJECT where PULPIT = @p)
		raiserror(N'ошибка', 11, 1);
	else 
		open SUBREP;
		FETCH SUBREP into @tv;
		print N'Дисциплины кафедры: ';
		while @@FETCH_STATUS = 0
		begin
			set @t = RTRIM(@tv) +', ' +  @t;
			set @rc = @rc + 1;
			fetch SUBREP into @tv;
		end;
		print @t;
		close SUBREP;
	
		return @rc;
end try
begin catch
	print N'ошибка в параметрах '  + @p
	IF ERROR_PROCEDURE() is not null
	print N'имя процедуры:' + error_procedure();
	return @rc;
end catch;
go
declare @rc int;
exec @rc = SUBJECT_REPORT @p = N'ИСиТ';
print N'количество дисциплин = ' + cast(@rc as varchar(3));


select SUBJECT from SUBJECT where PULPIT = N'ИСиТ';
drop procedure SUBJECT_REPORT 
----------------------------6666666666----------------------------------
go
alter procedure PAUDITORIUM_INSERT  @a char(20), @t char(10), @n varchar(50), @c  int = 0, @tn nvarchar(50)
as
declare 
@rc int = 1;
begin try
	set transaction isolation level SERIALIZABLE
	begin tran
	insert into AUDITORIUM_TYPE( AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
	values (@n, @tn)
	exec @rc = PAUDITORIUM_INSERT @a, @t, @n, @c;
	commit tran;
	return @rc;
end try
begin catch
	print N'номер ошибки  : ' + cast(error_number() as varchar(6));
	print N'сообщение : ' + error_message()
	print N'уровень: ' + cast(error_severity() as varchar(6));
	print N'уровень: ' + cast(error_state() as varchar(8));
	print N'номер строки: ' + cast(error_line() as varchar(8));
	if ERROR_PROCEDURE() is not null
		print N'имя процедуры :' + error_procedure();
	if @@TRANCOUNT > 0 rollback;
	return -1;
end catch;
go
declare @rc int;
exec @rc = PAUDITORIUM_INSERT @a = '161-9', @t = N'161-9', @n = 'ЛБббб-К', @c = 15, @tn = N'Компьютерный класс бббб';
print N'код ошибки= ' + CAST(@rc as varchar(3));



select * from AUDITORIUM
select * from AUDITORIUM_TYPE
--drop procedure PAUDITORIUM_INSERTX
drop procedure PAUDITORIUM_INSERT
----------------------------8888888888----------------------------------
go
CREATE PROCEDURE PRINT_REPORT @f CHAR(10) = NULL, @p CHAR(10) = NULL
as begin
    SET NOCOUNT ON
    DECLARE @FacultyName varchar(50)
    DECLARE @PulpitName varchar(100)
    DECLARE @TeacherCount int
    DECLARE @SubjectsList varchar(max)
    DECLARE @PrevFacultyName varchar(50) = ''
    DECLARE @TotalDepartments int = 0

   
        DECLARE ReportCursor CURSOR STATIC FOR
        SELECT F.FACULTY, P.PULPIT, COUNT(T.TEACHER) AS TeacherCount, ISNULL((SELECT STRING_AGG(ISNULL(SUBJECT, 'нет'), ', ')
                FROM ( SELECT DISTINCT SUBJECT
                       FROM SUBJECT
                       WHERE P.PULPIT = SUBJECT.PULPIT) AS DistinctSubjects ), 'нет') AS SubjectsList
        FROM FACULTY F
        JOIN PULPIT P ON F.FACULTY = P.FACULTY
        LEFT JOIN TEACHER T ON P.PULPIT = T.PULPIT
        WHERE (@f IS NULL OR F.FACULTY = @f)
          AND (@p IS NULL OR P.PULPIT = @p)
        GROUP BY F.FACULTY, P.PULPIT, P.PULPIT  
        ORDER BY F.FACULTY, P.PULPIT
    raiserror('ошибка', 11, 1)
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
   

    RETURN @TotalDepartments
END


go

DECLARE @fp INT
EXEC @fp = PRINT_REPORT @f = 'ЛХФ', @p = NULL
DROP PROCEDURE PRINT_REPORT