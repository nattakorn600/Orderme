
<?php
header("Access-Control-Allow-Origin: *"); 
header('Content-type:image/png');
include "../phpqrcode/qrlib.php"; 

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
    $logosize = "iconpromp170.png";
}else{
    $logosize = "iconpromp130.png";
}

$id = date("YmdHis").rand(100,999);
$path = "../image/qrcode/";
$filename = $path.$id.".png";
$qrcode = $path.'qrcode.png';

QRcode::png($text, $filename, "H", 24, 5);

$base = imagecreatefrompng($filename);
list($px, $py, $type, $attr) = getimagesize($filename);
list($pxi, $pyi, $typei, $attri) = getimagesize($logosize);
unlink($filename);
$logo = imagecreatefrompng($logosize);
$px = ($px / 2) - ($pxi / 2);
$py = ($py / 2) - ($pyi / 2);
imagecopymerge_alpha($base, $logo, $px, $py, 0, 0, $pxi, $pyi, 100);
ImageString($images, 5, 10, 10, $TEXT, $photo);
imagepng($base, $qrcode);
imagedestroy($base);


echo file_get_contents($qrcode);

function imagecopymerge_alpha($dst_im, $src_im, $dst_x, $dst_y, $src_x, $src_y, $src_w, $src_h, $pct){ 
        $cut = imagecreatetruecolor($src_w, $src_h); 
        imagecopy($cut, $dst_im, 0, 0, $dst_x, $dst_y, $src_w, $src_h); 
        imagecopy($cut, $src_im, 0, 0, $src_x, $src_y, $src_w, $src_h); 
        imagecopymerge($dst_im, $cut, $dst_x, $dst_y, 0, 0, $src_w, $src_h, $pct); 
    } 
?>