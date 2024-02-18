<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$query = "SELECT * FROM map";
$result1 = mysqli_query($con, $query);

$arr = array();
$i = 0;

while($row = mysqli_fetch_array($result1)){
    $query1 = "SELECT * FROM shop";
    $result = mysqli_query($con, $query1);
    while($row1 = mysqli_fetch_array($result))
    {
        if($row1['shop_id'] == $row['shop_id'])
        {
            $shop_img = $row1['shop_img'];
            $shop_name = $row1['shop_name'];
        }
    }

    $arr[$i]['latitude'] = $row['latitude'];
    $arr[$i]['longtitude'] = $row['longtitude'];
    $arr[$i]['img'] = $shop_img;
    $arr[$i]['name'] = $shop_name;
    $arr[$i]['address'] = $row['address'];
    $arr[$i]['shop_id'] = $row['shop_id'];
    $i++;   
}

mysqli_close($con);
echo json_encode($arr);
?>