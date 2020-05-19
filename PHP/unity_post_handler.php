<?php
    $text1 = $_POST["name"];
    $text2 = $_POST["feedback"];
  

    if($text1 != "")
    {
        echo("Message Sent");
        $file = fopen("feedback.txt","a");
        fwrite($file, $text1);
        fwrite($file, $text2);
        fclose($file);
    }else{
        echo("Delivery failed");
    }
?>
