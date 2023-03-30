using System;
using UnityEngine;

public class Grid<T>
{
    private int width;

    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private T[,] gridArray;

    public int Width => width;
    public int Height => height;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new T[width, height];

        bool showDebug = true;
        if (showDebug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    gridArray[x, y] = createGridObject(x, y);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.green, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.green, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.green, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.green, 100f);
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return (new Vector3(x, 0, y) + originPosition) * cellSize;
    }

    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        return default;
    }
    
    public T[,] GetPath()
    {
        return gridArray;
    }
}
