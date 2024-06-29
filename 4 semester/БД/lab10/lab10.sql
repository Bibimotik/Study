--TASK 1 Создайте таблицу T_RANGE c диапазонным секционированием. Используйте ключ секционирования типа NUMBER.
CREATE TABLE T_RANGE (
    id NUMBER,
    data VARCHAR2(50)
)
PARTITION BY RANGE (id) (
    PARTITION p1 VALUES LESS THAN (100),
    PARTITION p2 VALUES LESS THAN (200),
    PARTITION p3 VALUES LESS THAN (MAXVALUE)
);

DROP TABLE T_RANGE;

--TASK 2 Создайте таблицу T_INTERVAL c интервальным секционированием. Используйте ключ секционирования типа DATE.
CREATE TABLE T_INTERVAL (
    sale_date DATE,
    amount NUMBER
)
PARTITION BY RANGE (sale_date) 
INTERVAL (NUMTOYMINTERVAL(1, 'MONTH')) (
    PARTITION p1 VALUES LESS THAN (TO_DATE('23-04-2024', 'DD-MM-YYYY'))
);

DROP TABLE T_INTERVAL;

--TASK 3 Создайте таблицу T_HASH c хэш-секционированием. Используйте ключ секционирования типа VARCHAR2.
CREATE TABLE T_HASH (
    name VARCHAR2(50),
    address VARCHAR2(100)
)
PARTITION BY HASH (name) 
PARTITIONS 4;

DROP TABLE T_HASH;

--TASK 4 Создайте таблицу T_LIST со списочным секционированием. Используйте ключ секционирования типа CHAR.
CREATE TABLE T_LIST (
    region CHAR(1),
    sales NUMBER
)
PARTITION BY LIST (region) (
    PARTITION p1 VALUES ('A'),
    PARTITION p2 VALUES ('B'),
    PARTITION p3 VALUES ('C'),
    PARTITION p4 VALUES (DEFAULT)
);

DROP TABLE T_LIST;


--TASK 5 Введите с помощью операторов INSERT данные в таблицы T_RANGE, T_INTERVAL, T_HASH, T_LIST. Данные должны быть такими, чтобы они разместились по всем секциям. Продемонстрируйте это с помощью SELECT запроса.
INSERT INTO T_RANGE (id, data) VALUES (50, 'Less than 100');
INSERT INTO T_RANGE (id, data) VALUES (150, 'Between 100 and 200');
INSERT INTO T_RANGE (id, data) VALUES (250, 'Greater than 200');

SELECT * FROM T_RANGE PARTITION (p1);
SELECT * FROM T_RANGE PARTITION (p2);
SELECT * FROM T_RANGE PARTITION (p3);


INSERT INTO T_INTERVAL (sale_date, amount) VALUES (TO_DATE('15-05-2024', 'DD-MM-YYYY'), 1000);
INSERT INTO T_INTERVAL (sale_date, amount) VALUES (TO_DATE('16-04-2024', 'DD-MM-YYYY'), 1000);
INSERT INTO T_INTERVAL (sale_date, amount) VALUES (TO_DATE('16-04-2024', 'DD-MM-YYYY'), 1000);
INSERT INTO T_INTERVAL (sale_date, amount) VALUES (TO_DATE('15-04-2024', 'DD-MM-YYYY'), 1500);
INSERT INTO T_INTERVAL (sale_date, amount) VALUES (TO_DATE('15-04-2024', 'DD-MM-YYYY'), 2000);

SELECT * FROM T_INTERVAL PARTITION (p1); 

INSERT INTO T_HASH (name, address) VALUES ('John', 'Address 1');
INSERT INTO T_HASH (name, address) VALUES ('Michael', 'Address 2');
INSERT INTO T_HASH (name, address) VALUES ('Sara', 'Address 3');
INSERT INTO T_HASH (name, address) VALUES ('Alexey', 'Address 4');
INSERT INTO T_HASH (name, address) VALUES ('Hregoriy', 'Address 5');

SELECT * FROM T_HASH;


INSERT INTO T_LIST (region, sales) VALUES ('A', 10000);
INSERT INTO T_LIST (region, sales) VALUES ('B', 15000);
INSERT INTO T_LIST (region, sales) VALUES ('C', 20000);
INSERT INTO T_LIST (region, sales) VALUES ('D', 25000);

SELECT * FROM T_LIST PARTITION (p1);
SELECT * FROM T_LIST PARTITION (p2);
SELECT * FROM T_LIST PARTITION (p3);
SELECT * FROM T_LIST PARTITION (p4);

--TASK 6(только T_RANGE) Продемонстрируйте для всех таблиц процесс перемещения строк между секциями, при изменении (оператор UPDATE) ключа секционирования.
UPDATE T_RANGE SET id = 60 WHERE id = 50;

--TASK 7 Для одной из таблиц продемонстрируйте действие оператора ALTER TABLE MERGE.
ALTER TABLE T_RANGE 
MERGE PARTITIONS p1, p2 INTO PARTITION p12;

SELECT * FROM T_RANGE PARTITION (p1);
SELECT * FROM T_RANGE PARTITION (p12);

--TASK 8 Для одной из таблиц продемонстрируйте действие оператора ALTER TABLE SPLIT.
ALTER TABLE T_RANGE 
SPLIT PARTITION p12 AT (150) INTO (PARTITION p1, PARTITION p2);

SELECT * FROM T_RANGE PARTITION (p1);
SELECT * FROM T_RANGE PARTITION (p2);

--TASK 9 Для одной из таблиц продемонстрируйте действие оператора ALTER TABLE EXCHANGE.
CREATE TABLE T_TEMP AS SELECT * FROM T_RANGE PARTITION (p1);

DELETE FROM T_RANGE PARTITION (p1);
SELECT * FROM T_RANGE PARTITION (p1);

ALTER TABLE T_RANGE
EXCHANGE PARTITION p1 WITH TABLE T_TEMP;
SELECT * FROM T_RANGE PARTITION (p1);

DROP TABLE T_TEMP;

--TASK 10 10.Выведите при помощи SELECT запросов:
--список всех секционированных таблиц;
--список всех секций какой-либо таблицы;
--список всех значений из какой-либо секции по имени секции;
--список всех значений из какой-либо секции по ссылке.
SELECT DISTINCT table_name
FROM user_tab_partitions;

SELECT partition_name
FROM user_tab_partitions
WHERE table_name = 'T_RANGE';

SELECT * 
FROM T_RANGE PARTITION (p1);

SELECT * FROM T_RANGE PARTITION FOR (300);

