CREATE OR REPLACE FUNCTION create_report_car()
RETURNS TRIGGER
SECURITY DEFINER
AS $$
BEGIN
    DELETE FROM reports_car;

    INSERT INTO reports_car (car_id, car_label, car_model, car_year,
                              car_mileage, car_enginetype, car_enginecapacity,
                              car_power, car_price, total_sales, total_price)
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
        COUNT(o.id) AS total_orders,
        SUM(c.price) AS total_revenue
    FROM ordercar o
        INNER JOIN cars c ON o.car_id = c.id
        INNER JOIN label_model lm ON c.label_model_id = lm.id
        INNER JOIN labels l ON lm.label_id = l.id
        INNER JOIN model m ON lm.model_id = m.id
    WHERE o.status = true
    GROUP BY
        c.id, l.label, m.model;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_reports_car_trigger
AFTER INSERT ON ordercar
FOR EACH STATEMENT
EXECUTE FUNCTION create_report_car();

CREATE OR REPLACE FUNCTION generate_sales_report_car()
RETURNS VOID AS $$
BEGIN
    EXECUTE format('COPY (
        SELECT to_json(reports_car) FROM reports_car
    ) TO %L', 'D:\reports_car.json');
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION create_report_sparepart()
RETURNS TRIGGER
SECURITY DEFINER
AS $$
BEGIN
    DELETE FROM reports_sparepart;

    INSERT INTO reports_sparepart (sparepart_id, sparepart_label, label, model,
                                   sparepart_quantity, sparepart_price, total_sales,
                                   total_price)
    SELECT
        s.id,
        s.label,
        l.label,
        m.model,
        o.quantity,
        s.price * o.quantity AS price,
        COUNT(o.id) AS total_orders,
        SUM(s.price) AS total_revenue
    FROM orderspareparts o
        INNER JOIN spareparts s ON s.id = o.sparepart_id
        INNER JOIN label_model lm ON s.label_model_id = lm.id
        INNER JOIN labels l ON lm.label_id = l.id
        INNER JOIN model m ON lm.model_id = m.id
    WHERE o.status = true
    GROUP BY
        s.id, s.label, l.label, m.model, o.quantity, s.price;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_reports_sparepart_trigger
AFTER INSERT ON orderspareparts
FOR EACH STATEMENT
EXECUTE FUNCTION create_report_sparepart();

CREATE OR REPLACE FUNCTION generate_sales_report_sparepart()
RETURNS VOID AS $$
BEGIN
    EXECUTE format('COPY (
        SELECT to_json(reports_sparepart) FROM reports_sparepart
    ) TO %L', 'D:\reports_sparepart.json');
END;
$$ LANGUAGE plpgsql;

REVOKE EXECUTE ON FUNCTION create_report_car() FROM PUBLIC;
REVOKE EXECUTE ON FUNCTION generate_sales_report_car() FROM PUBLIC;
REVOKE EXECUTE ON FUNCTION create_report_sparepart() FROM PUBLIC;
REVOKE EXECUTE ON FUNCTION generate_sales_report_sparepart() FROM PUBLIC;




CREATE OR REPLACE PROCEDURE add_review_from_json() AS $$
BEGIN
    CREATE TEMP TABLE temp_reviews (
        data JSONB
    );

    COPY temp_reviews(data) FROM 'D:/reviews.json';

    INSERT INTO reviews (customer_id, date, rating, review)
    SELECT
        (data->>'CUSTOMER_ID')::INT,
        (data->>'DATE')::DATE,
        (data->>'RATING')::INT,
        (data->>'REVIEW')::VARCHAR(1000)
    FROM temp_reviews;
END;
$$ LANGUAGE plpgsql;