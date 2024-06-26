CREATE TABLE Student (
    StudentId SERIAL PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Age INT,
    Specialty VARCHAR(50),
    Course INT,
    GroupName VARCHAR(20),
    AverageGrade DECIMAL(4,2),
    Gender VARCHAR(10),
    AddressId INT,
    Image BYTEA
);

CREATE TABLE Address (
    AddressId SERIAL PRIMARY KEY,
    City VARCHAR(50),
    PostalCode VARCHAR(10),
    Street VARCHAR(50),
    HouseNumber VARCHAR(10),
    ApartmentNumber VARCHAR(10)
);

-- Создание внешнего ключа для связи между таблицами
ALTER TABLE Student
ADD CONSTRAINT FK_Student_Address
FOREIGN KEY (AddressId)
REFERENCES Address(AddressId);

-- Создание триггера на таблице "Student" для проверки возраста
CREATE OR REPLACE FUNCTION CheckAgeFunction()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.Age < 18 THEN
        RAISE EXCEPTION 'Студент не может быть младше 18 лет.';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER CheckAgeTrigger
BEFORE INSERT OR UPDATE ON Student
FOR EACH ROW
EXECUTE FUNCTION CheckAgeFunction();

-- Создание хранимой процедуры для получения списка студентов
CREATE OR REPLACE FUNCTION GetStudents()
RETURNS SETOF Student AS $$
BEGIN
    RETURN QUERY SELECT * FROM Student;
END;
$$ LANGUAGE plpgsql;

-- Заполнение таблицы "Address"
INSERT INTO Address (City, PostalCode, Street, HouseNumber, ApartmentNumber)
VALUES
    ('Москва', '123456', 'Улица Пушкина', '10', '5'),
    ('Санкт-Петербург', '654321', 'Улица Лермонтова', '15', '7'),
    ('Киев', '987654', 'Улица Гоголя', '20', '3');

-- Заполнение таблицы "Student"
INSERT INTO Student (FullName, Age, Specialty, Course, GroupName, AverageGrade, Gender, AddressId, Image)
VALUES
    ('Иванов Иван Иванович', 20, 'Информатика', 3, 'Группа 1', 4.5, 'М', 1, null),
    ('Петрова Елена Сергеевна', 22, 'Экономика', 4, 'Группа 2', 4.7, 'Ж', 2, null),
    ('Сидоров Алексей Петрович', 19, 'Физика', 2, 'Группа 1', 4.2, 'М', 1, null);

CREATE OR REPLACE PROCEDURE add_student(
  p_full_name VARCHAR(100),
  p_age INT,
  p_specialty VARCHAR(50),
  p_course INT,
  p_group_name VARCHAR(20),
  p_average_grade DECIMAL(4,2),
  p_gender VARCHAR(10),
  p_address_id INT,
    p_image BYTEA
)
AS $$
BEGIN
  INSERT INTO Student (FullName, Age, Specialty, Course, GroupName, AverageGrade, Gender, AddressId, Image)
  VALUES (p_full_name, p_age, p_specialty, p_course, p_group_name, p_average_grade, p_gender, p_address_id, p_image);
END;
$$ LANGUAGE plpgsql;