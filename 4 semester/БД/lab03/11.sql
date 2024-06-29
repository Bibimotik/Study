create table test_table(
    ID number not null,
    NAME VARCHAR2(30),
    primary key (ID)
);

INSERT INTO test_table (ID, NAME) VALUES (1, 'тест 1');
INSERT INTO test_table (ID, NAME) VALUES (2, 'тест 2');
INSERT INTO test_table (ID, NAME) VALUES (3, 'тест 3'); 
--BMO_PDB
SELECT owner, object_name, object_type
FROM all_objects
WHERE owner IN ('c##cdb_admin', 'U1_BMO_PDB')
ORDER BY owner, object_type, object_name;