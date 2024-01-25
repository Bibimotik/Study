use B_MyBase

SELECT ORDERS.customer AS Компания
FROM ORDERS
WHERE ORDERS.name_of_goods IN (
    SELECT GOODS.name_goods
    FROM GOODS
    WHERE GOODS.name_goods LIKE '%чашка%'
);

SELECT ORDERS.customer AS Компания
FROM ORDERS INNER JOIN 
(
    SELECT GOODS.name_goods
    FROM GOODS
    WHERE GOODS.name_goods LIKE '%чашка%'
) AS GOODS ON ORDERS.name_of_goods = GOODS.name_goods;

SELECT CUSTOMERS.name_company AS Компания
FROM ORDERS INNER JOIN GOODS 
ON ORDERS.name_of_goods = GOODS.name_goods
INNER JOIN CUSTOMERS 
ON ORDERS.customer = CUSTOMERS.name_company
WHERE GOODS.name_goods LIKE '%чашка%';

SELECT O.customer, O.number_order, O.quantity_of_ordered_goods
FROM ORDERS O
WHERE O.number_order IN (
    SELECT TOP 1 O2.number_order
    FROM ORDERS O2
    WHERE O2.customer = O.customer
    ORDER BY O2.quantity_of_ordered_goods DESC
)
ORDER BY O.quantity_of_ordered_goods DESC;

SELECT O.*
FROM Orders O
WHERE NOT EXISTS (
    SELECT 1
    FROM Customers C
    WHERE C.name_company = O.customer
);

SELECT
    (SELECT AVG(quantity_of_ordered_goods) FROM Orders WHERE name_of_goods = 'блюдце') AS avg_blyudtse,
    (SELECT AVG(quantity_of_ordered_goods) FROM Orders WHERE name_of_goods = 'стакан') AS avg_stakan,
    (SELECT AVG(quantity_of_ordered_goods) FROM Orders WHERE name_of_goods = 'ваза') AS avg_vaza;

SELECT name_goods
FROM GOODS
WHERE price > ALL (
    SELECT price
    FROM GOODS
    WHERE name_goods = 'чашка'
);

SELECT name_goods
FROM GOODS
WHERE price > ANY (
    SELECT price
    FROM GOODS
    WHERE name_goods = 'чашка'
);