create table BMO_table(
    ID number not null,
    NAME VARCHAR2(30),
    primary key (ID)
);

INSERT INTO BMO_table (ID, NAME) VALUES (1, 'Строка 1');
INSERT INTO BMO_table (ID, NAME) VALUES (2, 'Строка 2');
INSERT INTO BMO_table (ID, NAME) VALUES (3, 'Строка 3'); 
--drop table BMO_table;
select * from BMO_table;
---------------------------------------------
CREATE TABLESPACE PD_EA_DATA
DATAFILE 'PD_EA_DATA.dbf'
SIZE 7M
AUTOEXTEND ON
NEXT 5M
MAXSIZE 30M;

CREATE TEMPORARY TABLESPACE PD_EA_TEMP
TEMPFILE 'PD_EA_TEMP.dbf'
SIZE 5M
AUTOEXTEND ON
NEXT 3M
MAXSIZE 20M;

select FILE_NAME, TABLESPACE_NAME, STATUS, MAXBYTES, USER_BYTES FROM DBA_DATA_FILES
UNION
SELECT FILE_NAME, TABLESPACE_NAME, STATUS, MAXBYTES, USER_BYTES FROM DBA_TEMP_FILES;

create role RLEACORE;

select * from dba_roles where role like 'RL%';

grant create session,
    create table,
    create view,
    create procedure to RLEACORE;
    
select * from DBA_SYS_PRIVS where GRANTEE = 'RLEACORE';

create profile PFEACORE limit
password_life_time 100
sessions_per_user 3
failed_login_attempts 7
password_lock_time 1
password_reuse_time 10
password_grace_time default
connect_time 180
idle_time 30;



select * from DBA_PROFILES where PROFILE = 'PFEACORE';

select * from DBA_PROFILES where PROFILE = 'DEFAULT';

create user U1_BMO_PDB
default tablespace PD_EA_DATA quota unlimited on PD_EA_DATA
temporary tablespace PD_EA_TEMP
profile PFEACORE
account unlock
--drop user u1_bmo_pdb;

ALTER USER U1_BMO_PDB IDENTIFIED BY NewPassword;
-----------------------------------------------------------
select * from user_tablespaces;

select * from dba_data_files;

select * from dba_roles;
select * from dba_role_privs order by grantee;

select * from dba_profiles;
select * from dba_users;
-----------------------------------------------------------
SELECT owner, object_name, object_type
FROM all_objects
WHERE owner IN ('c##cdb_admin', 'U1_BMO_PDB')
ORDER BY owner, object_type, object_name;
-----------------------------------------------------------
SELECT sid, serial#, username, machine
FROM v$session
WHERE type = 'USER';