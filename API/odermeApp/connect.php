<?php
$host = "localhost";
$username = "mahamhor_mhorr";
$password = "Hzn123456+";
$dbname = "mahamhor_oderme";

$con= mysqli_connect($host,$username,$password,$dbname) or die("Error: " . mysqli_error($con));
mysqli_query($con, "SET NAMES 'utf8' ");
//mysqli_set_charset($con, 'utf8');
error_reporting( error_reporting() & ~E_NOTICE );

?>