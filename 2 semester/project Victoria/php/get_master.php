<?php
require_once "connect.php";

$telephone = $_POST["telephone"];
$password = $_POST["password"];

$check_user = mysqli_query($connect, "SELECT `id` FROM `masters` WHERE `telephone` = '$telephone' AND `password` = '$password'");
if ($row = mysqli_fetch_array($check_user, MYSQLI_ASSOC))
{
	echo $row['id'];
} else {
	echo "-1";
}
?>