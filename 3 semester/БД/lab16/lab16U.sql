--1
select * from [UNIVER].[dbo].[TEACHER]
		where [UNIVER].[dbo].[TEACHER].[PULPIT] = '����'
		for xml PATH('�������'),
		root('������_��������'), elements;

--2
select AUD.AUDITORIUM_NAME [������������_���������],AUD_T.AUDITORIUM_TYPE[������������_����_���������],AUDITORIUM_CAPACITY[�����������]
from AUDITORIUM AUD join AUDITORIUM_TYPE AUD_T 
on AUD.AUDITORIUM_TYPE = AUD_T.AUDITORIUM_TYPE
where AUD.AUDITORIUM_TYPE like '%��%'
order by AUD.AUDITORIUM_CAPACITY for xml AUTO,
root('���������'),elements;


--3

select * from subject;
declare @h int= 0,
@x varchar(2000)='
	   <?xml version="1.0" encoding = "windows-1251" ?>
       <subjects> 
       <subj SUBJECT="DEN" SUBJECT_NAME="Delovoienglish" PULPIT="����"/> 
	   <subj SUBJECT="TV" SUBJECT_NAME="Theoryver" PULPIT="����"/> 
       </subjects>';
exec sp_xml_preparedocument @h output, @x; ;  -- ���������� ��������� 
insert SUBJECT select [SUBJECT], [SUBJECT_NAME], [PULPIT] 
                   from openxml(@h, '/subjects/subj', 0)     
       with([SUBJECT] nvarchar(20), [SUBJECT_NAME] varchar(30), [PULPIT] nvarchar(20))    

    select * from openxml(@h, '/subjects/subj', 0)
       with([SUBJECT] nvarchar(20), [SUBJECT_NAME] varchar(30), [PULPIT] nvarchar(20))    
    exec sp_xml_removedocument @h; -- �������� ���������
	


--4
select * from STUDENT
insert STUDENT(IDGROUP, NAME, BDAY, INFO)
values(2, N'�������� ������ ��������', cast('2003-12-12' as date), 
N'<�������>
<������� �����="MP" �����="123456" ���� = "30-08-2002">

</�������>
<�������>7529137</�������>

<�����>
<������>��������</������>
<�����>�����</�����>  
<�����>�������</�����>
<���>19</���>  
<��������>19</��������> 
</�����>

</�������>')

update STUDENT set INFO = N'<�������>
<������� �����="MP" �����="123456" ���� = "30-08-2002">

</�������>
<�������>7529137</�������>

<�����>
<������>��������</������>
<�����>�����</�����>  
<�����>�������</�����>
<���>11</���>  
<��������>11</��������> 
</�����>

</�������>'
where IDSTUDENT = 1082--

select STUDENT.NAME,
	INFO.value(N'(/�������/�����/���)[1]',N'nvarchar(20)')[dom],

	INFO.query(N'/�������/�����') [adres]
from STUDENT
where INFO is not null

delete from STUDENT where name like '%��������%'


-- Task 5 
drop xml schema collection STUDENT;

use UNIVER
go
create xml schema collection STUDENT as 
N'<?xml version="1.0" encoding="utf-16" ?>
<xs:schema attributeFormDefault="unqualified" 
           elementFormDefault="qualified"
           xmlns:xs="http://www.w3.org/2001/XMLSchema">
       <xs:element name="�������">  
       <xs:complexType><xs:sequence>
       <xs:element name="�������" maxOccurs="1" minOccurs="1">
       <xs:complexType>
       <xs:element name="�����" type="xs:string" use="required" />
       <xs:element name="�����" type="xs:unsignedInt" use="required"/>
       <xs:element name="����" type="xs:string" use="required" />  
       <xs:simpleType>  <xs:restriction base ="xs:string">
   <xs:pattern value="[0-9]{2}.[0-9]{2}.[0-9]{4}"/>
   </xs:restriction> 	</xs:simpleType>
 
   </xs:complexType> 
   </xs:element>
   <xs:element maxOccurs="3" name="�������" type="xs:unsignedInt"/>
   <xs:element name="�����">   <xs:complexType><xs:sequence>
   <xs:element name="������" type="xs:string" />
   <xs:element name="�����" type="xs:string" />
   <xs:element name="�����" type="xs:string" />
   <xs:element name="���" type="xs:string" />
   <xs:element name="��������" type="xs:string" />
   </xs:sequence></xs:complexType>  </xs:element>
   </xs:sequence></xs:complexType>
   </xs:element>
</xs:schema>';

alter table STUDENT ALTER COLUMN INFO xml(STUDENT)
insert STUDENT(IDGROUP , NAME, BDAY, INFO)
values(16, N'��� ��� �������', cast('30-08-2002' as date), 
N'<�������>
<������� �����="MP" �����="123456" ���� = "30-08-2002">

</�������>
<�������>7529137</�������>

<�����>
<������>��������</������>
<�����>�����</�����>  
<�����>�������</�����>
<���>11</���>  
<��������>11</��������> 
</�����>

</�������>')

select * from STUDENT where NAME = N'��� ��� �������'

--7

SELECT
 FACULTY.FACULTY as [@���] ,
    (
 SELECT count(PULPIT.PULPIT)  from PULPIT where FACULTY.FACULTY = PULPIT.FACULTY
   FOR XML PATH('���-��_������'), TYPE
 ),
    (
 SELECT PULPIT.PULPIT as [@���],
 
    (
 SELECT TEACHER.TEACHER as [@���], TEACHER.TEACHER_NAME  from TEACHER where  TEACHER.PULPIT = PULPIT.PULPIT
   FOR XML PATH('������'), TYPE, ROOT('�������')
 )
 
 from PULPIT where FACULTY.FACULTY = PULPIT.FACULTY
   FOR XML PATH('�������'),  TYPE, ROOT('�������')
 )
  from FACULTY --where FACULTY.FACULTY = '��'
FOR XML PATH('���������'), ROOT('�����������')