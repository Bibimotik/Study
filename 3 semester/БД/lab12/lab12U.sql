--------------------------1111111111---------------------------------
declare @c int, @flag char = 'c';
SET IMPLICIT_TRANSACTIONS  ON
	CREATE table X
	(
		S varchar(10),
		K int
	);
	INSERT X values ('first', 1),('second', 2),('third', 3);
	SELECT * from X;
	set @c = (select count(*) from X);
	print 'количество строк в таблице X: ' + cast( @c as varchar(2));
	if @flag = 'c'  commit;
	        else   rollback;
SET IMPLICIT_TRANSACTIONS  OFF
drop table X;
--------------------------2222222222---------------------------------
use UNIVER
begin try
	begin tran
		update TEACHER
			set GENDER = 'X'
			where TEACHER = 'СМЛВ';
	commit tran;
end try
begin catch
	if @@TRANCOUNT > 0
		ROLLBACK;

	select 
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_STATE() AS ErrorState,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
end catch
select * from TEACHER where TEACHER_NAME = 'Смелов Владимир Владиславович';
--------------------------3333333333---------------------------------
begin try
	begin tran;

	-- Изменяем имя преподавателя
	UPDATE TEACHER
		set TEACHER_NAME = 'Смелов Владимир Владиславович'
		where TEACHER = 'СМЛВ';

	save tran SavePoint1;

	UPDATE TEACHER
		set GENDER = 'X'
		where TEACHER = 'СМЛВ';

	commit tran;
end try
begin catch
	-- Если произошла ошибка, откатываем транзакцию до контрольной точки
	if @@TRANCOUNT > 0
	begin
		rollback tran SavePoint1;
		commit tran;
		end
	select 
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_STATE() AS ErrorState,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
end catch;
select * from TEACHER where TEACHER_NAME = 'Смелов Владимир Владиславович';
--------------------------4444444444---------------------------------
-- Сценарий A (READ UNCOMMITTED)
set transaction isolation level READ UNCOMMITTED

begin transaction 
-------------------------- t1 ------------------
	select @@SPID, 'select STUDENTS' 'operation', * FROM STUDENT where  NAME = 'Никитенко Екатерина Дмитриевна';

	-- UPDATE STUDENTS SET NAME = 'Игорь' WHERE NAME = 'Иван';

	select @@SPID, 'select STUDENTS' 'operation', * FROM STUDENT where  NAME = 'Никитенко Екатерина Дмитриевна';

commit
-------------------------- t2 -----------------
--- B --
begin transaction 

	select @@SPID 
	update  STUDENT set  NAME = 'Игорь2' where NAME = 'Сергель Виолетта Николаевна';
-------------------------- t1 --------------------
-------------------------- t2 --------------------
rollback;
select * from STUDENT
--------------------------5555555555---------------------------------
-- Сценарий A (READ COMMITTED)
set transaction isolation level READ COMMITTED

begin transaction 
	select count(*) FROM STUDENT where  NAME = 'Никитенко Екатерина Дмитриевна';
	-------------------------- t1 -----------------
	-------------------------- t2 -----------------
	select 'select STUDENTS' 'operation', count(*) FROM STUDENT where  NAME = 'Никитенко Екатерина Дмитриевна';

commit;

--- B --
begin transaction 
	-------------------------- t1 --------------------
	update  STUDENT set  NAME = 'Сергель Виолетта Николаевна' where NAME = 'Сергель Виолетта Николаевна';
commit;
-------------------------- t2 --------------------
select * from STUDENT
--------------------------6666666666---------------------------------
-- A ---
set transaction isolation level  REPEATABLE READ 
begin transaction 
	select NAME FROM STUDENT where NAME = 'Никитенко Екатерина Дмитриевна';
	-------------------------- t1 ------------------ 
	-------------------------- t2 -----------------
	select case
		when NAME = 'Селило Екатерина Геннадьевна' then 'insert  STUDENT'  else ' ' 
end 'результат', NAME from STUDENT  where NAME = 'Никитенко Екатерина Дмитриевна';
commit; 
--- B ---	
begin transaction 	  
-------------------------- t1 --------------------
	insert into STUDENT (IDGROUP,NAME, BDAY) values (18, 'Белка Стрелка Николаевна', '02.11.1995');
commit; 
-------------------------- t2 --------------------
--------------------------7777777777---------------------------------
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
-- Начало транзакции A
BEGIN TRANSACTION
	SELECT * FROM AUDITORIUM_TYPE

	INSERT INTO AUDITORIUM_TYPE (AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
	VALUES ('А3', 'Аудитория 3')

	SELECT * FROM AUDITORIUM_TYPE

COMMIT TRANSACTION

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
-- Начало транзакции B
BEGIN TRANSACTION
	SELECT * FROM AUDITORIUM

	UPDATE AUDITORIUM
	SET AUDITORIUM_NAME = 'Новое название'
	WHERE AUDITORIUM = '206-1'

	SELECT * FROM AUDITORIUM

COMMIT TRANSACTION
--------------------------8888888888---------------------------------
BEGIN TRANSACTION;

    INSERT INTO STUDENT (NAME) VALUES ('Сергей Иванов');
    
    BEGIN TRANSACTION;
    
        INSERT INTO STUDENT (NAME) VALUES ('Анна Петрова');
        
        IF @@ERROR <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
    COMMIT;
    
    INSERT INTO STUDENT (NAME) VALUES ('Иван Сергеев');
    
COMMIT;