CREATE VIEW ������ AS
SELECT * FROM GOODS;

CREATE VIEW ������_������ AS
SELECT G.*, O.number_order, O.date_of_order, O.quantity_of_ordered_goods
FROM GOODS G
JOIN ORDERS O ON G.name_goods = O.name_of_goods;

CREATE VIEW �� AS
SELECT *
FROM ORDERS
WHERE name_of_goods LIKE '��%';

CREATE VIEW ������� AS
SELECT TOP (100) PERCENT *
FROM ORDERS
ORDER BY quantity_of_ordered_goods ASC;


SELECT * FROM ������
SELECT * FROM ������_������
SELECT * FROM ��
SELECT * FROM �������