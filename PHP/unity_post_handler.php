<?php
    $text1 = $_POST["name"];
    $text2 = $_POST["answer1"];
    $text3 = $_POST["answer3"];

    if($text1 != "")
    {
        echo("Message Sent");
        echo("Field 1:" . $text1);
        echo("Field 2:" . $text2);
        echo("Field 3:" . $text3);
        $file = fopen("data.txt","a");
        fwrite($file, $text1);
        fwrite($file, $text2);
        fwrite($file, $text3);
        fclose($file);
    }else{
        echo("Delivery failed");
    }
?>
