using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    private int x;
    public int X => x;
    private int y;
    public int Y => y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
