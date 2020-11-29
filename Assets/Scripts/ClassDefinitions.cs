using UnityEngine;

[System.Serializable]
public class Cells
{
    public bool visited = false;
    public GameObject northwall, southwall, eastwall, westwall;
}

[System.Serializable]
public class ArrayedQueue
{
    public int[] arr=new int[4];
    int index = -1;

    public void add(int data)
    {
        if(index<3)
        {
            index++;
            arr[index] = data;
        }
    }
    public void ClearArray()
    {
        for(int i=0;i<4;i++)
        {
            arr[i] = 0;
        }
        index = -1;
    }
    
    public int ArrayIndexLength()
    {
        return (index);
    }
}



