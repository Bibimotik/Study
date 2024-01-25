<?php
include_once 'connect.php';

$master_id = $_POST['master_id'];
$date = $_POST['date'];

$result = mysqli_query($connect, "SELECT `open_time`, `close_time`, `step` FROM `masters` WHERE `id` = '$master_id'");
if ($row = mysqli_fetch_array($result, MYSQLI_ASSOC))
{
	echo $row['open_time'] . " " . $row['close_time'] . " " . $row['step'] . " orders:";

	$orders = mysqli_query($connect, "SELECT `time` FROM `orders` WHERE `master_id` = '$master_id' AND `date` = '$date'");
	while ($order = mysqli_fetch_array($orders, MYSQLI_ASSOC)) {
		echo " " . $order['time'];
	}
}
?>