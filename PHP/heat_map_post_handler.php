<?php
$HeatMap= $_POST["heatmap"];
if($HeatMap != ""){
    echo("Saving Heat Map");
    $heatFile =fopen("heatmap.txt","a");
    fwrite($heatFile,$HeatMap);
    fclose($heatFile);
}else{
    echo("Delivery failed");
}
?>