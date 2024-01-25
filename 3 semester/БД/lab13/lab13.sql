-- �������� �������� ��������� AddNewGood
USE B_MyBase;
GO

CREATE PROCEDURE AddNewGood
    @name_goods NVARCHAR(50),
    @price REAL,
    @description_of_goods NVARCHAR(100),
    @quantity_in_stock INT
AS
BEGIN
    INSERT INTO GOODS (name_goods, price, description_of_goods, quantity_in_stock)
    VALUES (@name_goods, @price, @description_of_goods, @quantity_in_stock)
END
GO
-- ����� �������� ��������� ��� ���������� ������ ������
EXEC AddNewGood '����� �����1', 15.99, '�������� ������ ������1', 10
drop procedure AddNewGood
go

CREATE PROCEDURE UpdateQuantityInStock
    @name_goods NVARCHAR(50),
    @new_quantity INT
AS
BEGIN
    UPDATE GOODS
    SET quantity_in_stock = @new_quantity
    WHERE name_goods = @name_goods
END
GO
-- ���������� ���������� �������
EXEC UpdateQuantityInStock '����', 20
drop procedure UpdateQuantityInStock
go

CREATE PROCEDURE GetOrdersByCustomer
    @customer NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM ORDERS
    WHERE customer = @customer
END
GO
-- ��������� ���������� � ������� ��� ������� '�������� �'
EXEC GetOrdersByCustomer '�������� �'
drop procedure GetOrdersByCustomer