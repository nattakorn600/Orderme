<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$shop_id = $_REQUEST['shop_id'];

$query = "SELECT * FROM shop WHERE id='$shop_id'";
$result = mysqli_query($con, $query);

$arr = array();

while($row = mysqli_fetch_array($result))
{
    $shop_id = $row['id'];
    $arr['Id'] = $row['id'];
    $arr['Name'] = $row['name'];
    $arr['Address'] = $row['address'];
    $arr['Image'] = $row['image'];
    $arr['Latitude'] = $row['latitude'];
    $arr['Longitude'] = $row['longitude'];
    $arr['Promptpay_Number'] = $row['prompt_number'];
    $arr['Promptpay_Name'] = $row['prompt_name'];
    $arr['Promptpay_Name'] = $row['prompt_name'];
    if($row['status'] == 1){
        $arr['Status'] = true;
    }else{
        $arr['Status'] = false;
    }
    if($row['background'] == "")
    {
        $arr['Background'] = "https://mahamhorr.com/odermeApp/image/app/background.png";
    }
    else
    {
        $arr['Background'] = $row['background'];
    }
    $query_re = "SELECT * FROM review WHERE shop_id='$shop_id'";
    $result_re = mysqli_query($con, $query_re);
    $starcount = 0;
    $c = 0;
    while($row_re = mysqli_fetch_array($result_re))
    {
        $starcount = $starcount + $row_re['point'];
        $arr['Review'][$c]['Point'] = $row_re['point'];
        $arr['Review'][$c]['Comment'] = $row_re['comment'];
        $arr['Review'][$c]['Star1'] = "star.png";
        $arr['Review'][$c]['Star2'] = "star.png";
        $arr['Review'][$c]['Star3'] = "star.png";
        $arr['Review'][$c]['Star4'] = "star.png";
        $arr['Review'][$c]['Star5'] = "star.png";

        if($row_re['point'] >= 0.5)
        {
            $arr['Review'][$c]['Star1'] = "stared.png";
        }
        if($row_re['point'] >= 1.5)
        {
            $arr['Review'][$c]['Star2'] = "stared.png";
        }
        if($row_re['point'] >= 2.5)
        {
            $arr['Review'][$c]['Star3'] = "stared.png";
        }
        if($row_re['point'] >= 3.5)
        {
            $arr['Review'][$c]['Star4'] = "stared.png";
        }
        if($row_re['point'] >= 4.5)
        {
            $arr['Review'][$c]['Star5'] = "stared.png";
        }

        $c++;
    }
    if($c>0)
    {
        $starcount = $starcount/$c;
        $arr['PointCount'] = $c;
        $arr['Point'] = substr($starcount, 0 ,3);
    }
    else
    {
        $arr['PointCount'] = 0;
        $arr['Point'] = 0;
    }

    $arr['Star1'] = "star.png";
    $arr['Star2'] = "star.png";
    $arr['Star3'] = "star.png";
    $arr['Star4'] = "star.png";
    $arr['Star5'] = "star.png";
    if($star >= 0.5)
    {
        $arr['Star1'] = "stared.png";
    }
    if($star >= 1.5)
    {
        $arr['Star2'] = "stared.png";
    }
    if($star >= 2.5)
    {
        $arr['Star3'] = "stared.png";
    }
    if($star >= 3.5)
    {
        $arr['Star4'] = "stared.png";
    }
    if($star >= 4.5)
    {
        $arr['Star5'] = "stared.png";
    }
    $query_menu = "SELECT * FROM menu WHERE shop_id='$shop_id'";
    $result_menu = mysqli_query($con, $query_menu);
    $k = 0;
    $menu = array();
    while($row_menu = mysqli_fetch_array($result_menu))
    {
        $arr['MenuList'][$k]['Id'] = $row_menu['id'];
        $arr['MenuList'][$k]['Shop_Id'] = $row_menu['shop_id'];
        $arr['MenuList'][$k]['Name'] = $row_menu['name'];
        $arr['MenuList'][$k]['Image'] = $row_menu['image'];
        $arr['MenuList'][$k]['Price'] = $row_menu['price'];
        $arr['MenuList'][$k]['Type'] = $row_menu['type'];
        if($row_menu['status'] == 1){
            $arr['MenuList'][$k]['Status'] = True;
        }else{
            $arr['MenuList'][$k]['Status'] = False;
        }
        $oth = explode(",", $row_menu['other']);
        $othp = explode(",", $row_menu['otherprice']);
        for($o=0; $o<count($oth); $o++)
        {
            $arr['MenuList'][$k]['OtherGroup'][$o]['Name'] = $oth[$o];
            $arr['MenuList'][$k]['OtherGroup'][$o]['Price'] = $othp[$o];
        }
        $k++;
    }
    $i++;
}

echo json_encode($arr);