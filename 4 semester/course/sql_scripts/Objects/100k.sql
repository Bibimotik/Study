DO $$
DECLARE
    i INT;
    random_customer_id INT;
    random_date TIMESTAMP;
    random_rating INT;
    random_review TEXT;
BEGIN
    FOR i IN 1..100000 LOOP
        SELECT id INTO random_customer_id FROM customer ORDER BY RANDOM() LIMIT 1;

        random_date := NOW() - INTERVAL '2 years' * RANDOM();

        random_rating := FLOOR(RANDOM() * 5 + 1)::INT;

        random_review := 'Отзыв ' || i;

        INSERT INTO reviews (customer_id, date, rating, review)
        VALUES (random_customer_id, random_date, random_rating, random_review);
    END LOOP;
END $$;

CREATE INDEX idx_reviews_customer_rating_date ON reviews (customer_id, rating, date);