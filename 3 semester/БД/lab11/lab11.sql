use B_MyBase
DECLARE @name_goods NVARCHAR(50)
DECLARE @price REAL
DECLARE @description_of_goods NVARCHAR(100)
DECLARE @quantity_in_stock INT

DECLARE cursor_goods CURSOR FOR
SELECT name_goods, price, description_of_goods, quantity_in_stock
FROM GOODS

OPEN cursor_goods
	FETCH NEXT FROM cursor_goods INTO @name_goods, @price, @description_of_goods, @quantity_in_stock
	WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT 'Name: ' + @name_goods
		PRINT 'Price: ' + CAST(@price AS NVARCHAR(15))
		PRINT 'Description: ' + @description_of_goods
		PRINT 'Quantity in Stock: ' + CAST(@quantity_in_stock AS NVARCHAR(10))
		PRINT '--------------------------'

		FETCH NEXT FROM cursor_goods INTO @name_goods, @price, @description_of_goods, @quantity_in_stock
	END

CLOSE cursor_goods
DEALLOCATE cursor_goods