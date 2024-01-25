use B_MyBase
SELECT
    name_of_goods,
    customer,
    date_of_order,
    SUM(quantity_of_ordered_goods) AS total_quantity
FROM
    ORDERS
GROUP BY
    ROLLUP(name_of_goods, customer, date_of_order)

SELECT
    name_of_goods,
    customer,
    date_of_order,
    SUM(quantity_of_ordered_goods) AS total_quantity
FROM
    ORDERS
GROUP BY
    CUBE(name_of_goods, customer, date_of_order)

SELECT customer, 'order customer' AS category
FROM ORDERS
UNION
SELECT name_company AS item_name, 'CUSTOMERS' AS category
FROM CUSTOMERS;

SELECT customer, 'order customer' AS category
FROM ORDERS
UNION ALL
SELECT name_company AS item_name, 'CUSTOMERS' AS category
FROM CUSTOMERS;

SELECT customer, 'order customer' AS category
FROM ORDERS
INTERSECT
SELECT name_company AS item_name, 'CUSTOMERS' AS category
FROM CUSTOMERS;

SELECT customer, 'order customer' AS category
FROM ORDERS
EXCEPT
SELECT name_company AS item_name, 'CUSTOMERS' AS category
FROM CUSTOMERS;