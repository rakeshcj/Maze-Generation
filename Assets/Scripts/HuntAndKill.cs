using UnityEngine;

public class HuntAndKill
{
    public Cells[,] cells = CellCreator.cells;
    public int no_of_column = CellCreator.no_of_column, no_of_rows = CellCreator.no_of_rows;

    public static void InstantitateHuntAndKill()
    {
        HuntAndKill huntandkill = new HuntAndKill();
        huntandkill.KillPhase(0, 0);
        huntandkill.HuntPhase();
    }
    private void KillPhase(int row, int column)
    {
        ArrayedQueue arrayedqueue = new ArrayedQueue();

        bool isEnd = false;
        if (cells[row, column].visited != true)
        {
            while (!isEnd)
            {
                cells[row, column].visited = true;

                bool[] AvailaleWallToBreak = BreakageCountFinder(row, column);
                arrayedqueue.ClearArray();
                for (int i = 0; i < 4; i++)
                {
                    if (AvailaleWallToBreak[i] == true)
                    {
                        arrayedqueue.add(i);
                    }
                }

                if (arrayedqueue.ArrayIndexLength() <= -1)
                {
                    isEnd = true;
                    break;
                }
                else
                {
                    int RandomVar = Random.Range(0, (arrayedqueue.ArrayIndexLength() + 1));

                    switch (arrayedqueue.arr[RandomVar])
                    {
                        case 0:                            
                            GameObject.Destroy(cells[row, column].northwall);
                            row++;
                            break;
                        case 1:
                            GameObject.Destroy(cells[row, column].eastwall);
                            column++;                            
                            break;
                        case 2:
                            GameObject.Destroy(cells[(row - 1), column].northwall);
                            row--;                            
                            break;
                        case 3:
                            GameObject.Destroy(cells[row, (column - 1)].eastwall);
                            column--;                            
                            break;
                    }

                }
            }
        }
    }

    private bool[] BreakageCountFinder(int row, int column)
    {
        bool[] breakageCount = new bool[4];

        if (row > 0)
        {
            if (cells[row - 1, column].visited == false)
            {
                breakageCount[2] = true;
            }
        }
        if (row < no_of_rows - 1)
        {
            if (cells[row + 1, column].visited == false)
            {
                breakageCount[0] = true;
            }
        }
        if (column > 0)
        {
            if (cells[row, column - 1].visited == false)
            {
                breakageCount[3] = true;
            }
        }
        if (column < no_of_column - 1)
        {
            if (cells[row, column + 1].visited == false)
            {
                breakageCount[1] = true;
            }
        }

        return breakageCount;
    }

    private void HuntPhase()
    {
        for (int i = 0, m = no_of_rows; i < m; i++)
        {
            for (int j = 0, n = no_of_column; j < n; j++)
            {
                if(cells[i,j].visited==false)
                {
                    FindAdjacency(i, j);
                }
            }
        }
    }

    private void FindAdjacency(int row, int column)
    {

        bool adjacency_Availabe = false;

        if (row > 0)
        {
            if (cells[(row - 1), column].visited == true)
            {
                GameObject.Destroy(cells[(row - 1), column].northwall);
                adjacency_Availabe = true;
            }
        }
        if (row < (no_of_rows - 1) && adjacency_Availabe!=true)
        {
            if (cells[(row + 1), column].visited == true)
            {
                GameObject.Destroy(cells[row, column].northwall);
                adjacency_Availabe = true;
            }
        }
        if (column > 0 && adjacency_Availabe != true)
        {
            if (cells[row, (column - 1)].visited == true)
            {
                GameObject.Destroy(cells[row, (column - 1)].eastwall);
                adjacency_Availabe = true;
            }
        }
        if (column < (no_of_column - 1) && adjacency_Availabe != true)
        {
            if (cells[row, (column + 1)].visited == true)
            {
                GameObject.Destroy(cells[row, column].eastwall);
                adjacency_Availabe = true;
            }
        }

        if (adjacency_Availabe == true)
        {
            KillPhase(row, column);
        }
    }

}

