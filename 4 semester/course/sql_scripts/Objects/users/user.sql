/*----------------------USER----------------------------*/
CREATE OR REPLACE PROCEDURE add_ordercar(
    order_customer_id INTEGER,
    order_manager_id INTEGER,
    order_date DATE,
    order_car_id INTEGER,
    order_status BOOLEAN,
    order_comment VARCHAR(1000)
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO ORDERCAR (CUSTOMER_ID, MANAGER_ID, DATE, CAR_ID, STATUS, COMMENT)
    VALUES (order_customer_id, order_manager_id, order_date, order_car_id, order_status, order_comment);
    RAISE NOTICE 'Заказ машины создан успешно.';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при создании заказа машины: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_ordersparepart(
    order_customer_id INTEGER,
    order_sparepart_id INTEGER,
    order_date DATE,
    order_quantity INT,
    order_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO ORDERSPAREPARTS (CUSTOMER_ID, SPAREPART_ID, DATE, QUANTITY, STATUS)
    VALUES (order_customer_id, order_sparepart_id, order_date, order_quantity, order_status);
    RAISE NOTICE 'Заказ запчасти создан успешно.';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при создании заказа запчасти: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_review(
    cust_id INT,
    review_date DATE,
    review_rating INT,
    review_text VARCHAR(1000)
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO REVIEWS (CUSTOMER_ID, DATE, RATING, REVIEW)
    VALUES (cust_id, review_date, review_rating, review_text);
    RAISE NOTICE 'Отзыв оставлен успешно';
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Ошибка при добавлении отзыва: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE add_servicesheet(
    p_customer_id NUMERIC,
    p_mechanic_id NUMERIC,
    p_date DATE,
    p_label_model_id NUMERIC,
    p_problem_description VARCHAR(1000),
    p_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    INSERT INTO SERVICESHEET (CUSTOMER_ID, MECHANIC_ID, DATE, LABEL_MODEL_ID, PROBLEMDESCRIPTION, STATUS)
    VALUES (p_customer_id, p_mechanic_id, p_date, p_label_model_id, p_problem_description, p_status);
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

CREATE OR REPLACE FUNCTION get_customer_by_phone(phone_number VARCHAR(16))
    RETURNS TABLE (
        id INT,
        secondname VARCHAR(20),
        firstname VARCHAR(20),
        thirdname VARCHAR(20),
        mail VARCHAR(40),
        phone VARCHAR(16),
        country VARCHAR(50),
        address VARCHAR(100),
        requisites VARCHAR(16)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM CUSTOMER
    WHERE CUSTOMER.PHONE = phone_number;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_available_spareparts()
  RETURNS TABLE (
    sparepart_id INT,
    label_sparepart VARCHAR(50),
    label VARCHAR(30),
    model VARCHAR(30),
    description VARCHAR(1000),
    quantity INT,
    price MONEY
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
    s.quantity,
    s.price
  FROM
    spareparts s
    INNER JOIN label_model lm ON s.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    s.status = true;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION check_quantity_spareparts(order_id INT)
    RETURNS TABLE (
        available_quantity INT
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT s.quantity FROM SPAREPARTS s WHERE s.id = order_id;
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

CREATE OR REPLACE FUNCTION get_date_status(cust_id NUMERIC)
  RETURNS TABLE (
    label VARCHAR(30),
    model VARCHAR(30),
    year INT,
    price MONEY,
    date DATE,
    status BOOLEAN
  )
SECURITY DEFINER
AS $$
BEGIN
  RETURN QUERY
  SELECT
    l.label,
    m.model,
    c.year,
    c.price,
    o.date,
    o.status
  FROM
    ordercar o
    INNER JOIN cars c ON o.car_id = c.id
    INNER JOIN label_model lm ON c.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    o.customer_id = cust_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_date_status_spare(cust_id NUMERIC)
  RETURNS TABLE (
    label_sparepart VARCHAR(50),
    label VARCHAR(50),
    model VARCHAR(30),
    quantity INT,
    price MONEY,
    date DATE,
    status BOOLEAN
  )
SECURITY DEFINER
AS $$
BEGIN
  RETURN QUERY
  SELECT
    sp.label,
    l.label,
    m.model,
    os.quantity,
    sp.price * os.quantity AS price,
    os.date,
    os.status
  FROM
    orderspareparts os
    INNER JOIN spareparts sp ON os.sparepart_id = sp.id
    INNER JOIN label_model lm ON sp.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    os.customer_id = cust_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_history_orders_cars(cust_id NUMERIC)
  RETURNS TABLE (
    label VARCHAR(30),
    model VARCHAR(30),
    year INT,
    price MONEY,
    date DATE
  )
SECURITY DEFINER
AS $$
BEGIN
  RETURN QUERY
  SELECT
    l.label,
    m.model,
    c.year,
    c.price,
    o.date
  FROM
    ordercar o
    INNER JOIN cars c ON o.car_id = c.id
    INNER JOIN label_model lm ON c.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    o.customer_id = cust_id AND o.status = true;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_history_orders_spareparts(cust_id NUMERIC)
  RETURNS TABLE (
    label_sparepart VARCHAR(50),
    label VARCHAR(50),
    model VARCHAR(30),
    quantity INT,
    price MONEY,
    date DATE
  )
SECURITY DEFINER
AS $$
BEGIN
  RETURN QUERY
  SELECT
    sp.label,
    l.label,
    m.model,
    os.quantity,
    sp.price * os.quantity AS price,
    os.date
  FROM
    orderspareparts os
    INNER JOIN spareparts sp ON os.sparepart_id = sp.id
    INNER JOIN label_model lm ON sp.label_model_id = lm.id
    INNER JOIN labels l ON lm.label_id = l.id
    INNER JOIN model m ON lm.model_id = m.id
  WHERE
    os.customer_id = cust_id AND os.status = true;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_servicesheet(cust_id NUMERIC)
RETURNS TABLE (
    label VARCHAR(30),
    model VARCHAR(30),
    mechanic_secondname VARCHAR(20),
    mechanic_firstname VARCHAR(20),
    mechanic_thirdname VARCHAR(20),
    date DATE,
    problem_description VARCHAR(1000),
    price MONEY,
    status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT l.label,
           m.model,
           mech.secondname,
           mech.firstname,
           mech.thirdname,
           s.date,
           s.problemdescription,
           s.price,
           s.status
    FROM SERVICESHEET s
        INNER JOIN LABEL_MODEL lm ON s.label_model_id = lm.id
        INNER JOIN LABELS l ON lm.label_id = l.id
        INNER JOIN MODEL m ON lm.model_id = m.id
        INNER JOIN MECHANIC mech ON s.mechanic_id = mech.id
    WHERE s.customer_id = cust_id;
END;
$$
LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_reviews()
    RETURNS TABLE (
        review_id INT,
        customer_firstname VARCHAR(20),
        review_date DATE,
        review_rating INT,
        review_review VARCHAR(1000)
    )
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT r.id,
            c.firstname,
            r.date,
            r.rating,
            r.review
    FROM reviews r
        INNER JOIN customer c ON r.customer_id = c.id
    ORDER BY r.date DESC
    LIMIT 30;
END;
$$
LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_labels()
RETURNS TABLE (
    label_label_id INT,
    label_label VARCHAR(30),
    label_country VARCHAR(50)
)
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT id, label, country FROM labels;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_models_by_label(l_id INT)
RETURNS TABLE (
    id INT,
    model_model_id INT,
    model_model VARCHAR(30)
)
SECURITY DEFINER
AS $$
BEGIN
    RETURN QUERY
    SELECT lm.id, m.id, m.model
    FROM LABEL_MODEL lm
    JOIN MODEL m ON lm.MODEL_ID = m.ID
    WHERE lm.LABEL_ID = l_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION search_cars(
    search_year INT,
    search_label_id INT,
    search_label_model_id INT
) RETURNS TABLE (
    p_id INT,
    p_label VARCHAR(30),
    p_model VARCHAR(30),
    p_year INT,
    p_mileage NUMERIC,
    p_engine_type VARCHAR(20),
    p_engine_capacity NUMERIC,
    p_power INT,
    p_price MONEY,
    p_description VARCHAR(1000),
    p_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    IF search_year IS NOT NULL AND search_label_id IS NOT NULL AND search_label_model_id IS NOT NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            c.year = search_year AND
            c.label_model_id = search_label_model_id AND
            lm.label_id = search_label_id;
    ELSIF search_year IS NOT NULL AND search_label_id IS NOT NULL AND search_label_model_id IS NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            c.year = search_year AND
            lm.label_id = search_label_id;
    ELSIF search_year IS NULL AND search_label_id IS NOT NULL AND search_label_model_id IS NOT NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            c.label_model_id = search_label_model_id AND
            lm.label_id = search_label_id;
    ELSIF search_year IS NOT NULL AND search_label_id IS NULL AND search_label_model_id IS NOT NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            c.year = search_year AND
            c.label_model_id = search_label_model_id;
    ELSIF search_year IS NOT NULL AND search_label_id IS NULL AND search_label_model_id IS NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            c.year = search_year;
    ELSIF search_year IS NULL AND search_label_id IS NOT NULL AND search_label_model_id IS NULL THEN
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id
        WHERE
            lm.label_id = search_label_id;
    ELSE
        RETURN QUERY
        SELECT
            c.id,
            l.label,
            m.model,
            c.year,
            c.mileage,
            c.enginetype,
            c.enginecapacity,
            c.power,
            c.price,
            c.description,
            c.status
        FROM
            cars c
            JOIN label_model lm ON c.label_model_id = lm.id
            JOIN labels l ON lm.label_id = l.id
            JOIN model m ON lm.model_id = m.id;
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION search_spareparts(
    search_label_id INT,
    search_label_model_id INT DEFAULT NULL
) RETURNS TABLE (
    p_id INT,
    p_label VARCHAR(30),
    p_llabel VARCHAR(30),
    p_model VARCHAR(30),
    p_description VARCHAR(1000),
    p_quantity INT,
    p_price MONEY,
    p_status BOOLEAN
)
SECURITY DEFINER
AS $$
BEGIN
    IF search_label_model_id IS NOT NULL THEN
        RETURN QUERY
        SELECT
            s.id,
            s.label,
            l.label,
            m.model,
            s.description,
            s.quantity,
            s.price,
            s.status
        FROM
            spareparts s
        JOIN label_model lm ON s.label_model_id = lm.id
        JOIN labels l ON lm.label_id = l.id
        JOIN model m ON lm.model_id = m.id
        WHERE
            lm.label_id = search_label_id AND
            lm.model_id = search_label_model_id;
    ELSE
        RETURN QUERY
        SELECT
            s.id,
            s.label,
            l.label,
            m.model,
            s.description,
            s.quantity,
            s.price,
            s.status
        FROM
            spareparts s
        JOIN label_model lm ON s.label_model_id = lm.id
        JOIN labels l ON lm.label_id = l.id
        JOIN model m ON lm.model_id = m.id
        WHERE
            lm.label_id = search_label_id;
    END IF;
END;
$$ LANGUAGE plpgsql;










CREATE USER MYUSER PASSWORD '222';

GRANT EXECUTE ON PROCEDURE add_ordercar(INT, INT, DATE, INT, BOOLEAN, VARCHAR(1000)) TO MYUSER;
GRANT EXECUTE ON PROCEDURE add_ordersparepart(INT, INT, DATE, INT, BOOLEAN) TO MYUSER;
GRANT EXECUTE ON PROCEDURE add_review(INT, DATE, INT, VARCHAR(1000)) TO MYUSER;
GRANT EXECUTE ON PROCEDURE add_servicesheet(NUMERIC, NUMERIC, DATE, NUMERIC, VARCHAR(1000), BOOLEAN) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_car() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_label_model() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_manager() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_available_cars() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_customer_by_phone(VARCHAR(16)) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_available_spareparts() TO MYUSER;
GRANT EXECUTE ON FUNCTION check_quantity_spareparts(INT) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_mechanic() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_date_status(NUMERIC) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_date_status_spare(NUMERIC) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_history_orders_cars(NUMERIC) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_history_orders_spareparts(NUMERIC) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_servicesheet(NUMERIC) TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_reviews() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_labels() TO MYUSER;
GRANT EXECUTE ON FUNCTION get_all_models_by_label(INT) TO MYUSER;
GRANT EXECUTE ON FUNCTION search_cars(INT, INT, INT) TO MYUSER;
GRANT EXECUTE ON FUNCTION search_spareparts(INT, INT) TO MYUSER;
