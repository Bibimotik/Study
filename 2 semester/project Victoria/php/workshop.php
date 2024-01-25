<!DOCTYPE html>
<html lang="ru-RU">
<head>
    <meta charset="UTF-8">
    <link rel="shortcut icon" href="/project/images/icons/favicon.ico">
    <link media="all" href="/project/css/workshop.css" rel="stylesheet">
    <style>
        html {
            height: 100%
        }
        body {
            width: 100%;
            height: 100%;
            font-family: "Montserrat";
            background: url(/project/images/form_out_background.jpg) center;
            background-size: cover;
        }
    </style>
    <title>Мастерская</title>
</head>
<body>
    <div style="background-color: white; height: calc(100% - 100px); min-width: 800px; padding: 10px 50px; margin-right: auto; margin-left: auto; margin-top: 50px;">
        <h3 class="title">Мастерская</h3>
        <h3>Текущий клиент</h3>
        <div class="active_order">
            <table border="1" id="active_orders">
                <tr><td>Статус</td><td>Телефон</td><td>Время</td></tr>
            </table>
        </div>
        <h3>Список заказов на сегодня</h3>
        <div class="order_list">
            <table border="1" id="order_list">
                <?php
                    include_once 'connect.php';

                    $telephone = $_POST["telephone"];
                    $password = $_POST["password"];
           
                    $check_user = mysqli_query($connect, "SELECT `id` FROM `masters` WHERE `telephone` = '$telephone' AND `password` = '$password'");
                    if ($master = mysqli_fetch_array($check_user, MYSQLI_ASSOC))
                    {
                        $master_id = $master['id'];
                        $date = date("Y-m-d");
                        $result = mysqli_query($connect, "SELECT * FROM `orders` WHERE `master_id` = '$master_id' AND `date` = '$date'");
                        while ($row = mysqli_fetch_array($result, MYSQLI_ASSOC)) {
                            $client_telephone = $row['client_telephone'];
                            $client = mysqli_query($connect, "SELECT `status` FROM `clients` WHERE `telephone` = '$client_telephone'");
                        
                            if ($status = mysqli_fetch_array($client, MYSQLI_ASSOC))
                            {
                                echo "<tr>";
                                echo "<td>" . $status['status'] . "</td>";
                                echo "<td>" . $client_telephone . "</td>";
                                echo "<td id='time'>" . $row['time'] . "</td>";
                                echo "</tr>";
                            }
                        }
                    } else {
                        header("Location: /project/input_master.html");
                    }
                ?>
            </table>
        </div>
    </div>
    <script src="/project/js/workshop.js">
    </script>
    <script>
        let date = new Date();
        date.setDate(date.getDate() - 1);
        deleteOldOrders(date);
        setInterval(cheack, 1000);
    </script>
</body>
</html>