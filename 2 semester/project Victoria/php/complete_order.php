<?php
require_once "connect.php";

$date = $_POST["date"];
$time = $_POST["time"];

$order = mysqli_query($connect, "SELECT `client_telephone` FROM `orders` WHERE `date` = '$date' AND `time` = '$time'");
if ($result = mysqli_query($connect, "DELETE FROM `orders` WHERE `date` = '$date' AND `time` = '$time'"))
{
	$row = mysqli_fetch_array($order, MYSQLI_ASSOC);
	$client_telephone = $row['client_telephone'];
	
	$client = mysqli_query($connect, "SELECT `status` FROM `clients` WHERE `telephone` = '$client_telephone'");
	if ($row_in = mysqli_fetch_array($client, MYSQLI_ASSOC)) {
		$new_status = $row_in['status'] + 1;
		mysqli_query($connect, "UPDATE `clients` SET `status` = '$new_status' WHERE `telephone` = '$client_telephone'");
	}
}
?>