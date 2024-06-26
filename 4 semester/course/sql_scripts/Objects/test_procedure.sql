/*----------------------MANAGER----------------------------*/
SELECT calculate_total_price(null, '2023-03-03');
CALL add_car(
    2,
    2019,
    33000,
    'Electric'::VARCHAR(20),
    3.1,
    330,
    45000::MONEY,
    'cool'::VARCHAR(200),
    false
);
CALL update_car(
    8,
    1,
    1980,
    12000,
    'Electric'::VARCHAR(20),
    2,
    256,
    7000::MONEY,
    'not cool'::VARCHAR(200),
    false
);
CALL delete_car(3, true);
CALL add_sparepart(
    'Valve'::VARCHAR(20),
    1,
    'new cool valve',
    450::MONEY,
    3,
    true
);
CALL update_sparepart(
    4,
    'Valve'::VARCHAR(20),
    1,
    'new cool valve',
    450::MONEY,
    3,
    false
);
CALL delete_sparepart(4, true);
CALL add_customer(
    'Bankuzau'::VARCHAR(20),
    'Mikhail'::VARCHAR(20),
    'Olegovich'::VARCHAR(20),
    'mikhail@yandex.ru'::VARCHAR(40),
    '111234567'::VARCHAR(10),
    'Belarus'::VARCHAR(10),
    'Sverdlova 13a'::VARCHAR(100),
    '1234123412341234'::VARCHAR(16)
);
CALL update_customer(
    4,
    'Bankuzau'::VARCHAR(20),
    'Mikhail'::VARCHAR(20),
    'Olegovich'::VARCHAR(20),
    'mikhail@yandex.ru'::VARCHAR(40),
    '111234567'::VARCHAR(10),
    'Belarus'::VARCHAR(10),
    'Sverdlova 13a'::VARCHAR(100),
    '1234123412341234'::VARCHAR(16)
);
CALL change_status_ordercar(8, false);
CALL change_status_orderspareparts(2, false);
/*----------------------USER----------------------------*/
CALL get_cars();
CALL get_spareparts();
CALL get_ordercars(1);
CALL get_orderspareparts(1);
CALL get_servicesheet(1);
CALL get_data_status(2);
/*----------------------MECHANIC----------------------------*/
CALL get_form(1);
CALL update_service_sheet(1, '2023-01-01', 1, 'new description');
CALL order_spareparts(1, 4, true);