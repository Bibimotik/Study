<!DOCTYPE html>
<html lang="ru-RU">
<head>
	<meta charset="UTF-8">
	<link rel="shortcut icon" href="/project/images/icons/favicon.ico">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link media="all" href="/project/css/form_styles.css" rel="stylesheet">
	<link media="all" href="/project/css/order.css" rel="stylesheet">
	<title>Заказ оформлен</title>
</head>
<body>
	<?php
	require_once "connect.php";

	$telephone = $_POST["telephone"];
	$master_id = $_POST["adres"];
	$date = $_POST["date"];
	$time = $_POST["time"];

	$check_user = mysqli_query($connect, "SELECT * FROM `clients` WHERE `telephone` = '$telephone'");
	
	if (mysqli_num_rows($check_user) == 0) {
		mysqli_query($connect, "INSERT INTO `clients` (`id`, `status`, `telephone`) VALUES ('0', '0', '$telephone');");
	}
	mysqli_query($connect, "INSERT INTO `orders` (`master_id`, `client_telephone`, `date`, `time`) VALUES ('$master_id', '$telephone', '$date', '$time:00');");
	?>

	<div class="main_region">
        <div class="header">
            <h3 class="title">Заказ оформлен</h3>
        </div>
        <div class="page">
            <p>Вы записаны на стрижку в парикмахерской на время <?php echo $time; echo " " . $date; ?></p>
			<p>При неявке к месту стрижки без отменения заказа ваш рейтинг понизится</p>
			<button class="visuale_button exit_button" id="exit_button">Главная страница</button>
			<script>
				let exit_button = document.getElementById("exit_button");
				exit_button.onclick = function () {
					window.location.href = "/project/index.html";
				}
			</script>
        </div>
    </div>
</body>
</html>