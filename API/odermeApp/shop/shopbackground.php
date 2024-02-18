<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

function imageResize($ext, $ori_file, $new_file, $max_imageSize)
    {
        $ori_size = getimagesize($ori_file);
        $ori_w = $ori_size[0];
        $ori_h = $ori_size[1];
         
        if($ori_w > $ori_h) {
            $new_w = $max_imageSize;
            $new_h = round(($new_w/$ori_w) * $ori_h);
        }
        else
        {
            $new_h = $max_imageSize;
            $new_w = round(($new_h/$ori_h) * $ori_w);
        }
     
        if ($ext == "jpg" or $ext == "jpeg") {
            $ori_img = imagecreatefromjpeg($ori_file);
        } else
        if ($ext == "png") {
            $ori_img = imagecreatefrompng($ori_file);
        } else
        if ($ext == "gif") {
            $ori_img = imagecreatefromgif($ori_file);
        } 
 
        $new_img = imagecreatetruecolor($new_w, $new_h);
        imagecopyresized($new_img, $ori_img, 0, 0, 0, 0, $new_w, $new_h, $ori_w, $ori_h);
        if ($ext == "jpg" or $ext == "jpeg") {
            imagejpeg($new_img, $new_file); 
        } else
        if ($ext == "png") {
            imagepng($new_img, $new_file); 
        } else
        if ($ext == "gif") {
            imagegif($new_img, $new_file); 
        }
         
        imagedestroy($ori_img);
        imagedestroy($new_img);
    }

$user_id = $_REQUEST['user_id'];

$time = date("YmdHis");	

$typeimg = strrchr($_FILES['file']['name'],".");
$new_user = $time.$typeimg; 
$path_user = "../image/shop/";
$path_copy_user = $path_user.$new_user;

$card_img = "https://mahamhorr.com/odermeApp/image/shop/".$new_user;

$ori_file = $path_user.$_FILES['file']['name'];
$ext = strtolower(end(explode('.', $_FILES['file']['name'])));
if ($ext == "jpg" or $ext == "jpeg" or $ext == "png" or $ext=="gif") {
    //copy($_FILES['file']['tmp_name'], $path_copy_user);
    copy($_FILES['file']['tmp_name'], $ori_file);
    imageResize($ext, $ori_file, $path_copy_user, 1080);
    unlink($ori_file); 
}

$sql = "UPDATE shop SET background='$card_img' WHERE user_id='$user_id'";
    $arr = array();
    if ($con->query($sql) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
$arr['Status'] = $status;
echo json_encode($arr);
?>