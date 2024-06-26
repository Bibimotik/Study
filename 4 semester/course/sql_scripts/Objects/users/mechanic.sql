/*----------------------MECHANIC----------------------------*/
CREATE OR REPLACE FUNCTION get_mechanic_by_phone(phone_number VARCHAR)
    RETURNS TABLE (
        mech_id INT,
        secondname VARCHAR(20),
        firstname VARCHAR(20),
        thirdname VARCHAR(20),
        mail VARCHAR(40),
        phone VARCHAR(16)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT m.id, m.secondname, m.firstname, m.thirdname, m.mail, m.phone
    FROM mechanic m
    WHERE m.phone = phone_number;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_mechanic()
    RETURNS TABLE (
        mechanic_id INT,
        mechanic_secondname VARCHAR(20),
        mechanic_firstname VARCHAR(20),
        mechanic_thirdname VARCHAR(20)
    )
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT id, secondname, firstname, thirdname FROM mechanic;
END;
$$;

CREATE OR REPLACE FUNCTION get_customer_full_names(mech_id INT)
    RETURNS TABLE (
        id INT,
        secondname VARCHAR(20),
        firstname VARCHAR(20),
        thirdnmae VARCHAR(20)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT
        c.id,
        c.secondname,
        c.firstname,
        c.thirdname
    FROM servicesheet ss
        INNER JOIN customer c ON ss.customer_id = c.id
    WHERE ss.mechanic_id = mech_id;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_service_sheet(mech_id INT)
    RETURNS TABLE (
        id INT,
        label VARCHAR(30),
        model VARCHAR(30),
        customer_id INT,
        customer_secondname VARCHAR(20),
        customer_firstname VARCHAR(20),
        price MONEY,
        date DATE,
        mechanic_secondname VARCHAR(20),
        mechanic_firstname VARCHAR(20),
        description VARCHAR(1000),
        status BOOLEAN
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT
        ss.id,
        lb.LABEL,
        md.MODEL,
        c.id,
        c.SECONDNAME,
        c.FIRSTNAME,
        ss.PRICE,
        ss.DATE,
        m.SECONDNAME,
        m.FIRSTNAME,
        ss.problemdescription,
        ss.STATUS
    FROM
        SERVICESHEET ss
        JOIN CUSTOMER c ON ss.CUSTOMER_ID = c.ID
        JOIN MECHANIC m ON ss.MECHANIC_ID = m.ID
        JOIN LABEL_MODEL lm ON ss.LABEL_MODEL_ID = lm.ID
        JOIN LABELS lb ON lm.LABEL_ID = lb.ID
        JOIN MODEL md ON lm.MODEL_ID = md.ID
    WHERE
        ss.MECHANIC_ID = mech_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_service_sheet(
    service_sheet_id INT,
    service_sheet_description VARCHAR(1000),
    service_sheet_price MONEY,
    service_sheet_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE SERVICESHEET
    SET
        PROBLEMDESCRIPTION = service_sheet_description,
        PRICE = service_sheet_price,
        STATUS = service_sheet_status
    WHERE ID = service_sheet_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_sparepart()
    RETURNS TABLE (
        sparepart_id INT,
        sparepart_label VARCHAR(50),
        label_model_label VARCHAR(30),
        label_model_model VARCHAR(30),
        sparepart_quantity INT,
        sparepart_status BOOLEAN,
        sparepart_description VARCHAR(1000),
        label_model_id INT,
        sparepart_price MONEY
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT s.id, s.label, l.label, m.model, s.quantity, s.status, s.description, s.label_model_id, s.price
    FROM spareparts s
        INNER JOIN label_model lm ON s.label_model_id = lm.id
        INNER JOIN labels l ON lm.label_id = l.id
        INNER JOIN model m ON lm.model_id = m.id
    ORDER BY s.label;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_sparepart_quantity(part_id INT, reduce_quantity INT)
SECURITY DEFINER
AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SPAREPARTS WHERE ID = part_id) THEN
        RAISE EXCEPTION 'Запчасть с ID % не существует', part_id;
    END IF;

    IF (SELECT QUANTITY FROM SPAREPARTS WHERE ID = part_id) < reduce_quantity THEN
        RAISE EXCEPTION 'Недостаточное количество для запчасти с ID %', part_id;
    END IF;

    UPDATE SPAREPARTS
    SET
        QUANTITY = QUANTITY - reduce_quantity,
        STATUS = CASE WHEN QUANTITY - reduce_quantity = 0 THEN FALSE ELSE STATUS END
    WHERE ID = part_id;

    RAISE NOTICE 'Количество для запчасти с ID % уменьшено на %', part_id, reduce_quantity;
END;
$$ LANGUAGE plpgsql;








CREATE USER MECHANIC PASSWORD '333';

GRANT EXECUTE ON PROCEDURE update_service_sheet(INT, VARCHAR(1000), MONEY, BOOLEAN) TO MECHANIC;
GRANT EXECUTE ON PROCEDURE update_sparepart_quantity(INT, INT) TO MECHANIC;
GRANT EXECUTE ON FUNCTION get_service_sheet(INT) TO MECHANIC;
GRANT EXECUTE ON FUNCTION get_customer_full_names(INT) TO MECHANIC;
GRANT EXECUTE ON FUNCTION get_all_mechanic() TO MECHANIC;
GRANT EXECUTE ON FUNCTION get_mechanic_by_phone(VARCHAR(16)) TO MECHANIC;
GRANT EXECUTE ON FUNCTION get_all_sparepart() TO MECHANIC;
