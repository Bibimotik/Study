<?php
include_once 'connect.php';

$telephone = $_POST["telephone"];
$adres = $_POST["adres"];
$review = $_POST["review"];

$check_user = mysqli_query($connect, "SELECT * FROM `users` WHERE `telephone` = '$telephone'");
if (mysqli_num_rows($check_user) > 0)
{
	print("<br>Пользователь существует");
	$check_master = mysqli_query($connect, "SELECT * FROM `masters` WHERE `id` = '$adres'");
	if (mysqli_num_rows($check_master) > 0)
	{
		print("<br>Мастер существует");
		$master_id = mysqli_fetch_array($check_master, MYSQLI_ASSOC)["id"];
		$result = mysqli_query($connect, "INSERT INTO `reviews` (`master_id`, `user_telephone`, `assessment`, `review`) VALUES ('$master_id', '$telephone', '0', '$review')");
		if ($result)
		{
			print("<br>Отзыв отправлен");
			header("Location: http://localhost/project/thanks_for_review.html");
		}
	} else {
		print("<br>Данный мастер не зарегестрироват");
	}
} else {
	print("<br>Данный пользователь не зарегестрироват");
}
?>