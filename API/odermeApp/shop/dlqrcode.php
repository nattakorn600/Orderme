<?php
header("Access-Control-Allow-Origin: *");
header('Content-type:image/png');
include "../phpqrcode/qrlib.php";
include "../connect.php"; 

$data = $_REQUEST['data'];

$shop_id = substr($data, 2, substr($data, 0, 2));
$i=2 + strlen($shop_id);
$table = substr($data, $i+2, substr($data, $i, 2));

$nubshop = strlen($shop_id);
if($nubshop < 10)
{
    $nubshop = "0".$nubshop;
}
$nubtable = strlen($table);
if($nubtable < 10)
{
    $nubtable = "0".$nubtable;
}
$code = "6036".$nubshop.$shop_id.$nubtable.$table;
$cksum = 0;
for($i=0; $i<strlen($code); $i++)
{
    $cksum = $cksum + $code[$i];
}
$text = $code.$cksum;

if(strlen($text) > 17)
{
    $logosize = "../iconpromp170.png";
}else{
    $logosize = "../iconpromp130.png";
}

$id = date("YmdHis").rand(100,999);
$path = "../image/qrcode/";
$filename = $path.$id.".png";
$qrcode = $path.'qrcode.png';

QRcode::png($text, $filename, "H", 24, 1);

$query = "SELECT * FROM shop WHERE id='$shop_id'";
$result = mysqli_query($con, $query);
$row = mysqli_fetch_array($result);
$shop_name = $row['name'];

$base = imagecreatefrompng($filename);
list($px, $py, $type, $attr) = getimagesize($filename);
list($pxi, $pyi, $typei, $attri) = getimagesize($logosize);
unlink($filename);
$logo = imagecreatefrompng($logosize);
$pxd = ($px / 2) - ($pxi / 2);
$pyd = ($py / 2) - ($pyi / 2);
imagecopymerge_alpha($base, $logo, $pxd, $pyd, 0, 0, $pxi, $pyi, 100);
$pyyy = ($py * 0.15);
$pyy = $py + $pyyy*2;
$my_img = imagecreatetruecolor($px, $pyy);
$textcolor = imagecolorallocate($my_img, 0, 0, 0);
$white = imagecolorallocate($my_img, 255, 255, 255);
imagefilledrectangle($my_img, 0, 0, $px, $pyy, $white);
$text = "โต๊ะหมายเลข: ".$table;
$font = '../font/Sarabun-Regular.ttf';
$fontsize = $py * 0.05;
$bbox2 = imagettfbbox($fontsize, 0, $font, $shop_name);
$bbox = imagettfbbox($fontsize, 0, $font, $text);

//(image, size, rotation, left, top, textcolor, font, text)
imagettftext($my_img, $fontsize, 0, $px/2 - $bbox2[4]/2, $fontsize*2, $textcolor, $font, $shop_name);
imagettftext($my_img, $fontsize, 0, $px/2 - $bbox[4]/2, $pyy - $fontsize, $textcolor, $font, $text);
imagecopymerge_alpha($my_img, $base, 0, $pyyy, 0, 0, $px, $py, 100);
imagepng($my_img, $qrcode);
imagedestroy($my_img);
imagedestroy($base);

echo file_get_contents($qrcode);

function imagecopymerge_alpha($dst_im, $src_im, $dst_x, $dst_y, $src_x, $src_y, $src_w, $src_h, $pct){ 
        $cut = imagecreatetruecolor($src_w, $src_h); 
        imagecopy($cut, $dst_im, 0, 0, $dst_x, $dst_y, $src_w, $src_h); 
        imagecopy($cut, $src_im, 0, 0, $src_x, $src_y, $src_w, $src_h); 
        imagecopymerge($dst_im, $cut, $dst_x, $dst_y, 0, 0, $src_w, $src_h, $pct); 
    } 
?>