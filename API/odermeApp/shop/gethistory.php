<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$shop_id = $_REQUEST['shop_id'];

$arr = array();
$mounts = array('ม.ค.','ก.พ.','มี.ค.','เม.ย.','พ.ค.','มิ.ย.','ก.ค.','ส.ค.','ก.ย.','ต.ค.','พ.ย.','ธ.ค.');

$querye = "SELECT * FROM orders WHERE status>=4 AND shop_id='$shop_id'  ORDER BY date DESC";
$resulte = mysqli_query($con, $querye);
$i = 0;
$d = -1;
$last = "";
while($rowe = mysqli_fetch_array($resulte))
{
    $last = explode(" ", $rowe['date']);
    if($last[0] != $old)
    {
        $d++;
        $i = 0;
        $dat = explode("-", $last[0]);
        $mount = $mounts[$dat[1]-1];
        $arr[$d]['Date'] = $dat[2]." ".$mount." ".($dat[0]+543);
    }
    $old = $last[0];
    $order_id = $rowe['order_id'];
    $arr[$d]['Menus'][$i]['Order_Id'] = $rowe['order_id'];
    $dt = explode(" ", $rowe['date']);
    $dat = explode("-", $dt[0]);
    $tim = explode(":", $dt[1]);
    $mount = $mounts[$dat[1]-1];
    $arr[$d]['Menus'][$i]['Date'] = $dat[2]." ".$mount." ".($dat[0]+543)." - ".$tim[0].".".$tim[1]." น.";
    $arr[$d]['Menus'][$i]['Price'] = $rowe['price'];
    $arr[$d]['Menus'][$i]['Table'] = $rowe['tables'];
    $arr[$d]['Menus'][$i]['Payment'] = "Promptpay";
    $query_mo = "SELECT * FROM menu_order WHERE order_id='$order_id'";
    $result_mo = mysqli_query($con, $query_mo);
    $arr[$d]['Menus'][$i]['Number'] = mysqli_num_rows($result_mo);
    $arr[$d]['Menus'][$i]['Rotation'] = 90;
    $arr[$d]['Menus'][$i]['Visible'] = false;
    $i++;
}
   
/*$query = "SELECT * FROM orders WHERE status=5 AND user_id='$shop_id' ORDER BY id DESC";
$result = mysqli_query($con, $query);


while($row = mysqli_fetch_array($result))
{
    $order_id = $row['order_id'];
    $arr[$i]['Id'] = $row['id'];
    $arr[$i]['Order_Id'] = $order_id;

    $sid = $row['user_id'];
    $query_s = "SELECT * FROM user WHERE id='$sid'";
    $result_s = mysqli_query($con, $query_s);
    $row_s = mysqli_fetch_array($result_s);
    $arr[$i]['Shop_Id'] = $row['user_id'];
    $arr[$i]['Shop_Name'] = $row_s['name'];
    $arr[$i]['Price'] = $row['price'];

    $dt = explode(" ", $row['date']);
    $dat = explode("-", $dt[0]);
    $tim = explode(":", $dt[1]);
    $mount = $mounts[$dat[1]-1];

    $arr[$i]['Date'] = $dat[2]." ".$mount." ".$dat[0]." ".$tim[0].".".$tim[1]." น.";
    $arr[$i]['Status'] = $row['status'];

    $query_mo = "SELECT * FROM menu_order WHERE order_id='$order_id'";
    $result_mo = mysqli_query($con, $query_mo);
    $j = 0;
    $menutext = "";
    while($row_mo = mysqli_fetch_array($result_mo))
    {
        $menu_id = $row_mo['menu_id'];
        $query_m = "SELECT * FROM menu WHERE id='$menu_id'";
        $result_m = mysqli_query($con, $query_m);
        $row_m = mysqli_fetch_array($result_m);
        if($menutext == ""){
            $menutext = $row_m['name'];
        }else{
            $menutext = $menutext.", ".$row_m['name'];
        }
        $arr[$i]['Menus'][$j]['Id'] = $row_m['id'];
        $arr[$i]['Menus'][$j]['Name'] = $row_m['name'];
        $arr[$i]['Menus'][$j]['Price'] = $row_m['price'];
        $arr[$i]['Menus'][$j]['Image'] = $row_m['image'];
        $arr[$i]['Menus'][$j]['Type'] = $row_m['type'];
        $arr[$i]['Menus'][$j]['Other'] = $row_mo['other'];
        $arr[$i]['Menus'][$j]['Other_Price'] = $row_mo['other_price'];
        $arr[$i]['Menus'][$j]['Number'] = $row_mo['number'];
        
        $total = $row_m['price'];
        $tot = explode(",", $row_mo['other_price']);
        for($o=0; $o<count($tot); $o++)
            {
                $total = $total + $tot[$o];
            }
        $total = $total * $row_mo['number'];
        $arr[$i]['Menus'][$j]['Total'] = $total;
        $j++;
    }
    $arr[$i]['MenusText'] = $menutext;
    $i++;
}*/

echo json_encode($arr);
?>