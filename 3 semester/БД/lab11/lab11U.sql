use UNIVER
DECLARE @sp char(20), @s char(300) = '';
DECLARE Discip CURSOR for 
	SELECT  SUBJECT.SUBJECT 
	from PULPIT
	join SUBJECT
	on PULPIT.PULPIT = SUBJECT.PULPIT where PULPIT.PULPIT like 'ИСиТ'
OPEN Discip;
	FETCH Discip into @sp;
	print 'Дисциплины';
	while @@FETCH_STATUS = 0
	begin
		set @s = rtrim(@sp) + ',' + @s;
		FETCH Discip into @sp;
	end;
	print @s;
CLOSE Discip;
DEALLOCATE Discip;
---------------------------------------------------------------------
DECLARE @teacher char(20)
DECLARE list2_1 CURSOR LOCAL for 
	SELECT T.TEACHER from TEACHER T;
OPEN list2_1;
	FETCH NEXT FROM list2_1  INTO @teacher;
	while @@FETCH_STATUS = 0
	begin
		print @teacher;
		FETCH NEXT FROM list2_1 INTO @teacher;
	end;
	go
DEALLOCATE list2_1; --напишиет что такого нет
----------------------------
DECLARE @teacher2 char(20);
DECLARE list2_2 CURSOR GLOBAL for 
	SELECT T.TEACHER from TEACHER T;
OPEN list2_2;
	FETCH   list2_2 INTO @teacher2;
	print '1' + @teacher2;
	go

	DECLARE @teacher2 char(20);
		FETCH   list2_2 INTO @teacher2;
		print '2' + @teacher2;
close list2_2;
DEALLOCATE list2_2;
---------------------------------------------------------------------
DECLARE @tid char(40), @tnm char(40), @tgn char(40), @ttt char(40)
DECLARE prof CURSOR LOCAL STATIC for
	SELECT FACULTY, PROFESSION, PROFESSION_NAME, QUALIFICATION
	FROM dbo.PROFESSION where FACULTY = 'ИТ'
	open prof;
	print   'Количество строк : '+cast(@@CURSOR_ROWS as varchar(5));
	INSERT PROFESSION (FACULTY, PROFESSION, PROFESSION_NAME, QUALIFICATION)
		values ('ИТ',  '1-55 13 13',   'Инфи ', 'системотехник');
	FETCH  prof into @tid, @tnm, @tgn, @ttt;
	while @@fetch_status = 0
		begin
			print @tid + ' '+ @tnm + ' '+ @tgn + ' '+@ttt;
			fetch prof into @tid, @tnm, @tgn, @ttt;
		end;
close prof;
---------------------------------------------------------------------
DECLARE  @tc int, @rn int;
DECLARE Primer1 CURSOR LOCAL DYNAMIC SCROLL for
	SELECT row_number() over (order by IDSTUDENT) N, IDSTUDENT
	FROM dbo.STUDENT
open Primer1;
	FETCH  Primer1 into  @tc, @rn;                 
	print 'следующая строка        : ' + cast(@tc as varchar(3)) + ' '+  cast(@rn as varchar(10));
	FETCH  LAST from  Primer1 into @tc, @rn
	print 'последняя строка          : ' +  cast(@tc as varchar(3)) + ' '+  cast(@rn as varchar(10));
close Primer1;
go
---------------------------------------------------------------------
DECLARE @idstud int;
DECLARE my CURSOR LOCAL DYNAMIC for 
	SELECT IDSTUDENT
		from STUDENT for update;
open my;
	FETCH my into @idstud;
	DELETE STUDENT WHERE CURRENT OF my;
	FETCH my into @idstud;
	UPDATE STUDENT set IDGROUP = IDGROUP + 1 WHERE CURRENT OF my;
close my;
go
---------------------------------------------------------------------
DECLARE @nt char(40), @is char(40);
DECLARE OTCHI CURSOR LOCAL DYNAMIC FOR
	SELECT PROGRESS.NOTE, STUDENT.IDSTUDENT
	from PROGRESS
    join STUDENT ON PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT FOR UPDATE;
open OTCHI;
	FETCH OTCHI INTO @nt, @is;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @nt <= 4
		BEGIN
			print @nt + '  ' + @is;
			DELETE PROGRESS WHERE IDSTUDENT = @is AND PROGRESS.NOTE = @nt;
			DELETE STUDENT WHERE STUDENT.IDSTUDENT = @is;
		END;
		FETCH OTCHI INTO @nt, @is;
	END;
close OTCHI;
go
select * from PROGRESS
-------
DECLARE @mark int;
DECLARE CHEN CURSOR LOCAL DYNAMIC FOR
    SELECT NOTE
    FROM PROGRESS
    WHERE IDSTUDENT = '1014' FOR UPDATE;

OPEN CHEN;
  FETCH CHEN INTO @mark;
  WHILE @@FETCH_STATUS = 0
  BEGIN
		IF @mark < 10
		BEGIN
			UPDATE PROGRESS
			SET NOTE = @mark + 1
			WHERE CURRENT OF CHEN;
		END;
		FETCH OTCHI INTO @mark;
	END;
CLOSE CHEN;
---------------------------------------------------------------------
DECLARE @FacultyName varchar(50)
DECLARE @PulpitName varchar(100)
DECLARE @TeacherCount int
DECLARE @SubjectsList varchar(max)
DECLARE @PrevFacultyName varchar(50) = ''

DECLARE ReportCursor CURSOR STATIC FOR
SELECT F.FACULTY, P.PULPIT, COUNT(T.TEACHER) AS TeacherCount, ISNULL((SELECT STRING_AGG(ISNULL(SUBJECT, 'нет'), ', ')
	FROM ( SELECT DISTINCT SUBJECT
    FROM SUBJECT
        WHERE P.PULPIT = SUBJECT.PULPIT) AS DistinctSubjects ), 'нет') AS SubjectsList
	FROM FACULTY F
	JOIN PULPIT P ON F.FACULTY = P.FACULTY
	LEFT JOIN TEACHER T ON P.PULPIT = T.PULPIT
	GROUP BY F.FACULTY, P.PULPIT, P.PULPIT  
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

		FETCH NEXT FROM ReportCursor INTO @FacultyName, @PulpitName, @TeacherCount, @SubjectsList
END
CLOSE ReportCursor
DEALLOCATE ReportCursor