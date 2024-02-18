<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
include '../connect.php';

$us_id = $_REQUEST['user_id'];

$arr = array();
$x = 0; 
while($x <= 4)
{
    if($x <= 1)
    {
        $query = "SELECT * FROM orders WHERE status<2 AND user_id='$us_id' ORDER BY status ASC";
        $y = 0;
    }
    else if($x == 2)
    {
        $query = "SELECT DISTINCT shop_id FROM orders WHERE status=2 AND user_id='$us_id' ORDER BY id ASC";
        $result = mysqli_query($con, $query);
        $y = 1;
        $arr[$y] = array();
        while($row = mysqli_fetch_array($result))
        {
            $sid = $row['shop_id'];
            $query_s = "SELECT * FROM orders WHERE status=2 AND shop_id='$sid' ORDER BY id ASC";
            $result_s = mysqli_query($con, $query_s);
            $i = 0;
            $k = 1;
            while($row_s = mysqli_fetch_array($result_s))
            {
                if($row_s['user_id'] == $us_id)
                {   
                    $order_id = $row_s['order_id'];
                    $arr[$y][$i]['Id'] = $row_s['id'];
                    $arr[$y][$i]['Order_Id'] = $order_id;
                    $arr[$y][$i]['Shop_Id'] = $row_s['shop_id'];
                    $arr[$y][$i]['Price'] = $row_s['price'];
                    $arr[$y][$i]['Wait'] = $k;
                    $arr[$y][$i]['Date'] = $row_s['date'];
                    $arr[$y][$i]['Status'] = $row_s['status'];

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
                $k++;
            }
        }
    }
    else if($x == 3)
    {
        $query = "SELECT * FROM orders WHERE status=3 AND user_id='$us_id' ORDER BY id ASC";
        $y = 2;
    }
    else if($x == 4)
    {
        $query = "SELECT * FROM orders WHERE status=4 AND user_id='$us_id' ORDER BY id ASC";
        $y = 3;
    }

    if($x != 2)
    {
        $result = mysqli_query($con, $query);
        $arr[$y] = array();
        $i = 0;
    
        while($row = mysqli_fetch_array($result))
        {
            if($row['user_id'] == $us_id)
            {      
                $order_id = $row['order_id'];
                $arr[$y][$i]['Id'] = $row['id'];
                $arr[$y][$i]['Order_Id'] = $order_id;
                $arr[$y][$i]['Shop_Id'] = $row['shop_id'];
                $arr[$y][$i]['Price'] = $row['price'];
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
            }
            $i++;
        }
    }
    $x++;
}

echo json_encode($arr);
?>