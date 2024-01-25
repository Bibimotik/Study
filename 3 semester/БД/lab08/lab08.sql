CREATE VIEW Товары AS
SELECT * FROM GOODS;

CREATE VIEW Товары_Заказы AS
SELECT G.*, O.number_order, O.date_of_order, O.quantity_of_ordered_goods
FROM GOODS G
JOIN ORDERS O ON G.name_goods = O.name_of_goods;

CREATE VIEW Ва AS
SELECT *
FROM ORDERS
WHERE name_of_goods LIKE 'ва%';

CREATE VIEW Порядок AS
SELECT TOP (100) PERCENT *
FROM ORDERS
ORDER BY quantity_of_ordered_goods ASC;


SELECT * FROM Товары
SELECT * FROM Товары_Заказы
SELECT * FROM Ва
SELECT * FROM Порядок