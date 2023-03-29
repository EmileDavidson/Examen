using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{
    private void Start()
    {
        PathFinding pathFinding = new PathFinding(10, 10);

        List<PathNode> path = pathFinding.FindPath(0, 0, 3, 7);
        if (path != null)
        {
            print("cool");
            foreach (PathNode pathNode in path)
            {
                Debug.DrawLine(new Vector3(pathNode.X, 0, pathNode.Y) * 10f + Vector3.one * 5f, new Vector3(pathNode.X, 0, pathNode.Y) * 10f + Vector3.one * 5f, Color.blue, 100f);
            }
        }
    }
}
