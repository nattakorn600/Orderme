<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$name = $_REQUEST['name'];
$address = $_REQUEST['address'];
$latitude = $_REQUEST['latitude'];
$longitude = $_REQUEST['longitude'];
$prompt_number = $_REQUEST['prompt_number'];
$prompt_name = $_REQUEST['prompt_name'];
$user_id = $_REQUEST['user_id'];

$sql = "INSERT INTO shop (name,address,latitude,longitude,prompt_number,prompt_name,user_id,status) VALUES 
('$name','$address','$latitude','$longitude','$prompt_number','$prompt_name','$user_id',1)"; 

$arr = array();

if ($con->query($sql) === TRUE) {
    $query = "SELECT * FROM shop WHERE user_id=$user_id";
    $result = mysqli_query($con, $query);
    $row = mysqli_fetch_array($result);
    $shop_id = $row['id'];

    $sqq = "UPDATE user SET shop_id='$shop_id' WHERE id=$user_id";
    $result_u = mysqli_query($con, $sqq);

    $status = "success";
} else {
    $status = "fail";
}

$arr['Shop_Id'] = $shop_id;
$arr['Status'] = $status;

echo json_encode($arr);