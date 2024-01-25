USE UNIVER;

-- �������� CREATE VIEW
CREATE VIEW [�������������] AS
SELECT
  TEACHER AS ���,
  TEACHER_NAME AS "��� �������������",
  GENDER AS ���,
  PULPIT AS "��� �������"
FROM TEACHER;


CREATE VIEW [����������_������] AS
SELECT
  f.FACULTY AS ���������,
  COUNT(p.PULPIT) AS "���������� ������" 
FROM FACULTY f
LEFT JOIN PULPIT p ON f.FACULTY = p.FACULTY
GROUP BY f.FACULTY;

CREATE VIEW [���������] AS
SELECT
  AUDITORIUM AS ���,
  AUDITORIUM_NAME AS "������������ ���������"
FROM AUDITORIUM
WHERE AUDITORIUM_TYPE LIKE '��%' WITH CHECK OPTION;


CREATE VIEW [����������] AS
SELECT TOP 100 PERCENT
  SUBJECT AS ���,
  SUBJECT_NAME AS "������������ ����������",
  PULPIT AS "��� �������"
FROM SUBJECT
ORDER BY SUBJECT_NAME;


ALTER VIEW [����������_������]  WITH SCHEMABINDING AS
SELECT
  dbo.FACULTY.FACULTY AS ���������, 
  COUNT(dbo.PULPIT.PULPIT) AS "���������� ������"
FROM dbo.FACULTY 
LEFT JOIN dbo.PULPIT ON dbo.FACULTY.FACULTY = dbo.PULPIT.FACULTY
GROUP BY dbo.FACULTY.FACULTY;


SELECT * FROM [�������������]
SELECT * FROM [���������]
SELECT * FROM [����������]
select * from [����������_������]


CREATE VIEW ���������� AS
SELECT *
FROM 
(
  SELECT g.GroupName, r.RoomName, s.SubjectName, t.TeacherName, tt.DayOfWeek, tt.Period
  FROM Timetable tt 
  JOIN Groupss g ON tt.GroupID = g.GroupID
  JOIN Rooms r ON tt.RoomID = r.RoomID
  JOIN Subjects s ON tt.SubjectID = s.SubjectID
  JOIN Teachers t ON tt.TeacherID = t.TeacherID
) src --�������� ������
PIVOT
(
  MAX(SubjectName)
  FOR Period IN ([1], [2], [3])
) piv; --���������

select * from ����������