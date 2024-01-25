create database TEST

use TEST

CREATE TABLE Groupss (
    GroupID INT PRIMARY KEY,
    GroupName VARCHAR(50)
);

CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY,
    RoomName VARCHAR(50)
);

CREATE TABLE Subjects (
    SubjectID INT PRIMARY KEY,
    SubjectName VARCHAR(50)
);

CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY,
    TeacherName VARCHAR(50)
);

CREATE TABLE Timetable (
    GroupID INT,
    RoomID INT,
    SubjectID INT,
    TeacherID INT,
    DayOfWeek VARCHAR(15),
    Period INT,
    FOREIGN KEY (GroupID) REFERENCES Groupss(GroupID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
);


INSERT INTO Groupss (GroupID, GroupName) VALUES
    (1, 'Group A'),
    (2, 'Group B'),
    (3, 'Group C');

INSERT INTO Rooms (RoomID, RoomName) VALUES
    (1, 'Room 101'),
    (2, 'Room 102'),
    (3, 'Room 103');

INSERT INTO Subjects (SubjectID, SubjectName) VALUES
    (1, 'Math'),
    (2, 'Physics'),
    (3, 'Chemistry');

INSERT INTO Teachers (TeacherID, TeacherName) VALUES
    (1, 'John Doe'),
    (2, 'Jane Smith'),
    (3, 'David Johnson');
  
  
INSERT INTO Timetable (GroupID, RoomID, SubjectID, TeacherID, DayOfWeek, Period) VALUES
	 (2, 2, 1, 1, 'Monday', 1),

  (2, 2, 3, 1, 'Monday', 3),

  (1, 1, 1, 2, 'Monday', 1),
  (1, 1, 2, 2, 'Monday', 2),
  (1, 1, 3, 2, 'Monday', 3),

  (3, 3, 1, 3, 'Monday', 1),
  (3, 3, 2, 3, 'Monday', 2),
  (3, 3, 3, 3, 'Monday', 3),

  (2, 2, 2, 2, 'Tuesday', 2);



SELECT RoomName
FROM Rooms
WHERE RoomID NOT IN (
    SELECT RoomID
    FROM Timetable
    WHERE DayOfWeek = 'Tuesday' 
);



SELECT DISTINCT t.TeacherID, t.TeacherName
FROM Teachers t
WHERE NOT EXISTS (
  SELECT 1
  FROM Timetable tt
  WHERE tt.TeacherID = t.TeacherID
    AND tt.DayOfWeek = 'Monday'
    AND tt.Period = 1
) OR NOT EXISTS (
  SELECT 1
  FROM Timetable tt
  WHERE tt.TeacherID = t.TeacherID
    AND tt.DayOfWeek = 'Monday' 
    AND tt.Period = 2  
) OR NOT EXISTS (
  SELECT 1
  FROM Timetable tt
  WHERE tt.TeacherID = t.TeacherID
    AND tt.DayOfWeek = 'Monday'
    AND tt.Period = 3
)


SELECT DISTINCT g.GroupID, g.GroupName
FROM Groupss g
WHERE NOT EXISTS (
  SELECT 1
  FROM Timetable t
  WHERE t.GroupID = g.GroupID
    AND t.DayOfWeek = 'Monday'
    AND t.Period = 1  
) OR NOT EXISTS (
  SELECT 1 
  FROM Timetable t
  WHERE t.GroupID = g.GroupID
    AND t.DayOfWeek = 'Monday'
    AND t.Period = 2
) OR NOT EXISTS (
  SELECT 1
  FROM Timetable t
  WHERE t.GroupID = g.GroupID 
    AND t.DayOfWeek = 'Monday'
    AND t.Period = 3
)