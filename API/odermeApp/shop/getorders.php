<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$us_id = $_REQUEST['shop_id'];

$arr = array();
$y = 0; 
while($y <= 1)
{
    if($y == 0)
    {
        $query = "SELECT * FROM orders WHERE status=1 AND shop_id='$us_id' ORDER BY id ASC";
    }
    else if($y == 1)
    {
        $query = "SELECT * FROM orders WHERE status=2 AND shop_id='$us_id' ORDER BY id ASC";
    }
    
    $result = mysqli_query($con, $query);
    $arr[$y] = array();
    $i = 0;

    if($y == 1)
    {
        $query_3 = "SELECT * FROM orders WHERE status=3 AND shop_id='$us_id' ORDER BY id DESC";
        $result_3 = mysqli_query($con, $query_3);
        $count = mysqli_num_rows($result_3);
        if($count > 0)
        {
            $row_3 = mysqli_fetch_array($result_3);
            $order_id = $row_3['order_id'];
            $arr[$y][$i]['Id'] = $row_3['id'];
            $arr[$y][$i]['Order_Id'] = $order_id;
            $arr[$y][$i]['Shop_Id'] = $row_3['shop_id'];
            $arr[$y][$i]['Price'] = $row_3['price'];
            $arr[$y][$i]['Payment'] = $row_3['payment'];
            $arr[$y][$i]['Table'] = $row_3['tables'];
            $arr[$y][$i]['Wait'] = $i+1;
            $arr[$y][$i]['Date'] = $row_3['date'];
            $arr[$y][$i]['Status'] = $row_3['status'];
    
            $query_mos = "SELECT * FROM menu_order WHERE order_id='$order_id'";
            $result_mos = mysqli_query($con, $query_mos);
            $j = 0;
    
            while($row_mos = mysqli_fetch_array($result_mos))
            {
                $menu_id = $row_mos['menu_id'];
                $query_ms = "SELECT * FROM menu WHERE id='$menu_id'";
                $result_ms = mysqli_query($con, $query_ms);
                $row_ms = mysqli_fetch_array($result_ms);
    
                $arr[$y][$i]['Menus'][$j]['Id'] = $row_ms['id'];
                $arr[$y][$i]['Menus'][$j]['Name'] = $row_ms['name'];
                $arr[$y][$i]['Menus'][$j]['Price'] = $row_ms['price'];
                $arr[$y][$i]['Menus'][$j]['Image'] = $row_ms['image'];
                $arr[$y][$i]['Menus'][$j]['Type'] = $row_ms['type'];
                $arr[$y][$i]['Menus'][$j]['Other'] = $row_mos['other'];
                $arr[$y][$i]['Menus'][$j]['Other_Price'] = $row_mos['other_price'];
                $arr[$y][$i]['Menus'][$j]['Number'] = $row_mos['number'];
                
                $total = $row_ms['price'];
                $tot = explode(",", $row_mos['other_price']);
                for($o=0; $o<count($tot); $o++)
                    {
                        $total = $total + $tot[$o];
                    }
                $total = $total * $row_mos['number'];
                $arr[$y][$i]['Menus'][$j]['Total'] = $total;
                $j++;
            }
            $i++;
        }
    }
    
    while($row = mysqli_fetch_array($result))
    {     
        $order_id = $row['order_id'];
        $arr[$y][$i]['Id'] = $row['id'];
        $arr[$y][$i]['Order_Id'] = $order_id;
        $arr[$y][$i]['Shop_Id'] = $row['shop_id'];
        $arr[$y][$i]['Price'] = $row['price'];
        $arr[$y][$i]['Payment'] = $row['payment'];
        $arr[$y][$i]['Table'] = $row['tables'];
        $arr[$y][$i]['Wait'] = $i+1;
        $arr[$y][$i]['Date'] = $row['date'];
        $arr[$y][$i]['Status'] = $row['status'];

        $query_mo = "SELECT * FROM menu_order WHERE order_id='$order_id'";
        $result_mo = mysqli_query($con, $query_mo);
        $j = 0;

        while($row_mo = mysqli_fetch_array($result_mo))
        {
            $menu_id = $row_mo['menu_id'];
            $query_m = "SELECT * FROM menu WHERE id='$menu_id'";
            $result_m = mysqli_query($con, $query_m);
            $row_m = mysqli_fetch_array($result_m);

            $arr[$y][$i]['Menus'][$j]['Id'] = $row_m['id'];
            $arr[$y][$i]['Menus'][$j]['Name'] = $row_m['name'];
            $arr[$y][$i]['Menus'][$j]['Price'] = $row_m['price'];
            $arr[$y][$i]['Menus'][$j]['Image'] = $row_m['image'];
            $arr[$y][$i]['Menus'][$j]['Type'] = $row_m['type'];
            $arr[$y][$i]['Menus'][$j]['Other'] = $row_mo['other'];
            $arr[$y][$i]['Menus'][$j]['Other_Price'] = $row_mo['other_price'];
            $arr[$y][$i]['Menus'][$j]['Number'] = $row_mo['number'];
            
            $total = $row_m['price'];
            $tot = explode(",", $row_mo['other_price']);
            for($o=0; $o<count($tot); $o++)
                {
                    $total = $total + $tot[$o];
                }
            $total = $total * $row_mo['number'];
            $arr[$y][$i]['Menus'][$j]['Total'] = $total;
            $j++;
        }
        $i++;
    }
    $y++;
}

echo json_encode($arr);
?>