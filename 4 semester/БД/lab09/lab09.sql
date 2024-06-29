--TASK 2 �������� ��������� �������, �������� � ��� ������
CREATE GLOBAL TEMPORARY TABLE temp_table22 (
    id NUMBER PRIMARY KEY,
    data VARCHAR2(100)
);

INSERT INTO temp_table22 VALUES (1010, 'Temp data');

SELECT * FROM temp_table22;

--TASK 3 �������� ������������������ S1 (SEQUENCE), �� ���������� ����������������
CREATE SEQUENCE s1 START WITH 1000 INCREMENT BY 10 NOMINVALUE NOMAXVALUE NOCYCLE NOCACHE NOORDER;
DROP SEQUENCE s1;
SELECT s1.NEXTVAL FROM dual;
SELECT s1.CURRVAL FROM dual;

--TASK 4 �������� ������������������ S2 (SEQUENCE), �� ���������� ����������������
CREATE SEQUENCE s2 START WITH 10 INCREMENT BY 10 MAXVALUE 100 NOCYCLE;
DROP SEQUENCE s2;
SELECT s2.NEXTVAL FROM dual;
SELECT s2.CURRVAL FROM dual;

SELECT * FROM USER_SEQUENCES;

--TASK 5 �������� ������������������ S3 (SEQUENCE), �� ���������� ����������������
CREATE SEQUENCE s3 START WITH 10 INCREMENT BY -10 MINVALUE -100 MAXVALUE 10 NOCYCLE ORDER;
DROP SEQUENCE s3;
SELECT s3.NEXTVAL FROM dual;
SELECT s3.CURRVAL FROM dual;

--TASK 6 �������� ������������������ S4 (SEQUENCE), �� ���������� ����������������
CREATE SEQUENCE s4 START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 10 CYCLE CACHE 5 NOORDER;
DROP SEQUENCE s4;
SELECT s4.NEXTVAL FROM dual;
SELECT s4.CURRVAL FROM dual;

--TASK 7 �������� ������ ���� ������������������� � ������� ���� ������, ���������� ������� �������� ������������ XXX
SELECT sequence_name FROM all_sequences WHERE sequence_owner = 'PDBSHKA';

--TASK 8 �������� ������� T1, ������� ������� N1, N2, N3, N4,
create table T1 (
        N1 NUMBER(20),
        N2 NUMBER(20),
        N3 NUMBER(20),
        N4 NUMBER(20))
        cache storage(buffer_pool keep);
        
BEGIN
    FOR i IN 1..7 LOOP
        INSERT INTO T1 (N1, N2, N3, N4) VALUES (S1.NEXTVAL, S2.NEXTVAL, S3.NEXTVAL, S4.NEXTVAL);
    END LOOP;
COMMIT;
END;

SELECT * FROM T1;

DROP TABLE T1;

--TASK 9 �������� ������� ABC, ������� hash-��� (������ 200) � ���������� 2 ����
CREATE CLUSTER ABC (
    X NUMBER(10),
    V VARCHAR2(12)
) SIZE 200 HASHKEYS 10000;

--TASK 10 �������� ������� A, ������� ������� XA
CREATE TABLE A (
    XA NUMBER(10),
    VA VARCHAR2(12),
    EXTRA_COLUMN VARCHAR2(50)
) CLUSTER ABC (XA, VA);

--TASK 11 �������� ������� B, ������� ������� XB
CREATE TABLE B (
    XB NUMBER(10),
    VB VARCHAR2(12),
    EXTRA_COLUMN VARCHAR2(50)
) CLUSTER ABC (XB, VB);

--TASK 12 �������� ������� �, ������� ������� X�
CREATE TABLE C (
    XC NUMBER(10),
    VC VARCHAR2(12),
    EXTRA_COLUMN VARCHAR2(50)
) CLUSTER ABC (XC, VC);

--TASK 13 ������� ��������� ������� � ������� � �������������� ������� Oracle.
SELECT * FROM USER_TABLES WHERE TABLE_NAME IN ('T1', 'A', 'B', 'C');
SELECT * FROM USER_CLUSTERS WHERE CLUSTER_NAME = 'ABC';

--TASK 14 �������� ������� ������� ��� ������� XXX.� � ����������������� ��� ����������
CREATE SYNONYM my_c_syn FOR PDBSHKA.C;
SELECT * FROM my_c_syn;

--TASK 15 �������� ��������� ������� ��� ������� XXX.B � ����������������� ��� ����������
CREATE PUBLIC SYNONYM public_b_syn FOR PDBSHKA.B;
SELECT * FROM public_b_syn;

--TASK 16 �������� ��� ������������ ������� A � B
CREATE TABLE A1 (
    id NUMBER PRIMARY KEY,
    data VARCHAR2(100)
);

INSERT INTO A1 VALUES (2, 'Data for table A');
INSERT INTO A1 VALUES (3, 'Data for table A');
INSERT INTO A1 VALUES (4, 'Data for table A');
INSERT INTO A1 VALUES (5, 'Data for table A');


CREATE TABLE B1 (
    id NUMBER,
    a_id NUMBER REFERENCES A1(id),
    data VARCHAR2(100)
);

INSERT INTO B1 VALUES (2, 1, 'Data for table B');
INSERT INTO B1 VALUES (3, 1, 'Data for table B');
INSERT INTO B1 VALUES (4, 1, 'Data for table B');
INSERT INTO B1 VALUES (6, 5, 'Data for table B');


SELECT A1.id, A1.data, B1.data
FROM A1
INNER JOIN B1 ON A1.id = B1.a_id;

SELECT * FROM A1;
SELECT * FROM B1;


CREATE VIEW V1 AS
SELECT A1.id, A1.data as data_a1, B1.data as data_b1
FROM A1
INNER JOIN B1 ON A1.id = B1.a_id;


SELECT * FROM V1;

--TASK 17 �� ������ ������ A � B �������� ����������������� ������������� MV_XXX
CREATE MATERIALIZED VIEW MV_PDBSHKA
REFRESH COMPLETE ON DEMAND
NEXT SYSDATE + NUMTODSINTERVAL(2, 'MINUTE')
AS SELECT A1.id, A1.data as a1_data, B1.data as b1_data
FROM A1
INNER JOIN B1 ON A1.id = B1.a_id;



DROP MATERIALIZED VIEW MV_PDBSHKA;
SELECT * FROM MV_PDBSHKA;











drop database link MIKHAL;

CREATE DATABASE LINK MIKHAIL
CONNECT TO pdbshka IDENTIFIED BY pdbshka
USING '(DESCRIPTION=
(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.10.3)(PORT=1521))
(CONNECT_DATA=(SERVICE_NAME=pdbshka)))';


CREATE TABLE MISHA (
    id NUMBER PRIMARY KEY,
    data VARCHAR2(100)
);

INSERT INTO MISHA (id, data) VALUES (11, '������');
INSERT INTO MISHA (id, data) VALUES (12, '����');
INSERT INTO MISHA (id, data) VALUES (13, '�����');
COMMIT;

select * from MISHA;

SHOW PDBS;