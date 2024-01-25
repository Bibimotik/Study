USE UNIVER;

-- Оператор CREATE VIEW
CREATE VIEW [Преподаватель] AS
SELECT
  TEACHER AS код,
  TEACHER_NAME AS "имя преподавателя",
  GENDER AS пол,
  PULPIT AS "код кафедры"
FROM TEACHER;


CREATE VIEW [Количество_кафедр] AS
SELECT
  f.FACULTY AS факультет,
  COUNT(p.PULPIT) AS "количество кафедр" 
FROM FACULTY f
LEFT JOIN PULPIT p ON f.FACULTY = p.FACULTY
GROUP BY f.FACULTY;

CREATE VIEW [Аудитории] AS
SELECT
  AUDITORIUM AS код,
  AUDITORIUM_NAME AS "наименование аудитории"
FROM AUDITORIUM
WHERE AUDITORIUM_TYPE LIKE 'ЛК%' WITH CHECK OPTION;


CREATE VIEW [Дисциплины] AS
SELECT TOP 100 PERCENT
  SUBJECT AS код,
  SUBJECT_NAME AS "наименование дисциплины",
  PULPIT AS "код кафедры"
FROM SUBJECT
ORDER BY SUBJECT_NAME;


ALTER VIEW [Количество_кафедр]  WITH SCHEMABINDING AS
SELECT
  dbo.FACULTY.FACULTY AS факультет, 
  COUNT(dbo.PULPIT.PULPIT) AS "количество кафедр"
FROM dbo.FACULTY 
LEFT JOIN dbo.PULPIT ON dbo.FACULTY.FACULTY = dbo.PULPIT.FACULTY
GROUP BY dbo.FACULTY.FACULTY;


SELECT * FROM [Преподаватель]
SELECT * FROM [Аудитории]
SELECT * FROM [Дисциплины]
select * from [Количество_кафедр]


CREATE VIEW Расписание AS
SELECT *
FROM 
(
  SELECT g.GroupName, r.RoomName, s.SubjectName, t.TeacherName, tt.DayOfWeek, tt.Period
  FROM Timetable tt 
  JOIN Groupss g ON tt.GroupID = g.GroupID
  JOIN Rooms r ON tt.RoomID = r.RoomID
  JOIN Subjects s ON tt.SubjectID = s.SubjectID
  JOIN Teachers t ON tt.TeacherID = t.TeacherID
) src --исходный запрос
PIVOT
(
  MAX(SubjectName)
  FOR Period IN ([1], [2], [3])
) piv; --результат

select * from Расписание