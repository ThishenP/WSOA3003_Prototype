<?php
$scoreAndUser= $_POST["scores"];
$user = $_POST["user"];
$AddType = $_POST["AddType"];

if($scoreAndUser!="--none--"){
    if($scoreAndUser != ""){
        echo("Saving user data");
        if($AddType=="overwrite"){
            $scoresFile =fopen("scores.txt","w");
        }else{
            if($AddType=="append"){
                $scoresFile =fopen("scores.txt","a");
            }
        }
        
        fwrite($scoresFile,$scoreAndUser);
        fclose($scoresFile);
    }else{
        echo("Delivery failed");
    }
}

if($user!="--none--"){
    if($user != ""){
        echo("Saving user data");
        $userFile =fopen("current.txt","w");
        fwrite($userFile,$user);
        fclose($userFile);
    }else{
        echo("Delivery failed");
    }
}

?>