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
	print '���������� ����� � ������� X: ' + cast( @c as varchar(2));
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
			where TEACHER = '����';
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
select * from TEACHER where TEACHER_NAME = '������ �������� �������������';
--------------------------3333333333---------------------------------
begin try
	begin tran;

	-- �������� ��� �������������
	UPDATE TEACHER
		set TEACHER_NAME = '������ �������� �������������'
		where TEACHER = '����';

	save tran SavePoint1;

	UPDATE TEACHER
		set GENDER = 'X'
		where TEACHER = '����';

	commit tran;
end try
begin catch
	-- ���� ��������� ������, ���������� ���������� �� ����������� �����
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
select * from TEACHER where TEACHER_NAME = '������ �������� �������������';
--------------------------4444444444---------------------------------
-- �������� A (READ UNCOMMITTED)
set transaction isolation level READ UNCOMMITTED

begin transaction 
-------------------------- t1 ------------------
	select @@SPID, 'select STUDENTS' 'operation', * FROM STUDENT where  NAME = '��������� ��������� ����������';

	-- UPDATE STUDENTS SET NAME = '�����' WHERE NAME = '����';

	select @@SPID, 'select STUDENTS' 'operation', * FROM STUDENT where  NAME = '��������� ��������� ����������';

commit
-------------------------- t2 -----------------
--- B --
begin transaction 

	select @@SPID 
	update  STUDENT set  NAME = '�����2' where NAME = '������� �������� ����������';
-------------------------- t1 --------------------
-------------------------- t2 --------------------
rollback;
select * from STUDENT
--------------------------5555555555---------------------------------
-- �������� A (READ COMMITTED)
set transaction isolation level READ COMMITTED

begin transaction 
	select count(*) FROM STUDENT where  NAME = '��������� ��������� ����������';
	-------------------------- t1 -----------------
	-------------------------- t2 -----------------
	select 'select STUDENTS' 'operation', count(*) FROM STUDENT where  NAME = '��������� ��������� ����������';

commit;

--- B --
begin transaction 
	-------------------------- t1 --------------------
	update  STUDENT set  NAME = '������� �������� ����������' where NAME = '������� �������� ����������';
commit;
-------------------------- t2 --------------------
select * from STUDENT
--------------------------6666666666---------------------------------
-- A ---
set transaction isolation level  REPEATABLE READ 
begin transaction 
	select NAME FROM STUDENT where NAME = '��������� ��������� ����������';
	-------------------------- t1 ------------------ 
	-------------------------- t2 -----------------
	select case
		when NAME = '������ ��������� �����������' then 'insert  STUDENT'  else ' ' 
end '���������', NAME from STUDENT  where NAME = '��������� ��������� ����������';
commit; 
--- B ---	
begin transaction 	  
-------------------------- t1 --------------------
	insert into STUDENT (IDGROUP,NAME, BDAY) values (18, '����� ������� ����������', '02.11.1995');
commit; 
-------------------------- t2 --------------------
--------------------------7777777777---------------------------------
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
-- ������ ���������� A
BEGIN TRANSACTION
	SELECT * FROM AUDITORIUM_TYPE

	INSERT INTO AUDITORIUM_TYPE (AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
	VALUES ('�3', '��������� 3')

	SELECT * FROM AUDITORIUM_TYPE

COMMIT TRANSACTION

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
-- ������ ���������� B
BEGIN TRANSACTION
	SELECT * FROM AUDITORIUM

	UPDATE AUDITORIUM
	SET AUDITORIUM_NAME = '����� ��������'
	WHERE AUDITORIUM = '206-1'

	SELECT * FROM AUDITORIUM

COMMIT TRANSACTION
--------------------------8888888888---------------------------------
BEGIN TRANSACTION;

    INSERT INTO STUDENT (NAME) VALUES ('������ ������');
    
    BEGIN TRANSACTION;
    
        INSERT INTO STUDENT (NAME) VALUES ('���� �������');
        
        IF @@ERROR <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
    COMMIT;
    
    INSERT INTO STUDENT (NAME) VALUES ('���� �������');
    
COMMIT;