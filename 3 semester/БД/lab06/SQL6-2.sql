SELECT quantity_in_stock AS ÊÎËÈ×ÅÑÒÂÎ,
AVG(GOODS.quantity_in_stock) AS ÑÐÅÄÍÅÅ,
MAX(GOODS.quantity_in_stock) AS ÌÀÊÑ,
MIN(GOODS.quantity_in_stock) AS ÌÈÍ,
SUM(GOODS.quantity_in_stock) AS ÑÓÌÌÀ,
COUNT(GOODS.quantity_in_stock) AS ÊÎËÈ×ÅÑÒÂÎ
FROM GOODS
GROUP BY quantity_in_stock;

SELECT quantity_of_ordered_goods AS ÇÀÊÀÇ, COUNT(quantity_of_ordered_goods) AS ÊÎËÈ×ÅÑÒÂÎ
FROM
  (SELECT CASE
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 1 AND 10 THEN '1-10'
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 10 AND 20 THEN '10-20'
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 20 AND 30 THEN '20-30'
      ELSE '>30'
    END AS quantity_of_ordered_goods FROM ORDERS) AS A
GROUP BY quantity_of_ordered_goods ORDER BY quantity_of_ordered_goods DESC;

SELECT name_goods AS ÍÀÇÂÀÍÈÅ,
ROUND(AVG(CAST(GOODS.quantity_in_stock AS FLOAT(4))), 2) AS ÎÑÒÀÒÎÊ
FROM GOODS
GROUP BY name_goods
ORDER BY ÎÑÒÀÒÎÊ DESC;

SELECT name_goods AS ÍÀÇÂÀÍÈÅ,
ROUND(AVG(CAST(GOODS.quantity_in_stock AS FLOAT(4))), 2) AS ÎÑÒÀÒÎÊ
FROM GOODS
WHERE GOODS.price = 10.99
GROUP BY name_goods
ORDER BY ÎÑÒÀÒÎÊ DESC;

SELECT C.name_company AS ÇÀÊÀÇ×ÈÊ, G.name_goods AS ÒÎÂÀÐ, 
AVG(O.quantity_of_ordered_goods) AS ÑÐ_ÊÎËÈ×ÅÑÒÂÎ
FROM ORDERS O
JOIN CUSTOMERS C ON O.customer = C.name_company
JOIN GOODS G ON O.name_of_goods = G.name_goods
WHERE O.customer = 'Êîìïàíèÿ À'
GROUP BY C.name_company, G.name_goods;

SELECT MAX(quantity_in_stock) AS ÌÀÊÑ_ÎÑÒÀÒÎÊ, name_goods AS ÍÀÇÂÀÍÈÅ, COUNT(name_goods) AS ÊÎËÈ×ÅÑÒÂÎ
FROM GOODS
WHERE quantity_in_stock > 50 AND quantity_in_stock < 100
GROUP BY name_goods
HAVING MAX(quantity_in_stock) != 35
ORDER BY ÌÀÊÑ_ÎÑÒÀÒÎÊ DESC;