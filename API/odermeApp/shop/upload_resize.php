<?php 
    function imageResize($ext, $ori_file, $new_file, $max_imageSize)
    {
        $ori_size = getimagesize($ori_file);
        $ori_w = $ori_size[0];
        $ori_h = $ori_size[1];
         
        if($ori_w > $ori_h) {
            $new_w = $max_imageSize;
            $new_h = round(($new_w/$ori_w) * $ori_h);
        }
        else
        {
            $new_h = $max_imageSize;
            $new_w = round(($new_h/$ori_h) * $ori_w);
        }
     
        if ($ext == "jpg" or $ext == "jpeg") {
            $ori_img = imagecreatefromjpeg($ori_file);
        } else
        if ($ext == "png") {
            $ori_img = imagecreatefrompng($ori_file);
        } else
        if ($ext == "gif") {
            $ori_img = imagecreatefromgif($ori_file);
        } 
 
        $new_img = imagecreatetruecolor($new_w, $new_h);
        imagecopyresized($new_img, $ori_img, 0, 0, 0, 0, $new_w, $new_h, $ori_w, $ori_h);
        if ($ext == "jpg" or $ext == "jpeg") {
            imagejpeg($new_img, $new_file); 
        } else
        if ($ext == "png") {
            imagepng($new_img, $new_file); 
        } else
        if ($ext == "gif") {
            imagegif($new_img, $new_file); 
        }
         
        imagedestroy($ori_img);
        imagedestroy($new_img);
    }
?>