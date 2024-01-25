CREATE TRIGGER trg_insert_goods
ON GOODS
AFTER INSERT
AS
BEGIN
   UPDATE GOODS
    SET quantity_in_stock = quantity_in_stock + inserted.quantity_in_stock
    FROM GOODS INNER JOIN inserted ON GOODS.name_goods = inserted.name_goods;
END;

CREATE TRIGGER trg_update_goods
ON GOODS
AFTER UPDATE
AS
BEGIN
    IF EXISTS(SELECT * FROM inserted WHERE price > 15)
    BEGIN
        RAISERROR('Цена товара превышает 15.', 16, 1);
		rollback;
    END;
END;

update GOODS set price = 16 where name_goods like '%ваза%'
select * from GOODS

CREATE TRIGGER trg_delete_orders
ON ORDERS
AFTER DELETE
AS
BEGIN
    UPDATE GOODS
    SET quantity_in_stock = quantity_in_stock + deleted.quantity_of_ordered_goods
    FROM GOODS INNER JOIN deleted ON GOODS.name_goods = deleted.name_of_goods;
END;