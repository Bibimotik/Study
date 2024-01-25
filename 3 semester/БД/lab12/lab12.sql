USE B_MyBase;
GO

BEGIN TRANSACTION;

BEGIN TRY
    UPDATE GOODS
    SET quantity_in_stock = quantity_in_stock - 10
    WHERE name_goods = 'ваза';

    INSERT INTO ORDERS (number_order, name_of_goods, customer, date_of_order, quantity_of_ordered_goods)
    VALUES (11, 'стакан', 'Компания А', '2023-12-17', 5);

    DELETE FROM CUSTOMERS
    WHERE name_company = 'Компания Г';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
END CATCH;