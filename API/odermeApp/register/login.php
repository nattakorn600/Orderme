<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$email = $_REQUEST['email'];
$s_id = $_REQUEST['id'];
$name = $_REQUEST['name'];
$pic = $_REQUEST['picture'];
$type = $_REQUEST['type'];

$sql = "SELECT * FROM user WHERE social_id='$s_id'";
$result = mysqli_query($con,$sql);

$arr = array();
$rowsuser = mysqli_num_rows($result);
if($rowsuser<=0){
    $sql_r = "INSERT INTO user (name,email,social_id,picture,type) VALUES ('$name','$email','$s_id','$pic','$type')";
    if ($con->query($sql_r) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
}
else{
    $sql_r = "UPDATE user SET name='$name', picture='$pic' WHERE social_id='$s_id'";
    if ($con->query($sql_r) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
}
$result_u = mysqli_query($con,$sql);
$row = mysqli_fetch_array($result_u);

$arr['Id'] = $row['id'];
$arr['Name'] = $row['name'];
$arr['Social_Id'] = $row['social_id'];
$arr['Email'] = $row['email'];
$arr['Phone'] = $row['phone'];
$arr['Picture'] = $row['picture'];
$arr['Shop_Id'] = $row['shop_id'];
$arr['Type'] = $row['type'];
$arr['Status'] = $status;

echo json_encode($arr);
?>