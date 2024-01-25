CREATE FUNCTION dbo.GetTotalGoodsCount()
RETURNS INT
AS
BEGIN
    DECLARE @TotalCount INT;
    
    SELECT @TotalCount = SUM(quantity_in_stock)
    FROM GOODS;
    
    RETURN @TotalCount;
END;
GO

-- Использование функции скалярного значения
SELECT dbo.GetTotalGoodsCount() AS 'TotalGoodsCount';
drop function dbo.GetTotalGoodsCount
GO

CREATE FUNCTION dbo.GetOrdersByCustomer(@customer NVARCHAR(50))
RETURNS TABLE
AS
RETURN (
    SELECT *
    FROM ORDERS
    WHERE customer = @customer
);
GO

-- Использование табличной функции
SELECT * FROM dbo.GetOrdersByCustomer('Компания А');
drop function dbo.GetOrdersByCustomer
GO

CREATE FUNCTION dbo.GetGoodsByPriceRange(@minPrice REAL, @maxPrice REAL)
RETURNS TABLE
AS
RETURN (
    SELECT *
    FROM GOODS
    WHERE price BETWEEN @minPrice AND @maxPrice
);
GO

-- Использование функции с параметрами
SELECT * FROM dbo.GetGoodsByPriceRange(5.00, 10.00);
drop function dbo.GetGoodsByPriceRange
GO

CREATE FUNCTION dbo.GetTotalOrderedGoodsQuantity()
RETURNS INT
AS
BEGIN
    DECLARE @TotalQuantity INT;
    
    SELECT @TotalQuantity = SUM(quantity_of_ordered_goods)
    FROM ORDERS;
    
    RETURN @TotalQuantity;
END;
GO

-- Использование агрегатной функции
SELECT dbo.GetTotalOrderedGoodsQuantity() AS 'TotalOrderedGoodsQuantity';
drop function dbo.GetTotalOrderedGoodsQuantity