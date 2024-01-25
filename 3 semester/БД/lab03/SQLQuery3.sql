use B_MyBase
SELECT GOODS.name_goods, GOODS.description_of_goods
FROM GOODS;

SELECT GOODS.name_goods, GOODS.description_of_goods
FROM GOODS
WHERE GOODS.name_goods LIKE '%ваза%';

SELECT CUSTOMERS.name_company, ORDERS.name_of_goods, ORDERS.quantity_of_ordered_goods
FROM CUSTOMERS
INNER JOIN ORDERS ON CUSTOMERS.name_company = ORDERS.customer;

SELECT CUSTOMERS.name_company, ORDERS.name_of_goods, ORDERS.quantity_of_ordered_goods,
CASE
    WHEN ORDERS.quantity_of_ordered_goods = 1 THEN 'один'
    WHEN ORDERS.quantity_of_ordered_goods = 2 THEN 'два'
    WHEN ORDERS.quantity_of_ordered_goods = 3 THEN 'три'
END AS Количество
FROM CUSTOMERS
INNER JOIN ORDERS ON CUSTOMERS.name_company = ORDERS.customer
WHERE ORDERS.quantity_of_ordered_goods BETWEEN 1 AND 3
ORDER BY ORDERS.quantity_of_ordered_goods DESC;


SELECT GOODS.name_goods, GOODS.description_of_goods
FROM GOODS
CROSS JOIN CUSTOMERS;

SELECT GOODS.name_goods, GOODS.description_of_goods
FROM GOODS, CUSTOMERS;