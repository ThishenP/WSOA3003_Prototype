<?php
    $prefs = $_POST["prefs"];

    if($prefs!=""){
        echo("Preference saved");
        $prefsFile =fopen("prefs.txt","w");
        fwrite($prefsFile,$prefs);
        fclose($prefsFile);
    }else{
        echo("Delivery failed");
    }
?>