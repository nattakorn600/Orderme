<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$us_id = $_REQUEST['shop_id'];
$order = $_REQUEST['order_id'];

if($_REQUEST['order_id'])
{
    $sqls = "UPDATE orders SET status=4 WHERE order_id='$order'";
    if ($con->query($sqls) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
}

$arr = array();

$query = "SELECT * FROM orders WHERE status=3 AND shop_id='$us_id' ORDER BY id ASC";
$result = mysqli_query($con, $query);
$count = mysqli_num_rows($result);
if($count > 0)
{
    $row = mysqli_fetch_array($result);
}else{
    $query2 = "SELECT * FROM orders WHERE status=2 AND shop_id='$us_id' ORDER BY id ASC";
    $result2 = mysqli_query($con, $query2);
    $row = mysqli_fetch_array($result2);
}
$order_id = $row['order_id'];
if($order_id != "")
{
    $arr['Id'] = $row['id'];
    $arr['Order_Id'] = $order_id;
    $arr['Shop_Id'] = $row['shop_id'];
    $arr['Price'] = $row['price'];
    $arr['Payment'] = $row['payment'];
    $arr['Table'] = $row['tables'];
    $arr['Wait'] = $i+1;
    $arr['Date'] = $row['date'];
    $arr['Status'] = $row['status'];
    
    $query_mo = "SELECT * FROM menu_order WHERE order_id='$order_id'";
    $result_mo = mysqli_query($con, $query_mo);
    $j = 0;
    
    while($row_mo = mysqli_fetch_array($result_mo))
    {
        $menu_id = $row_mo['menu_id'];
        $query_m = "SELECT * FROM menu WHERE id='$menu_id'";
        $result_m = mysqli_query($con, $query_m);
        $row_m = mysqli_fetch_array($result_m);
    
        $arr['Menus'][$j]['Id'] = $j;
        $arr['Menus'][$j]['Name'] = $row_m['name'];
        $arr['Menus'][$j]['Price'] = $row_m['price'];
        $arr['Menus'][$j]['Image'] = $row_m['image'];
        $arr['Menus'][$j]['Type'] = $row_m['type'];
        $oth = explode(",",$row_mo['other']);
        $othp = explode(",",$row_mo['other_price']);
        $arr['Menus'][$j]['VisibleOther'] = false;
        $arr['Menus'][$j]['VisibleArrow'] = false;
        $arr['Menus'][$j]['Rotation'] = 90;
        if($row_mo['other'] != "")
        {
            $arr['Menus'][$j]['VisibleArrow'] = true;
        }
        if(count($oth) > 0)
        {
            for($x=0; $x<count($oth); $x++)
            {
                $arr['Menus'][$j]['OtherGroup'][$x]['Name'] = $oth[$x];
                $arr['Menus'][$j]['OtherGroup'][$x]['Price'] = $othp[$x];
            }
        }
        $arr['Menus'][$j]['Other'] = $row_mo['other'];
        $arr['Menus'][$j]['Other_Price'] = $row_mo['other_price'];
        $arr['Menus'][$j]['Number'] = $row_mo['number'];
        
        $total = $row_m['price'];
        $tot = explode(",", $row_mo['other_price']);
        for($o=0; $o<count($tot); $o++)
            {
                $total = $total + $tot[$o];
            }
        $total = $total * $row_mo['number'];
        $arr['Menus'][$j]['Total'] = $total;
        $j++;
    }
    
    $sql = "UPDATE orders SET status=3 WHERE order_id='$order_id'";
    if ($con->query($sql) === TRUE) {
        $status = "success";
    } else {
        $status = "fail";
    }
}

echo json_encode($arr);
?>