CREATE TABLE BMO_t(
    ID NUMBER (3) NOT NULL,
    STROK VARCHAR2 (50),
    PRIMARY KEY (ID)
);
INSERT INTO BMO_t (ID, STROK) VALUES (1, '������ 1');
INSERT INTO BMO_t (ID, STROK) VALUES (2, '������ 2');
INSERT INTO BMO_t (ID, STROK) VALUES (3, '������ 3');
COMMIT;

UPDATE BMO_t SET STROK = '���������� ������ 1' WHERE ID = 1;
UPDATE BMO_t SET STROK = '���������� ������ 2' WHERE ID = 2;
COMMIT;

SELECT * FROM BMO_t WHERE ID > 1;
SELECT COUNT(*) FROM BMO_t;
SELECT MAX(ID) FROM BMO_t;
SELECT MIN(ID) FROM BMO_t;

DELETE FROM BMO_t WHERE ID = 1;
ROLLBACK;
SELECT * FROM BMO_t;

CREATE TABLE BMO_t_CHILD(
    CHILD_ID NUMBER(3) NOT NULL,
    CHILD_DATA VARCHAR2(50),
    PARENT_ID NUMBER(3) NOT NULL,
    CONSTRAINT FK_PARENT_ID FOREIGN KEY (PARENT_ID) REFERENCES BMO_t (ID)
);

INSERT INTO BMO_t_CHILD (CHILD_ID, CHILD_DATA, PARENT_ID) VALUES (1, '������ 1', 1);
INSERT INTO BMO_t_CHILD (CHILD_ID, CHILD_DATA, PARENT_ID) VALUES (2, '������ 2', 2);
INSERT INTO BMO_t_CHILD (CHILD_ID, CHILD_DATA, PARENT_ID) VALUES (3, '������ 3', 3);

SELECT * FROM BMO_t
LEFT JOIN BMO_t_CHILD ON BMO_t.ID = BMO_t_CHILD.PARENT_ID;

SELECT * FROM BMO_t
INNER JOIN BMO_t_CHILD ON BMO_t.ID = BMO_t_CHILD.PARENT_ID;

DROP TABLE BMO_t_CHILD;

DROP TABLE BMO_t;
