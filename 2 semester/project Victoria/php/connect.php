<?php
$connect = mysqli_connect('localhost', 'root', 'toor', 'salon');
if ($connect == false){
    print("Ошибка: Невозможно подключиться к MySQL " . mysqli_connect_error());
}
?>