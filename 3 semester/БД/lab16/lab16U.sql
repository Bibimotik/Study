--1
select * from [UNIVER].[dbo].[TEACHER]
		where [UNIVER].[dbo].[TEACHER].[PULPIT] = 'ИСиТ'
		for xml PATH('Кафедра'),
		root('Список_учителей'), elements;

--2
select AUD.AUDITORIUM_NAME [наименование_аудитории],AUD_T.AUDITORIUM_TYPE[наименование_типа_аудитории],AUDITORIUM_CAPACITY[вместимость]
from AUDITORIUM AUD join AUDITORIUM_TYPE AUD_T 
on AUD.AUDITORIUM_TYPE = AUD_T.AUDITORIUM_TYPE
where AUD.AUDITORIUM_TYPE like '%ЛК%'
order by AUD.AUDITORIUM_CAPACITY for xml AUTO,
root('Аудитория'),elements;


--3

select * from subject;
declare @h int= 0,
@x varchar(2000)='
	   <?xml version="1.0" encoding = "windows-1251" ?>
       <subjects> 
       <subj SUBJECT="DEN" SUBJECT_NAME="Delovoienglish" PULPIT="ИСиТ"/> 
	   <subj SUBJECT="TV" SUBJECT_NAME="Theoryver" PULPIT="ИСиТ"/> 
       </subjects>';
exec sp_xml_preparedocument @h output, @x; ;  -- подготовка документа 
insert SUBJECT select [SUBJECT], [SUBJECT_NAME], [PULPIT] 
                   from openxml(@h, '/subjects/subj', 0)     
       with([SUBJECT] nvarchar(20), [SUBJECT_NAME] varchar(30), [PULPIT] nvarchar(20))    

    select * from openxml(@h, '/subjects/subj', 0)
       with([SUBJECT] nvarchar(20), [SUBJECT_NAME] varchar(30), [PULPIT] nvarchar(20))    
    exec sp_xml_removedocument @h; -- удаление документа
	


--4
select * from STUDENT
insert STUDENT(IDGROUP, NAME, BDAY, INFO)
values(2, N'Банкузов Михаил Олегович', cast('2003-12-12' as date), 
N'<студент>
<паспорт серия="MP" номер="123456" дата = "30-08-2002">

</паспорт>
<телефон>7529137</телефон>

<адрес>
<страна>Беларусь</страна>
<город>Минск</город>  
<улица>Громово</улица>
<дом>19</дом>  
<квартира>19</квартира> 
</адрес>

</студент>')

update STUDENT set INFO = N'<студент>
<паспорт серия="MP" номер="123456" дата = "30-08-2002">

</паспорт>
<телефон>7529137</телефон>

<адрес>
<страна>Беларусь</страна>
<город>Минск</город>  
<улица>Громово</улица>
<дом>11</дом>  
<квартира>11</квартира> 
</адрес>

</студент>'
where IDSTUDENT = 1082--

select STUDENT.NAME,
	INFO.value(N'(/студент/адрес/дом)[1]',N'nvarchar(20)')[dom],

	INFO.query(N'/студент/адрес') [adres]
from STUDENT
where INFO is not null

delete from STUDENT where name like '%Банкузов%'


-- Task 5 
drop xml schema collection STUDENT;

use UNIVER
go
create xml schema collection STUDENT as 
N'<?xml version="1.0" encoding="utf-16" ?>
<xs:schema attributeFormDefault="unqualified" 
           elementFormDefault="qualified"
           xmlns:xs="http://www.w3.org/2001/XMLSchema">
       <xs:element name="студент">  
       <xs:complexType><xs:sequence>
       <xs:element name="паспорт" maxOccurs="1" minOccurs="1">
       <xs:complexType>
       <xs:element name="серия" type="xs:string" use="required" />
       <xs:element name="номер" type="xs:unsignedInt" use="required"/>
       <xs:element name="дата" type="xs:string" use="required" />  
       <xs:simpleType>  <xs:restriction base ="xs:string">
   <xs:pattern value="[0-9]{2}.[0-9]{2}.[0-9]{4}"/>
   </xs:restriction> 	</xs:simpleType>
 
   </xs:complexType> 
   </xs:element>
   <xs:element maxOccurs="3" name="телефон" type="xs:unsignedInt"/>
   <xs:element name="адрес">   <xs:complexType><xs:sequence>
   <xs:element name="страна" type="xs:string" />
   <xs:element name="город" type="xs:string" />
   <xs:element name="улица" type="xs:string" />
   <xs:element name="дом" type="xs:string" />
   <xs:element name="квартира" type="xs:string" />
   </xs:sequence></xs:complexType>  </xs:element>
   </xs:sequence></xs:complexType>
   </xs:element>
</xs:schema>';

alter table STUDENT ALTER COLUMN INFO xml(STUDENT)
insert STUDENT(IDGROUP , NAME, BDAY, INFO)
values(16, N'Тот Кто Остаётся', cast('30-08-2002' as date), 
N'<студент>
<паспорт серия="MP" номер="123456" дата = "30-08-2002">

</паспорт>
<телефон>7529137</телефон>

<адрес>
<страна>Беларусь</страна>
<город>Минск</город>  
<улица>Громово</улица>
<дом>11</дом>  
<квартира>11</квартира> 
</адрес>

</студент>')

select * from STUDENT where NAME = N'Тот Кто Остаётся'

--7

SELECT
 FACULTY.FACULTY as [@код] ,
    (
 SELECT count(PULPIT.PULPIT)  from PULPIT where FACULTY.FACULTY = PULPIT.FACULTY
   FOR XML PATH('кол-во_кафедр'), TYPE
 ),
    (
 SELECT PULPIT.PULPIT as [@код],
 
    (
 SELECT TEACHER.TEACHER as [@код], TEACHER.TEACHER_NAME  from TEACHER where  TEACHER.PULPIT = PULPIT.PULPIT
   FOR XML PATH('препод'), TYPE, ROOT('преподы')
 )
 
 from PULPIT where FACULTY.FACULTY = PULPIT.FACULTY
   FOR XML PATH('кафедра'),  TYPE, ROOT('кафедры')
 )
  from FACULTY --where FACULTY.FACULTY = 'ИТ'
FOR XML PATH('факультет'), ROOT('университет')