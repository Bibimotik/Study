CREATE TABLE PNA_T1(
    ID NUMBER NOT NULL,
    STROK VARCHAR (30),
    PRIMARY KEY (ID)
);
INSERT INTO PNA_T1 (ID, STROK) VALUES (1, 'Строка 1');
INSERT INTO PNA_T1 (ID, STROK) VALUES (2, 'Строка 2');
INSERT INTO PNA_T1 (ID, STROK) VALUES (3, 'Строка 3');

-- 6. Удалите (DROP) таблицу XXX_T1.
DROP TABLE PNA_T1;
-- Выполните SELECT-запрос к представлению USER_RECYCLEBIN, поясните результат.
SELECT * FROM USER_RECYCLEBIN;
-- 8. Восстановите (FLASHBACK) удаленную таблицу.
FLASHBACK TABLE PNA_T1 TO BEFORE DROP;
-- 9. Выполните PL/SQL-скрипт, заполняющий таблицу XXX_T1 данными (10000 строк).
begin
    FOR loopIndex IN 0..9999
        LOOP
            INSERT INTO PNA_T1 VALUES (loopIndex + 20, 'STROK');
        END LOOP;
    COMMIT;
end;

SELECT COUNT(*) FROM PNA_T1;
-- 12. Исследуйте значения псевдостолбца RowId в таблице XXX_T1 и других таблицах. Поясните формат и использование RowId.
-- 13. Исследуйте значения псевдостолбца RowSCN в таблице XXX_T1 и других таблицах.
SELECT ID, ROWID, ORA_ROWSCN FROM PNA_T1
ORDER BY ID FETCH FIRST 10 ROWS ONLY;