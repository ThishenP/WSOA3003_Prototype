<?php
    $text1 = $_POST["name"];
    $text2 = $_POST["feedback"];
  

    if($text1 != "")
    {
        echo("Message Sent");
        echo("Field 1:" . $text1);
        echo("Field 2:" . $text2);
        $file = fopen("data.txt","a");
        fwrite($file, $text1);
        fwrite($file, $text2);
        fclose($file);
    }else{
        echo("Delivery failed");
    }
?>
