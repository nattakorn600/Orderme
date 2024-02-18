<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$shop_id = $_REQUEST['shop_id'];
$stat = $_REQUEST['status'];

$sql = "UPDATE shop SET status='$stat' WHERE id='$shop_id'";
   $arr = array();
    if ($con->query($sql) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
$arr['Status'] = $status;
echo json_encode($arr);
?>