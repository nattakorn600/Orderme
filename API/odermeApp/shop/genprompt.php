<?php
header("Access-Control-Allow-Origin: *"); 
header('Content-type:image/png');
include "../phpqrcode/qrlib.php";
include "CRC16.php";

$data = $_REQUEST['data'];

$prompt_id = substr($data, 2, substr($data, 0, 2));
$i = 2 + strlen($prompt_id);
$amount = substr($data, $i+2, substr($data, $i, 2));

$f1 = "00020101021129370016A0000006770101110";
if(strlen($prompt_id) <= 10)
{
    $f2 = "1130066";
    $prompt_id = substr($prompt_id, 1, 10);
}
else
{
    $f2 = "213";
}
$f3 = "5802TH54";
$f4 = "0".strlen($amount);
$f5 = "53037646304";
$promptpay = $f1.$f2.$prompt_id.$f3.$f4.$amount.$f5;
$checksum = strtoupper(CRC16::convertToString(CRC16::calculate($promptpay),10,16));

$id = date("YmdHis").rand(100,999);
$path = "../image/qrcode/";
$filename = $path.$id."prompt.png";

QRcode::png($promptpay.$checksum, $filename, "L", 10, 1);
echo file_get_contents($filename);
?>