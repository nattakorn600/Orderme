<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../../connect.php';

$order_id = $_REQUEST['order_id'];

$id = date("YmdHis").rand(100,999);

$typeimg = strrchr($_FILES['file']['name'],".");
$new_user = $id.$typeimg; 
$path_user = "../../image/bill";
$path_copy_user = $path_user."/".$new_user;

$card_img = "https://mahamhorr.com/odermeApp/image/bill/".$new_user;

$ori_file = $path_user.$_FILES['file']['name'];
$ext = strtolower(end(explode('.', $_FILES['file']['name'])));
if ($ext == "jpg" or $ext == "jpeg" or $ext == "png" or $ext=="gif") {
    copy($_FILES['file']['tmp_name'], $path_copy_user);
}

$sql1 = "UPDATE orders SET payment='$card_img',status=1 WHERE id='$order_id'";
if ($con->query($sql1) === TRUE) {
    $status = "success";
} else {
    $status = "fail";
}
$arr['Status'] = $status;
echo json_encode($arr);
?>