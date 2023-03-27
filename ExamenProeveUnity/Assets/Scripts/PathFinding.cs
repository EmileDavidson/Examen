using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private Grid<PathNode> _grid;

    public PathFinding(int width, int height)
    {
        _grid = new Grid<PathNode>(width, height, 1f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }
}
