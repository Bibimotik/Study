CREATE OR REPLACE PROCEDURE add_manager(
    new_secondname VARCHAR(20),
    new_firstname VARCHAR(20),
    new_thirdname VARCHAR(20),
    new_mail VARCHAR(40),
    new_phone VARCHAR(16)
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO manager (secondname, firstname, thirdname, mail, phone)
    VALUES (new_secondname, new_firstname, new_thirdname, new_mail, new_phone);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_manager(manager_id INT)
SECURITY DEFINER
AS $$
BEGIN
    DELETE FROM MANAGER
    WHERE ID = manager_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_mechanic(
    new_secondname VARCHAR(20),
    new_firstname VARCHAR(20),
    new_thirdname VARCHAR(20),
    new_mail VARCHAR(40),
    new_phone VARCHAR(16)
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO mechanic (secondname, firstname, thirdname, mail, phone)
    VALUES (new_secondname, new_firstname, new_thirdname, new_mail, new_phone);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_mechanic(mechanic_id INT)
SECURITY DEFINER
AS $$
BEGIN
    DELETE FROM MECHANIC
    WHERE ID = mechanic_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_manager()
    RETURNS TABLE (
        manager_id INT,
        manager_secondname VARCHAR(20),
        manager_firstname VARCHAR(20),
        manager_thirdname VARCHAR(20)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT id, secondname, firstname, thirdname FROM manager;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_mechanic()
    RETURNS TABLE (
        mechanic_id INT,
        mechanic_secondname VARCHAR(20),
        mechanic_firstname VARCHAR(20),
        mechanic_thirdname VARCHAR(20)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT id, secondname, firstname, thirdname FROM mechanic;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_car()
    RETURNS TABLE (
        car_id INT,
        label_model_label VARCHAR(30),
        label_model_model VARCHAR(30),
        car_year INT,
        car_status BOOLEAN,
        car_price MONEY
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, l.label, m.model, c.year, c.status, c.price
    FROM cars c
        INNER JOIN label_model lm ON c.label_model_id = lm.id
        INNER JOIN labels l ON lm.label_id = l.id
        INNER JOIN model m ON lm.model_id = m.id
    ORDER BY l.label, m.model;
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

CREATE OR REPLACE PROCEDURE change_car_price(car_id INT, car_price MONEY)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE cars SET price = car_price WHERE id = car_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE change_sparepart_price(sparepart_id INT, sparepart_price MONEY)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE spareparts SET price = sparepart_price WHERE id = sparepart_id;
END;
$$ LANGUAGE plpgsql;





GRANT EXECUTE ON PROCEDURE add_manager(VARCHAR(20), VARCHAR(20), VARCHAR(20), VARCHAR(40), VARCHAR(16)) TO postgres;
GRANT EXECUTE ON PROCEDURE delete_manager(INT) TO postgres;
GRANT EXECUTE ON PROCEDURE add_mechanic(VARCHAR(20), VARCHAR(20), VARCHAR(20), VARCHAR(40), VARCHAR(16)) TO postgres;
GRANT EXECUTE ON PROCEDURE delete_mechanic(INT) TO postgres;
GRANT EXECUTE ON PROCEDURE change_car_price(INT, MONEY) TO postgres;
GRANT EXECUTE ON PROCEDURE change_sparepart_price(INT, MONEY) TO postgres;
GRANT EXECUTE ON PROCEDURE add_review_from_json() TO postgres;
GRANT EXECUTE ON FUNCTION get_all_manager() TO postgres;
GRANT EXECUTE ON FUNCTION get_all_mechanic() TO postgres;
GRANT EXECUTE ON FUNCTION get_all_car() TO postgres;
GRANT EXECUTE ON FUNCTION get_all_sparepart() TO postgres;
GRANT EXECUTE ON FUNCTION create_report_car() TO postgres;
GRANT EXECUTE ON FUNCTION create_report_sparepart() TO postgres;
GRANT EXECUTE ON FUNCTION generate_sales_report_car() TO postgres;
GRANT EXECUTE ON FUNCTION generate_sales_report_sparepart() TO postgres;