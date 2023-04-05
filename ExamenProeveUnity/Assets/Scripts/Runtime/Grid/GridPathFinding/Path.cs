using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    public class Path
    {
        //variable declaration
        private readonly List<GridNode> _path;
        public int CurrentIndex = 0;
        
        //getters setters
        public List<GridNode> PathNodes => _path;
        public GridNode StartNode { get; set; }
        public GridNode EndNode { get; set; }

        public GridNode CurrentNode => _path[CurrentIndex];

        public bool DestinationReached { get; set; } = false;

        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        public Path(List<GridNode> nodes, GridNode startNode, GridNode endNode)
        {
            _path = nodes;
            StartNode = startNode;
            EndNode = endNode;
        }


        /// <summary>
        /// Peek in to the future! and get the next node in the path of it does not exist return null
        /// </summary>
        /// <returns></returns>
        public GridNode GetNextNode()
        {
            return CurrentIndex + 1 < _path.Count ? _path[CurrentIndex + 1] : null;
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
    }
}