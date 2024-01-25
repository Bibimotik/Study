<?php
include_once 'connect.php';

$full_name = $_POST["full_name"];
$adres = $_POST["adres"];
$telephone = $_POST["telephone"];
$password = $_POST["password"];

$open_time = $_POST["open_time"];
$close_time = $_POST["close_time"];
$step = $_POST["step"];

$result = mysqli_query($connect, "INSERT INTO `masters` (`id`, `full_name`, `telephone`, `password`, `adres`, `open_time`, `close_time`, `step`) VALUES (NULL, '$full_name', '$telephone', '$password', '$adres', '$open_time:00', '$close_time:00', '$step:00')");
if (!$result) {
	print("Ошибка регистрации");
}
header("Location: /project/index.html");
?>