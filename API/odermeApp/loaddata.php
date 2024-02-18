<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include 'connect.php';

$latitude = $_REQUEST['latitude'];
$longitude = $_REQUEST['longitude'];
$dis = $_REQUEST['distance'];

$query = "SELECT * FROM shop WHERE (((acos(sin(('$latitude'*pi()/180)) * sin((`latitude`*pi()/180)) + cos(('$latitude'*pi()/180)) 
    * cos((`latitude`*pi()/180)) * cos((('$longitude' - `longitude`) * pi()/180)))) * 180/pi()) * 60 * 1.1515 * 1.609344) <= '$dis' AND status=1
    ORDER BY (((acos(sin(('$latitude'*pi()/180)) * sin((`latitude`*pi()/180)) + cos(('$latitude'*pi()/180)) 
    * cos((`latitude`*pi()/180)) * cos((('$longitude' - `longitude`) * pi()/180)))) * 180/pi()) * 60 * 1.1515 * 1.609344) ASC";
$result = mysqli_query($con, $query);

$arr = array();
$i = 0;

while($row = mysqli_fetch_array($result))
{
    $distanceall = getDistanceBetweenPointsNew($latitude, $longitude, $row['latitude'], $row['longitude']);
    $shop_id = $row['id'];
    $arr[$i]['Id'] = $row['id'];
    $arr[$i]['Name'] = $row['name'];
    $arr[$i]['Address'] = $row['address'];
    $arr[$i]['Image'] = $row['image'];
    $arr[$i]['Latitude'] = $row['latitude'];
    $arr[$i]['Longitude'] = $row['longitude'];
    $arr[$i]['Promptpay_Number'] = $row['prompt_number'];
    $arr[$i]['Promptpay_Name'] = $row['prompt_name'];
    if($row['background'] == "")
    {
        $arr[$i]['Background'] = "https://mahamhorr.com/odermeApp/image/app/background.png";
    }
    else
    {
        $arr[$i]['Background'] = $row['background'];
    }
    if($distanceall < 1)
    {
        $str = explode(".", ($distanceall * 1000));
        $arr[$i]['Distance'] = $str[0];
        $arr[$i]['Unit'] = "m";
    }else
    {
        $str = explode(".", $distanceall);
        $arr[$i]['Distance'] = $str[0];
        $arr[$i]['Unit'] = "km";
    }
    $query_m = "SELECT DISTINCT type FROM menu WHERE shop_id='$shop_id' AND status=1";
    $result_m = mysqli_query($con, $query_m);
    $type = array();
    while($row_m = mysqli_fetch_array($result_m))
    {
        $str = explode(",", $row_m['type']);
       for($a=0; $a<count($str); $a++)
       {
           $add = true;
           for($b=0; $b<count($type); $b++)
           {
                if($str[$a] == $type[$b])
                {
                    $add = false;
                }
           }
           if($add == true)
           {
            array_push($type,$str[$a]);
           }
       }
    }

    $query_re = "SELECT * FROM review WHERE shop_id='$shop_id'";
    $result_re = mysqli_query($con, $query_re);
    $starcount = 0;
    $c = 0;
    while($row_re = mysqli_fetch_array($result_re))
    {
        $starcount = $starcount + $row_re['point'];
        $arr[$i]['Review'][$c]['Point'] = $row_re['point'];
        $arr[$i]['Review'][$c]['Comment'] = $row_re['comment'];
        $arr[$i]['Review'][$c]['Star1'] = "star.png";
        $arr[$i]['Review'][$c]['Star2'] = "star.png";
        $arr[$i]['Review'][$c]['Star3'] = "star.png";
        $arr[$i]['Review'][$c]['Star4'] = "star.png";
        $arr[$i]['Review'][$c]['Star5'] = "star.png";

        if($row_re['point'] >= 0.5)
        {
            $arr[$i]['Review'][$c]['Star1'] = "stared.png";
        }
        if($row_re['point'] >= 1.5)
        {
            $arr[$i]['Review'][$c]['Star2'] = "stared.png";
        }
        if($row_re['point'] >= 2.5)
        {
            $arr[$i]['Review'][$c]['Star3'] = "stared.png";
        }
        if($row_re['point'] >= 3.5)
        {
            $arr[$i]['Review'][$c]['Star4'] = "stared.png";
        }
        if($row_re['point'] >= 4.5)
        {
            $arr[$i]['Review'][$c]['Star5'] = "stared.png";
        }

        $c++;
    }
    if($c>0)
    {
        $starcount = $starcount/$c;
        $arr[$i]['PointCount'] = $c;
        $arr[$i]['Point'] = substr($starcount, 0 ,3);
    }
    else
    {
        $arr[$i]['PointCount'] = 0;
        $arr[$i]['Point'] = 0;
    }

    $arr[$i]['Star1'] = "star.png";
    $arr[$i]['Star2'] = "star.png";
    $arr[$i]['Star3'] = "star.png";
    $arr[$i]['Star4'] = "star.png";
    $arr[$i]['Star5'] = "star.png";

    $star = $arr[$i]['Point'];
    if($star >= 0.5)
    {
        $arr[$i]['Star1'] = "stared.png";
    }
    if($star >= 1.5)
    {
        $arr[$i]['Star2'] = "stared.png";
    }
    if($star >= 2.5)
    {
        $arr[$i]['Star3'] = "stared.png";
    }
    if($star >= 3.5)
    {
        $arr[$i]['Star4'] = "stared.png";
    }
    if($star >= 4.5)
    {
        $arr[$i]['Star5'] = "stared.png";
    }

    $j = 0;
    for($a=0; $a<count($type); $a++)
    {
        $query_menu = "SELECT * FROM menu WHERE shop_id='$shop_id' AND status=1";
        $result_menu = mysqli_query($con, $query_menu);
        $k = 0;
        $menu = array();
        while($row_menu = mysqli_fetch_array($result_menu))
        {
            $tp = $row_menu['type'];
            $arr[$i]['MenuGroup'][$j]['Type'] = $type[$a];
            if(strpos($tp, $type[$a]) !== false){
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Id'] = $row_menu['id'];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Shop_Id'] = $row_menu['shop_id'];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Name'] = $row_menu['name'];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Image'] = $row_menu['image'];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Price'] = $row_menu['price'];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['MainType'] = $type[$a];
                $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['Type'] = $row_menu['type'];
                $oth = explode(",", $row_menu['other']);
                $othp = explode(",", $row_menu['otherprice']);
                for($o=0; $o<count($oth); $o++)
                {
                    $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['OtherGroup'][$o]['Name'] = $oth[$o];
                    $arr[$i]['MenuGroup'][$j]['MenuList'][$k]['OtherGroup'][$o]['Price'] = $othp[$o];
                }
                $k++;
            }
        }
        $j++;
    }
    $i++;
}

function getDistanceBetweenPointsNew($latitude1, $longitude1, $latitude2, $longitude2) {
    $theta = $longitude1 - $longitude2; 
    $distance = (sin(deg2rad($latitude1)) * sin(deg2rad($latitude2))) + (cos(deg2rad($latitude1)) * cos(deg2rad($latitude2)) * cos(deg2rad($theta))); 
    $distance = acos($distance); 
    $distance = rad2deg($distance); 
    $distance = $distance * 60 * 1.1515;
    $distance = $distance * 1.609344;  
    return (round($distance,2)); 
  }

echo json_encode($arr);