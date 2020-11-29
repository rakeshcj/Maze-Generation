using System;
using UnityEngine;

public class CellCreator : MonoBehaviour
{
    public static int no_of_rows,no_of_column;
    public int rows, columns;
    public float cellSize;
    public static Cells[,] cells;
    public HuntAndKill huntAndKill;
    private GameObject flor;
    public GameObject wallPrefab,wallConnect1, wallConnect2, wallConnect3, wallConnect4, floor;

    //This is the starting part of the entire code.
    void Start()
    {
        no_of_rows = rows;
        no_of_column = columns;
        cells = new Cells[no_of_rows, no_of_column];
        createAnchors();
        GenerateMaze();
        //flor = Instantiate(floor, new Vector3((((cellSize / 2f)*no_of_rows)-cellSize/2f), -5, (((cellSize / 2f) * no_of_rows) - cellSize / 2f)), Quaternion.identity) as GameObject;
        //flor.transform.localScale = new Vector3(cellSize * no_of_rows, 0, cellSize * no_of_column);
        //Calls the InstantitateHuntAndKill function on hunt and kill script 
        HuntAndKill.InstantitateHuntAndKill();
    }

    private void createAnchors()
    {
        for(int i = 0; i <= no_of_rows; i++)
        {
            for(int j = 0; j <= no_of_column; j++)
            {
                switch ((i + j) % 4)
                {
                    case 1:
                        Instantiate(wallConnect1, new Vector3((cellSize * j) - (cellSize / 2), .05f, (cellSize * i) - (cellSize / 2)), Quaternion.Euler(-90f, 0f, 0f));
                        break;
                    case 2:
                        Instantiate(wallConnect2, new Vector3((cellSize * j) - (cellSize / 2), .05f, (cellSize * i) - (cellSize / 2)), Quaternion.Euler(-90f, 0f, 0f));
                        break;
                    case 3:
                        Instantiate(wallConnect3, new Vector3((cellSize * j) - (cellSize / 2), .05f, (cellSize * i) - (cellSize / 2)), Quaternion.Euler(-90f, 0f, 0f));
                        break;
                    case 0:
                        Instantiate(wallConnect4, new Vector3((cellSize * j) - (cellSize / 2), .05f, (cellSize * i) - (cellSize / 2)), Quaternion.Euler(-90f, 0f, 0f));
                        break;
                }  
                
            }
        }
    }


    //this will generate grid of size defined by user as rows and columns and assign it to cells variables.
    private void GenerateMaze()
    {
        for (int i = 0; i < no_of_rows; i++)
        {
            for (int j = 0; j < no_of_column; j++)
            {
                cells[i, j] = new Cells();

                cells[i, j].northwall = Instantiate(wallPrefab, new Vector3((cellSize * j), 0, (cellSize * i) + (cellSize / 2f)), Quaternion.identity) as GameObject;
                //cells[i, j].northwall.transform.localScale = new Vector3(cellSize, 0.001f, cellSize);
                cells[i, j].northwall.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                cells[i, j].northwall.transform.name = "NorthWall" + "[" + i + "]" + "[" + j + "]";

                cells[i, j].eastwall = Instantiate(wallPrefab, new Vector3(((cellSize * j) + cellSize / 2f), 0, (i * cellSize / 2f) + (i * cellSize / 2f)), Quaternion.identity) as GameObject;
                //cells[i, j].eastwall.transform.localScale = new Vector3(cellSize, 0.001f, cellSize);
                cells[i, j].eastwall.transform.localRotation = Quaternion.Euler(90f, 0f, 90f);
                cells[i, j].eastwall.transform.name = "EastWall" + "[" + i + "]" + "[" + j + "]";

                //west wall is generated only for the columns which is zero because other cells will have the j-1 cells's east wall as j's west wall 
                if(j==0)
                {
                    cells[i, j].westwall = Instantiate(wallPrefab, new Vector3((-cellSize / 2f), 0, cellSize * i), Quaternion.identity) as GameObject;
                    //cells[i, j].westwall.transform.localScale = new Vector3(cellSize, 0.001f, cellSize);
                    cells[i, j].westwall.transform.localRotation = Quaternion.Euler(90f, 0f, 90f);
                    cells[i, j].westwall.transform.name = "WestWall" + "[" + i + "]" + "[" + j + "]";
                }

                //south wall is generated only for the rows which is zero because other cells will have the i-1 cells's north wall as i's south wall 
                if (i==0)
                {
                    cells[i, j].southwall = Instantiate(wallPrefab, new Vector3((cellSize * j), 0, -(cellSize / 2f)), Quaternion.identity) as GameObject;
                    //cells[i, j].southwall.transform.localScale = new Vector3(cellSize, 0.001f, cellSize);
                    cells[i, j].southwall.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                    cells[i, j].southwall.transform.name = "SouthWall" + "[" + i + "]" + "[" + j + "]";
                }
                
            }
        }
    }
}
