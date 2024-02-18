<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../../connect.php';

$shop_id = $_REQUEST['shop_id'];
$order_id = $_REQUEST['order_id'];

$query = "SELECT * FROM shop WHERE id='$shop_id'";
$result = mysqli_query($con, $query);

$arr = array();
$i = 0;

while($row = mysqli_fetch_array($result))
{
    $arr['Id'] = $row['id'];
    $arr['Name'] = $row['name'];
    $arr['Address'] = $row['address'];
    $arr['Image'] = $row['image'];
    $arr['Promptpay_Number'] = $row['prompt_number'];
    $arr['Promptpay_Name'] = $row['prompt_name'];
}

$queryod = "SELECT * FROM orders WHERE order_id='$order_id'";
$resultod = mysqli_query($con, $queryod);

while($rowod = mysqli_fetch_array($resultod))
{
    $arr['Point'] = "โต๊ะหมายเลข ".$rowod['tables'];
}

echo json_encode($arr);
?>