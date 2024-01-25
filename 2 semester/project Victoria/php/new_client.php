<!DOCTYPE html>
<html lang="ru-RU">
<head>
	<meta charset="UTF-8">
	<link media="all" href="css/form_styles.css" rel="stylesheet">
	<link media="all" href="css/order.css" rel="stylesheet">
	<title>Новый клиент</title>
</head>
<body>
	<?php
	require_once "connect.php";

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
					window.location.href = "index.html";
				}
			</script>
        </div>
    </div>
</body>
</html>