CREATE TABLE MyTable (
    ID INT PRIMARY KEY,
    Data XML
);
INSERT INTO MyTable (ID, Data)
VALUES (1, '<Root>
                <Element>Value 1</Element>
            </Root>');
SELECT ID, Data
FROM MyTable;
SELECT ID, Data.value('(/Root/Element)[1]', 'nvarchar(50)') AS ElementValue
FROM MyTable;