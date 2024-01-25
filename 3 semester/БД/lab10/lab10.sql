use B_MyBase
EXEC sp_helpindex 'GOODS';
EXEC sp_helpindex 'CUSTOMERS';
EXEC sp_helpindex 'ORDERS';

-- Создание индекса с покрытием для таблицы
CREATE NONCLUSTERED INDEX IX_GOODS_name_goods_price
ON GOODS (name_goods)
INCLUDE (price);

-- Выполнение запроса, который использует индекс с покрытием
SELECT name_goods, price
FROM GOODS 
WHERE name_goods = 'ваза';

-- Создание фильтруемого индекса для таблицы
CREATE NONCLUSTERED INDEX IX_ORDERS_name_of_goods
ON ORDERS (name_of_goods)
WHERE customer = 'Компания Б';

DROP INDEX IX_ORDERS_name_of_goods ON ORDERS

-- Выполнение запроса, который использует фильтруемый индекс
SELECT *
FROM ORDERS
WHERE name_of_goods = 'стакан' AND customer = 'Компания Б';