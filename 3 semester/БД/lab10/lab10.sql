use B_MyBase
EXEC sp_helpindex 'GOODS';
EXEC sp_helpindex 'CUSTOMERS';
EXEC sp_helpindex 'ORDERS';

-- �������� ������� � ��������� ��� �������
CREATE NONCLUSTERED INDEX IX_GOODS_name_goods_price
ON GOODS (name_goods)
INCLUDE (price);

-- ���������� �������, ������� ���������� ������ � ���������
SELECT name_goods, price
FROM GOODS 
WHERE name_goods = '����';

-- �������� ������������ ������� ��� �������
CREATE NONCLUSTERED INDEX IX_ORDERS_name_of_goods
ON ORDERS (name_of_goods)
WHERE customer = '�������� �';

DROP INDEX IX_ORDERS_name_of_goods ON ORDERS

-- ���������� �������, ������� ���������� ����������� ������
SELECT *
FROM ORDERS
WHERE name_of_goods = '������' AND customer = '�������� �';