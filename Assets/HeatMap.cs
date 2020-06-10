using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    public float checkFrequency;
    private float timeSinceCheck=0.2f;
    private int column;
    private int row;
    private Vector2 pos;
    private int[,] gridPosArray = new int[8, 16];
    private int highest=0;
    private bool dataSaved=false;
  

    void Update()
    {
        if (control.instance.end==false)
        {
            timeSinceCheck += Time.deltaTime;
            if (timeSinceCheck > checkFrequency)
            {
                pos = new Vector2(transform.position.x, transform.position.y);
                column = GetColumn(pos.x);
                row = GetRow(pos.y);
                
                gridPosArray[row, column]++;
                timeSinceCheck = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                column = GetColumn(mousePos.x);
                row = GetRow(mousePos.y);
            }
        }
        else
        {
            
            if (dataSaved==false)
            {
                control.instance.heatMapData += "|\n";
                string test = "";
                for (int i = 0; i < gridPosArray.GetLength(0); i++)
                {
                    for (int j = 0; j < gridPosArray.GetLength(1); j++)
                    {
                       if(highest< gridPosArray[i, j])
                       {
                            highest = gridPosArray[i, j];
                       } 
                    }
                }
                string row = "";
              
                for (int i=0;i< gridPosArray.GetLength(0); i++)
                {
                    for (int j = 0; j < gridPosArray.GetLength(1); j++)
                    {
                        //row +=" "+ gridPosArray[i, j];
                        control.instance.heatMap[i,j]=(float)gridPosArray[i, j]/highest;//saving array with each cell being a value between 0 and 1 (% of time spent in each cell)
                        row +=" "+ control.instance.heatMap[i, j];
                        control.instance.heatMapData+="("+i+","+j+"): "+control.instance.heatMap[i, j]+"\n";
                    }
                   
                    row = "";
                }
                
                Debug.Log(control.instance.heatMapData);
                dataSaved = true;
            }
           
        }
       

    }

    //simply casting to int will give cell column and row due to the fact that my grid is lined up with unities grid
    //have to add half the amount of cells to each (8 and 4) because the unity positions go negative and array is only positive
    private int GetColumn(float xpos)
    {
        int col = (int)(xpos+8);
        return col;
    }
    

    private int GetRow(float ypos)
    {
        int row = (int)(Mathf.Abs(ypos-4));
        return row;
    }


}
