<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$menu_id = $_REQUEST['menu_id'];

   /*$sql = "DELETE FROM menu WHERE id='$menu_id'"; 
   $arr = array();
    if ($con->query($sql) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }*/

$sql = "UPDATE menu SET shop_id=NULL WHERE id='$menu_id'"; 
$arr = array();
if ($con->query($sql) === TRUE) {
    $status = "success";
} else {
    $status = "fail";
}

$arr['Status'] = $status;
echo json_encode($arr);
?>