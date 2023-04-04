using System;
using UnityEngine;

namespace Runtime.Grid
{
    public class GridHelper
    {
        public static void Grid3dLoop(int width, int height, int depth, Action<int, Vector3Int> callback)
        {
                int cellIndex = 0;
            for (var gridZ = 0; gridZ < depth; gridZ++)
            {
                for (var gridY = 0; gridY < height; gridY++)
                {
                    for (var gridX = 0; gridX < width; gridX++)
                    {
                        callback.Invoke(cellIndex, new Vector3Int(gridX, gridY, gridZ));
                        cellIndex++;
                    }
                }
            }

        }
    }
}