/*----------------------MANAGER------------------------*/
CREATE OR REPLACE PROCEDURE add_car(
    new_label_model_id NUMERIC,
    new_year INT,
    new_mileage NUMERIC,
    new_enginetype VARCHAR(20),
    new_enginecapacity NUMERIC,
    new_power INT,
    new_price MONEY,
    new_description VARCHAR(1000),
    new_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO CARS (label_model_id, year, mileage, enginetype, enginecapacity, power, price, description, status)
    VALUES (new_label_model_id, new_year, new_mileage, new_enginetype, new_enginecapacity, new_power, new_price, new_description, new_status);

    RAISE NOTICE 'Машина добавлена успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при добавлении машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_car(
    car_id NUMERIC,
    new_label_model_id NUMERIC,
    new_year INT,
    new_mileage NUMERIC,
    new_enginetype VARCHAR(20),
    new_enginecapacity NUMERIC,
    new_power INT,
    new_description VARCHAR(1000),
    new_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE CARS
    SET label_model_id = new_label_model_id,
        year = new_year,
        mileage = new_mileage,
        enginetype = new_enginetype,
        enginecapacity = new_enginecapacity,
        power = new_power,
        description = new_description,
        status = new_status
    WHERE id = car_id;

    RAISE NOTICE 'Машина обновлена успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_car(car_id NUMERIC, new_status BOOLEAN)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE CARS
    SET status = new_status
    WHERE id = car_id;

    RAISE NOTICE 'Статус машины обновлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении статуса машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_sparepart(
    new_label VARCHAR(50),
    new_label_model_id NUMERIC,
    new_description VARCHAR(1000),
    new_quantity INT,
    new_price MONEY,
    new_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO SPAREPARTS (label, label_model_id, description, quantity, price, status)
    VALUES (new_label, new_label_model_id, new_description, new_quantity, new_price, new_status);

    RAISE NOTICE 'Запчасть добавлена успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при добавлении запчасти: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_sparepart(
    sparepart_id NUMERIC,
    new_label VARCHAR(50),
    new_label_model_id NUMERIC,
    new_description VARCHAR(1000),
    new_quantity INT,
    new_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE SPAREPARTS
    SET label = new_label,
    label_model_id = new_label_model_id,
    description = new_description,
    quantity = new_quantity,
    status = new_status
    WHERE id = update_sparepart.sparepart_id;

    RAISE NOTICE 'Запчасть обновлена успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении запчасти: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_sparepart(sparepart_id NUMERIC, new_quantity INT, new_status BOOLEAN)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE SPAREPARTS
    SET status = new_status, quantity = new_quantity
    WHERE id = delete_sparepart.sparepart_id;

    RAISE NOTICE 'Статус запчасти обновлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении статуса запчасти: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_customer(
    new_secondname VARCHAR(20),
    new_firstname VARCHAR(20),
    new_thirdname VARCHAR(20),
    new_mail VARCHAR(40),
    new_phone VARCHAR(16),
    new_country VARCHAR(50),
    new_address VARCHAR(100),
    new_requisites VARCHAR(16)
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO CUSTOMER ( secondname, firstname, thirdname, mail, phone, country, address, requisites)
    VALUES (new_secondname, new_firstname, new_thirdname, new_mail, new_phone, new_country, new_address, new_requisites);

    RAISE NOTICE 'Покупатель добавлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при добавлении покупателя: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_customer(
    customer_id NUMERIC,
    new_secondname VARCHAR(20),
    new_firstname VARCHAR(20),
    new_thirdname VARCHAR(20),
    new_mail VARCHAR(40),
    new_phone VARCHAR(16),
    new_country VARCHAR(50),
    new_address VARCHAR(100),
    new_requisites VARCHAR(16)
)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE CUSTOMER
    SET id = customer_id,
    secondname = new_secondname,
    firstname = new_firstname,
    thirdname = new_thirdname,
    mail = new_mail,
    phone = new_phone,
    country = new_country,
    address = new_address,
    requisites = new_requisites
    WHERE id = update_customer.customer_id;

    RAISE NOTICE 'Покупатель обновлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении покупателя: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE change_status_ordercar(ordercar_id NUMERIC, new_status BOOLEAN)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE ORDERCAR
    SET status = new_status
    WHERE id = ordercar_id;

    RAISE NOTICE 'Статус заказа машины обновлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении статуса заказа машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE change_status_orderspareparts(ordersparepart_id NUMERIC, new_status BOOLEAN)
SECURITY DEFINER
AS $$
BEGIN
    UPDATE ORDERSPAREPARTS
    SET status = new_status
    WHERE id = ordersparepart_id;

    RAISE NOTICE 'Статус заказа запчасти обновлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при обновлении статуса заказа машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION calculate_total_price(start_date DATE, end_date DATE)
RETURNS MONEY
SECURITY DEFINER
AS $$
DECLARE
    total_price MONEY := 0;
BEGIN
    IF start_date IS NULL AND end_date IS NULL THEN
        SELECT SUM(c.PRICE) INTO total_price
        FROM ORDERCAR o
        JOIN CARS c ON o.CAR_ID = c.ID
        WHERE o.STATUS = true;
    ELSIF start_date IS NULL THEN
        SELECT SUM(c.PRICE) INTO total_price
        FROM ORDERCAR o
        JOIN CARS c ON o.CAR_ID = c.ID
        WHERE o.DATE <= end_date
        AND o.STATUS = true;
    ELSIF end_date IS NULL THEN
        SELECT SUM(c.PRICE) INTO total_price
        FROM ORDERCAR o
        JOIN CARS c ON o.CAR_ID = c.ID
        WHERE o.DATE >= start_date
        AND o.STATUS = true;
    ELSE
        SELECT SUM(c.PRICE) INTO total_price
        FROM ORDERCAR o
        JOIN CARS c ON o.CAR_ID = c.ID
        WHERE o.DATE BETWEEN start_date AND end_date
        AND o.STATUS = true;
    END IF;

    RETURN total_price;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_available_cars()
  RETURNS TABLE (
    car_id INT,
    label VARCHAR(30),
    model VARCHAR(30),
    year INT,
    mileage NUMERIC,
    engine_type VARCHAR(20),
    engine_capacity NUMERIC,
    power INT,
    price MONEY,
    description VARCHAR(1000)
  )
SECURITY DEFINER
AS $$
DECLARE
  total_cars_count INT;
BEGIN
  SELECT COUNT(*) INTO total_cars_count
  FROM cars
  WHERE status = true;

  RAISE NOTICE 'Доступно машин: %', total_cars_count;

  RETURN QUERY
  SELECT
    c.id AS car_id,
    l.label AS label,
    m.model AS model,
    c.year,
    c.mileage,
    c.enginetype AS engine_type,
    c.enginecapacity AS engine_capacity,
    c.power,
    c.price,
    c.description
  FROM
    cars c
    INNER JOIN label_model lm ON c.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    c.status = true;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_available_customers()
  RETURNS TABLE (
    customer_id INT,
    secondname VARCHAR(20),
    firstname VARCHAR(20),
    thirdname VARCHAR(20),
    mail VARCHAR(40),
    phone VARCHAR(16),
    country VARCHAR(50),
    address VARCHAR(100)
  )
SECURITY DEFINER
AS $$
DECLARE
  total_customers_count INT;
BEGIN
  SELECT COUNT(*)
  INTO total_customers_count
  FROM customer;
  RAISE NOTICE 'Всего клиентов: %', total_customers_count;

  RETURN QUERY
  SELECT
    c.id AS customer_id,
    c.secondname,
    c.firstname,
    c.thirdname,
    c.mail,
    c.phone,
    c.country,
    c.address
  FROM customer c;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_available_spareparts()
  RETURNS TABLE (
    sparepart_id INT,
    label_sparepart VARCHAR(50),
    label VARCHAR(30),
    model VARCHAR(30),
    description VARCHAR(1000),
    price MONEY,
    quantity INT
  )
SECURITY DEFINER
AS $$
DECLARE
  total_spareparts_count INT;
BEGIN
  SELECT COUNT(*) INTO total_spareparts_count
  FROM spareparts
  WHERE status = true;

  RAISE NOTICE 'Доступно запчастей: %', total_spareparts_count;

  RETURN QUERY
  SELECT
    s.id AS sparepart_id,
    s.label AS name,
    l.label,
    m.model,
    s.description,
    s.price,
    s.quantity
  FROM
    spareparts s
    INNER JOIN label_model lm ON s.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    s.status = true;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_label_model()
    RETURNS TABLE (
        label_model_id INT,
        label_model_label VARCHAR(30),
        label_model_model VARCHAR(30)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT lm.id AS label_model_id, l.label, m.model
    FROM label_model lm
        INNER JOIN labels l ON lm.label_id = l.id
        INNER JOIN model m ON lm.model_id = m.id
    ORDER BY l.label, m.model;
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

CREATE OR REPLACE FUNCTION get_all_customer()
    RETURNS TABLE (
        customer_id INT,
        customer_secondname VARCHAR(20),
        customer_firstname VARCHAR(20),
        customer_thirdname VARCHAR(20),
        customer_mail VARCHAR(40),
        customer_phone VARCHAR(16),
        customer_country VARCHAR(50),
        customer_address VARCHAR(100),
        customer_requisites VARCHAR(16)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT id, secondname, firstname, thirdname, mail, phone, country, address, requisites
    FROM customer
    ORDER BY secondname;
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

CREATE OR REPLACE FUNCTION get_orders_cars_by_manager_id(managers_id INT)
    RETURNS TABLE (
        order_id INT,
        secondname_manager VARCHAR(20),
        label_car VARCHAR(30),
        model_car VARCHAR(30),
        year_car INT,
        secondname_customer VARCHAR(20),
        status_car BOOLEAN
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT o.id, m.secondname, l.label, mo.model, c.year, mc.secondname, o.status
    FROM ORDERCAR o
        INNER JOIN MANAGER m ON o.manager_id = m.id
        INNER JOIN CARS c ON o.car_id = c.id
        INNER JOIN LABEL_MODEL lm ON c.label_model_id = lm.id
        INNER JOIN LABELS l ON lm.label_id = l.id
        INNER JOIN MODEL mo ON lm.model_id = mo.id
        INNER JOIN CUSTOMER mc ON o.customer_id = mc.id
    WHERE o.manager_id = managers_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_orders_spare_parts_by_customer_id(customers_id INT)
    RETURNS TABLE (
        order_id INT,
        secondname_customer VARCHAR(20),
        sparepart_label VARCHAR(50),
        label VARCHAR(30),
        model VARCHAR(30),
        status BOOLEAN
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT os.id, c.secondname, sp.label, l.label, m.model, os.status
    FROM ORDERSPAREPARTS os
        INNER JOIN CUSTOMER c ON os.customer_id = c.id
        INNER JOIN SPAREPARTS sp ON os.sparepart_id = sp.id
        INNER JOIN LABEL_MODEL lm ON sp.label_model_id = lm.id
        INNER JOIN LABELS l ON lm.label_id = l.id
        INNER JOIN MODEL m ON lm.model_id = m.id
    WHERE c.id = customers_id;
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




CREATE USER MANAGER PASSWORD '111';

GRANT EXECUTE ON PROCEDURE add_car(NUMERIC, NUMERIC, INT, NUMERIC, VARCHAR(20), NUMERIC, INT, VARCHAR(200), BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE update_car(NUMERIC, NUMERIC, INT, NUMERIC, VARCHAR(20), NUMERIC, INT, VARCHAR(200), BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE delete_car(NUMERIC, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE add_sparepart(VARCHAR(50), NUMERIC, VARCHAR(1000), INT, MONEY, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE update_sparepart(NUMERIC, VARCHAR(50), NUMERIC, VARCHAR(1000), INT, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE delete_sparepart(NUMERIC, INT, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE add_customer(VARCHAR(20), VARCHAR(20), VARCHAR(20), VARCHAR(40), VARCHAR(16), VARCHAR(50), VARCHAR(100), VARCHAR(16)) TO MANAGER;
GRANT EXECUTE ON PROCEDURE update_customer(NUMERIC, VARCHAR(20), VARCHAR(20), VARCHAR(20), VARCHAR(40), VARCHAR(16), VARCHAR(50), VARCHAR(100), VARCHAR(16)) TO MANAGER;
GRANT EXECUTE ON PROCEDURE change_status_ordercar(NUMERIC, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON PROCEDURE change_status_orderspareparts(NUMERIC, BOOLEAN) TO MANAGER;
GRANT EXECUTE ON FUNCTION calculate_total_price(DATE, DATE) TO MANAGER;
GRANT EXECUTE ON FUNCTION get_available_cars() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_available_customers() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_available_spareparts() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_all_label_model() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_all_car() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_all_customer() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_all_manager() TO MANAGER;
GRANT EXECUTE ON FUNCTION get_orders_cars_by_manager_id(INT) TO MANAGER;
GRANT EXECUTE ON FUNCTION get_orders_spare_parts_by_customer_id(INT) TO MANAGER;
GRANT EXECUTE ON FUNCTION get_all_sparepart() TO MANAGER;
