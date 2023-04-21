using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    /// <summary>
    /// Path is the class that represents a path in the grid.
    /// it stores a list of integers that represent the index of the gridnode in the grid.
    /// </summary>
    public class Path
    {
        //variable declaration
        private readonly List<int> _path;
        public int CurrentIndex = 0;
        
        //getters setters
        public List<int> PathNodes => _path;
        public GridNode StartNode { get; set; }
        public GridNode EndNode { get; set; }

        public int CurrentNodeIndex => _path[CurrentIndex];

        public bool DestinationReached { get; set; } = false;
        
        public PathType PathType { get; set; } = PathType.Unknown;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <param name="pType"></param>
        public Path(List<int> nodes, GridNode startNode, GridNode endNode, PathType pType = PathType.Unknown)
        {
            _path = nodes;
            StartNode = startNode;
            EndNode = endNode;
            PathType = pType;
        }


        /// <summary>
        /// Peek in to the future! and get the next node in the path of it does not exist return null
        /// </summary>
        /// <returns></returns>
        public int GetNextNode()
        {
            return CurrentIndex + 1 < _path.Count ? _path[CurrentIndex + 1] : -1;
        }

        /// <summary>
        /// Resets the path 
        /// </summary>
        public void Reset()
        {
            _path.Clear();
            CurrentIndex = 0;
            StartNode = null;
            EndNode = null;
            DestinationReached = false;
        }

        public Path Copy()
        {
            Path newPath = new Path(_path, StartNode, EndNode, PathType);
            newPath.CurrentIndex = CurrentIndex;
            newPath.DestinationReached = DestinationReached;

            return newPath;
        }
    }
}