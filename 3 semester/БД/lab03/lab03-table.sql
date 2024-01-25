USE master;
GO

/*drop database B_MyBase*/

CREATE DATABASE B_MyBase
/*ON 
(
    NAME = B_MyBase_data,
    FILENAME = 'D:\�����\��\B_MyBase_data.mdf',
    SIZE = 100MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10MB
),
filegroup FG1
( name = N'MYBASE_fg1', filename = N'D:\�����\��\UNIVER_fgq-1.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
filegroup FG2
( name = N'MYBASE_fg2', filename = N'D:\�����\��\UNIVER_fgq-2.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%)
LOG ON
(
    NAME = B_MyBase_log,
    FILENAME = 'D:\�����\��\B_MyBase_log.ldf',
    SIZE = 50MB,
    MAXSIZE = 2GB,
    FILEGROWTH = 10MB
);*/
GO

USE B_MyBase;
GO

CREATE TABLE GOODS(
    name_goods NVARCHAR(50) PRIMARY KEY,
    price real,
    description_of_goods NVARCHAR(100),
  quantity_in_stock INT
)/*on FG1*/;

CREATE TABLE CUSTOMERS(
  name_company NVARCHAR(50) PRIMARY KEY,
  phone_number NVARCHAR(10),
  adress NVARCHAR(100),
)/* on FG2*/;

CREATE TABLE ORDERS(
  number_order INT PRIMARY KEY,
  name_of_goods NVARCHAR(50) foreign key references GOODS(name_goods),
  customer NVARCHAR(50) foreign key references CUSTOMERS(name_company),
  date_of_order DATE,
  quantity_of_ordered_goods INT
);
/*������������ ALTER*/
ALTER TABLE CUSTOMERS ADD ID int;

ALTER TABLE GOODS ALTER COLUMN quantity_in_stock INT NOT NULL;
ALTER TABLE ORDERS ALTER COLUMN quantity_of_ordered_goods INT NOT NULL;

ALTER TABLE CUSTOMERS DROP Column ID;


/*��������� �������*/
INSERT INTO GOODS (name_goods, price, description_of_goods, quantity_in_stock)
VALUES
    ('����', 10.99, '�������� ���� �� ������', 50),
	('������', 10.99, '�������� ���� �� ������', 90),
    ('������', 2.99, '������� ������ ��� ��������', 100),
    ('������', 5.99, '��������� ������ ��� ���', 20),
    ('�������', 8.99, '������� ������� ��� ���', 30),
    ('�����', 4.99, '������� ����� ��� ������� ��������', 40);
    
INSERT INTO CUSTOMERS (name_company, phone_number, adress)
VALUES
    ('�������� �', '1234567890', '��. ������, 1'),
    ('�������� �', '9876543210', '��. ������, 2'),
    ('�������� �', '1112223334', '��. ������, 3'),
    ('�������� �', '5556667778', '��. ���������, 4'),
    ('�������� �', '9990001112', '��. �����, 5');
    
INSERT INTO ORDERS (number_order, name_of_goods, customer, date_of_order, quantity_of_ordered_goods)
VALUES
    (1, '����', '�������� �', '2023-09-01', 3),
    (2, '������', '�������� �', '2023-09-02', 2),
    (3, '������', '�������� �', '2023-09-03', 1),
    (4, '�������', '�������� �', '2023-09-04', 4),
    (5, '�����', '�������� �', '2023-09-05', 5);

	INSERT INTO ORDERS (number_order, name_of_goods, customer, date_of_order, quantity_of_ordered_goods)
VALUES
	(6, '����', '�������� �', '2023-09-01', 20),
    (7, '������', '�������� �', '2023-09-02', 15),
    (8, '������', '�������� �', '2023-09-03', 10),
    (9, '�������', '�������� �', '2023-09-04', 45),
    (10, '�����', '�������� �', '2023-09-05', 23);