<?php
include_once 'connect.php';

$base_ansver = $_GET['base_ansver'];

$result = mysqli_query($connect, "SELECT `id`, `adres` FROM `masters`");

if ($result)
{
    echo "<select>";
    echo "<option disabled selected hidden>$base_ansver</option>";
    while ($row = mysqli_fetch_array($result, MYSQLI_ASSOC)) {
        echo "<option value='" . $row['id'] . "'>" . $row['adres'] . "</option>";
    }
    echo "</select>";
}
?>