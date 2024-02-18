<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$user_id = $_REQUEST['user_id'];
$shop_id = $_REQUEST['shop_id'];
$menu = json_decode($_REQUEST['menu'], true);
$price = $_REQUEST['price'];
$table = $_REQUEST['table'];

$id = date("YmdHis").rand(100,999);

$typeimg = strrchr($_FILES['file']['name'],".");
$new_user = $id.$typeimg; 
$path_user = "../image/bill";
$path_copy_user = $path_user."/".$new_user;

$card_img = "https://mahamhorr.com/odermeApp/image/bill/".$new_user;

$ori_file = $path_user.$_FILES['file']['name'];
$ext = strtolower(end(explode('.', $_FILES['file']['name'])));
if ($ext == "jpg" or $ext == "jpeg" or $ext == "png" or $ext=="gif") {
    copy($_FILES['file']['tmp_name'], $path_copy_user);
}

$sql_o = "INSERT INTO orders (order_id,shop_id,user_id,payment,price,tables,status) VALUES ('$id','$shop_id','$user_id','$card_img','$price','$table',1)"; 

if ($con->query($sql_o) === TRUE) {
    for($i=0; $i<count($menu); $i++)
    {
        $menuid = $menu[$i]['Id'];
        $menunumber = $menu[$i]['Number'];
        $menuother = "";
        $priceother = "";
        if(count($menu[$i]['OtherGroup']) > 0)
        {
            for($j=0; $j<count($menu[$i]['OtherGroup']); $j++)
            {
                if($j==0)
                {
                    $menuother = $menuother.$menu[$i]['OtherGroup'][$j]['Name'];
                    $priceother = $priceother.$menu[$i]['OtherGroup'][$j]['Price'];
                }
                else
                {
                    $menuother = $menuother.",".$menu[$i]['OtherGroup'][$j]['Name'];
                    $priceother = $priceother.",".$menu[$i]['OtherGroup'][$j]['Price'];
                }
            }
        }
        $sql_m = "INSERT INTO menu_order (order_id,menu_id,other,other_price,number) VALUES ('$id','$menuid','$menuother','$priceother','$menunumber')"; 
        mysqli_query($con, $sql_m);
    }
    $status = "success";
} else {
    $status = "fail";
}

$arr = array();

$arr['Status'] = $status;

echo json_encode($arr);
?>