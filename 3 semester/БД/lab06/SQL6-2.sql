SELECT quantity_in_stock AS ����������,
AVG(GOODS.quantity_in_stock) AS �������,
MAX(GOODS.quantity_in_stock) AS ����,
MIN(GOODS.quantity_in_stock) AS ���,
SUM(GOODS.quantity_in_stock) AS �����,
COUNT(GOODS.quantity_in_stock) AS ����������
FROM GOODS
GROUP BY quantity_in_stock;

SELECT quantity_of_ordered_goods AS �����, COUNT(quantity_of_ordered_goods) AS ����������
FROM
  (SELECT CASE
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 1 AND 10 THEN '1-10'
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 10 AND 20 THEN '10-20'
      WHEN ORDERS.quantity_of_ordered_goods BETWEEN 20 AND 30 THEN '20-30'
      ELSE '>30'
    END AS quantity_of_ordered_goods FROM ORDERS) AS A
GROUP BY quantity_of_ordered_goods ORDER BY quantity_of_ordered_goods DESC;

SELECT name_goods AS ��������,
ROUND(AVG(CAST(GOODS.quantity_in_stock AS FLOAT(4))), 2) AS �������
FROM GOODS
GROUP BY name_goods
ORDER BY ������� DESC;

SELECT name_goods AS ��������,
ROUND(AVG(CAST(GOODS.quantity_in_stock AS FLOAT(4))), 2) AS �������
FROM GOODS
WHERE GOODS.price = 10.99
GROUP BY name_goods
ORDER BY ������� DESC;

SELECT C.name_company AS ��������, G.name_goods AS �����, 
AVG(O.quantity_of_ordered_goods) AS ��_����������
FROM ORDERS O
JOIN CUSTOMERS C ON O.customer = C.name_company
JOIN GOODS G ON O.name_of_goods = G.name_goods
WHERE O.customer = '�������� �'
GROUP BY C.name_company, G.name_goods;

SELECT MAX(quantity_in_stock) AS ����_�������, name_goods AS ��������, COUNT(name_goods) AS ����������
FROM GOODS
WHERE quantity_in_stock > 50 AND quantity_in_stock < 100
GROUP BY name_goods
HAVING MAX(quantity_in_stock) != 35
ORDER BY ����_������� DESC;