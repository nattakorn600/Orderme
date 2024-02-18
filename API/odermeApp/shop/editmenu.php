<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$menu_id = $_REQUEST['menu_id'];
$other= $_REQUEST['other'];
$otherprice = $_REQUEST['otherprice'];


$sql = "UPDATE menu SET other='$other',otherprice='$otherprice' WHERE id='$menu_id'"; 
   $arr = array();
    if ($con->query($sql) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
$arr['Status'] = $status;
echo json_encode($arr);
?>