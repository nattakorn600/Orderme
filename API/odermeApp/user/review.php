<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$us_id = $_REQUEST['user_id'];
$shop_id = $_REQUEST['shop_id'];
$order_id = $_REQUEST['order_id'];
$comment = $_REQUEST['comment'];
$val = $_REQUEST['value'];

$sql = "INSERT INTO review (point,comment,order_id,shop_id,user_id) VALUES ('$val','$comment','$order_id','$shop_id','$us_id')"; 

if ($con->query($sql) === TRUE) {
    $sql1 = "UPDATE orders SET status=5 WHERE id='$order_id'";
    if ($con->query($sql1) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
} else {
    $status = "fail";
}

$arr = array();

$arr['Status'] = $status;

echo json_encode($arr);
?>