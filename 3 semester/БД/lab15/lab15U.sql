--1
drop trigger TR_AUDIT
create table TR_AUDIT
(
	ID int identity,
	ST varchar(20)
	check (ST in ('INS','DEL','UPD')),
	TRN varchar(50),
	C varchar(300)
)
	drop table TR_AUDIT
go
drop trigger TR_TEACHER_INS
go
create trigger TR_TEACHER_INS 
							on TEACHER after INSERT
as declare @a1 varchar(3),@a2 varchar(20), @a3 varchar(1),@a4 varchar(10), @in varchar(300);
print 'операция втавки';
set @a1 = (select [TEACHER] from inserted);
set @a2 = rtrim((select [TEACHER_NAME] from inserted));
set @a3 = rtrim((select [GENDER] from inserted));
set @a4 = rtrim((select [PULPIT] from inserted));
set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
insert into TR_AUDIT(ST,TRN,C) values ('INS','TR_TEACHER_INS',@in);
return;
insert into  TEACHER    (TEACHER,  TEACHER_NAME, GENDER, PULPIT )
                       values  ('ПМС1',     'Прохоров Михаил Арсентьевич', 'м', 'ИСиТ'); 
  select * from TR_AUDIT

  --2
  drop trigger TR_TEACHER_DEL
  go
  create trigger TR_TEACHER_DEL on TEACHER after DELETE
as declare @a1 varchar(3),@a2 varchar(20), @a3 varchar(1),@a4 varchar(10), @in varchar(300);
print 'операция удаления';
set @a1 = (select [TEACHER] from deleted);
set @a2 = rtrim((select [TEACHER_NAME] from deleted));
set @a3 = rtrim((select [GENDER] from deleted));
set @a4 = rtrim((select [PULPIT] from deleted));
set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
insert into TR_AUDIT(ST,TRN,C) values ('DEL','TR_TEACHER_DEL',@in);
return;
delete  from TEACHER where TEACHER like'ПМС1'
select * from TR_AUDIT
go

--3
drop trigger TR_TEACHER_UPD
go
create trigger TR_TEACHER_UPD on TEACHER after UPDATE
as declare @a1 varchar(3),@a2 varchar(20), @a3 varchar(1),@a4 varchar(10), @in varchar(700), @a5 varchar(3),@a6 varchar(20), @a7 varchar(1),@a8 varchar(10)
print 'операция обновления';
set @a1 = (select [TEACHER] from inserted);
set @a2 = rtrim((select [TEACHER_NAME] from inserted));
set @a3 = rtrim((select [GENDER] from inserted));
set @a4 = rtrim((select [PULPIT] from inserted));
set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
set @a5 = (select [TEACHER] from deleted);
set @a6 = rtrim((select [TEACHER_NAME] from deleted));
set @a7 = rtrim((select [GENDER] from deleted));
set @a8 = rtrim((select [PULPIT] from deleted));
set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 + ' ' + @a5 + ' ' + @a6 + ' ' + @a7 + ' '+ @a8 ;
insert into TR_AUDIT(ST,TRN,C) values ('UPD','TR_TEACHER_UPD',@in);
return;
update TEACHER set TEACHER_NAME = 'Сталин Василий Александрович' where TEACHER like 'БАНКЗВ'

select * from TR_AUDIT

select * from TEACHER 

go

--4
drop trigger TR_Teacher
go
create trigger TR_Teacher on Teacher after INSERT, DELETE, UPDATE  
as declare @a1 varchar(3),@a2 varchar(20), @a3 varchar(1),@a4 varchar(10), @in varchar(300);
declare @ins int = (select count(*) from inserted),
              @del int = (select count(*) from deleted); 
if  @ins > 0 and  @del = 0  
begin 
	print 'Событие: INSERT';
	set @a1 = (select [TEACHER] from inserted);
	set @a2 = (select [TEACHER_NAME] from inserted);
	set @a3 = (select [GENDER] from inserted);
	set @a4 = (select [PULPIT] from inserted);
	set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
	insert into TR_AUDIT(ST,TRN,C) values ('INS','TR_TEACHER_INS',@in);
end; 
else		  	 
if @ins = 0 and  @del > 0  
begin 
	print 'Событие: DELETE';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = ((select [PULPIT] from deleted));
	set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
	insert into TR_AUDIT(ST,TRN,C) values ('DEL','TR_TEACHER_DEL',@in);end; 
else	  
if @ins > 0 and  @del > 0  
begin 
	print 'Событие: UPDATE';
	set @a1 = (select [TEACHER] from inserted);
	set @a2 = ((select [TEACHER_NAME] from inserted));
	set @a3 = ((select [GENDER] from inserted));
	set @a4 = ((select [PULPIT] from inserted));
	set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = ((select [TEACHER_NAME] from deleted));
	set @a3 = ((select [GENDER] from deleted));
	set @a4 = ((select [PULPIT] from deleted));
	set @in = @a1 + ' ' + @a2 + ' ' + @a3 + ' '+ @a4 ;
	insert into TR_AUDIT(ST,TRN,C) values ('UPD','TR_TEACHER_UPD',@in);
end;  
return;  

insert into TEACHER values('ЛВИ','Ленин Владимир Ильич',  'м', 'ИСиТ');
update TEACHER set TEACHER.GENDER = 'ж' where TEACHER like 'ЛВИ'
delete  from TEACHER where TEACHER like'ЛВИ'
select * from TEACHER where TEACHER like 'ЛВИ'
select * from TR_AUDIT

--5

insert into TEACHER values('ПИО','Павел Ильич Осадчий',  'муж', 'ИСиТ');

select * from TEACHER where TEACHER like 'ПИО'
select * from TR_AUDIT
go
--6
drop trigger TR_TEACHER_UPDA
go
drop trigger TR_TEACHER_UPDB
go
drop trigger TR_TEACHER_UPDC
go
CREATE TRIGGER TR_TEACHER_UPDA ON TEACHER AFTER UPDATE  
AS 
BEGIN
    PRINT 'AUD_AFTER_UPDATE_A';
    RETURN;
END;
GO

CREATE TRIGGER TR_TEACHER_UPDB ON TEACHER AFTER UPDATE  
AS 
BEGIN
    PRINT 'AUD_AFTER_UPDATE_B';
    RETURN;
END;
GO

CREATE TRIGGER TR_TEACHER_UPDC ON TEACHER AFTER UPDATE  
AS 
BEGIN
    PRINT 'AUD_AFTER_UPDATE_C';
    RETURN;
END;
GO

SELECT t.name, e.type_desc 
FROM sys.triggers t 
JOIN sys.trigger_events e ON t.object_id = e.object_id  
WHERE OBJECT_NAME(t.parent_id) = 'TEACHER' 
AND e.type_desc = 'UPDATE';  

EXEC SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_UPDA', 
                        @order = 'Last', 
                        @stmttype = 'UPDATE';

EXEC SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_UPDC', 
                        @order = 'First', 
                        @stmttype = 'UPDATE';

--7
drop  trigger TR_PROGRESS 
select * from PROGRESS
go
create trigger TR_PROGRESS 
		on PROGRESS after INSERT,DELETE,UPDATE
		as declare @c int = (select sum(NOTE) from PROGRESS);
if (@c >9)
begin 
raiserror('Оценок в БГТУ выше 9 нет',10,1);
rollback;
end;
return;
update PROGRESS set NOTE = 10 where PROGRESS.IDSTUDENT = 1019

--8

go 
create trigger TR_NOT_DEL_FAC 
			on FACULTY instead of DELETE
			as raiserror('нельзя',10,1);
return;
delete from FACULTY where FACULTY_NAME = 'ТОВ';
go 
drop trigger TR_NOT_DEL_FAC
--9
drop trigger DDL_UNIVER;
go
create trigger DDL_UNIVER on database
		for DDL_DATABASE_LEVEL_EVENTS as
declare @t varchar(50) =  EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'varchar(50)');
  declare @t1 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'varchar(50)');
  declare @t2 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]', 'varchar(50)'); 
  if @t1 = 'TEACHER' 
  begin
        print 'Тип события: '+@t;
       print 'Имя объекта: '+@t1;
       print 'Тип объекта: '+@t2;
       raiserror( N'операции с таблицей TEACHER запрещены', 16, 1);  

       rollback;    
   end;

select * from TEACHER;

begin tran
alter table TEACHER drop column TEACHER_NAME

rollback;

