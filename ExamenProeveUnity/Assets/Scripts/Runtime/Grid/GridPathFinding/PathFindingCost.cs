namespace Runtime.Grid.GridPathFinding
{
    public struct PathFindingCost
    {
        public readonly int Index; 
        
        public int Gcost;
        public int Hcost;
        public int Fcost;
        public GridNode CameFromNode;
        
        public PathFindingCost(int index, int gCost, int hCost, GridNode cameFromNode)
        {
            this.Index = index;
            this.Gcost = gCost;
            this.Hcost = hCost;
            this.Fcost = gCost + hCost;
            this.CameFromNode = cameFromNode;
        }
    }
}