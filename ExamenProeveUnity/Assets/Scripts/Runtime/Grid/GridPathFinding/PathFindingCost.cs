namespace Runtime.Grid.GridPathFinding
{
    public struct PathFindingCost
    {
        public int Gcost;
        public int Hcost;
        public int Fcost;
        public GridNode CameFromNode;
        
        public PathFindingCost(int gCost, int hCost, GridNode cameFromNode)
        {
            this.Gcost = gCost;
            this.Hcost = hCost;
            this.Fcost = gCost + hCost;
            this.CameFromNode = cameFromNode;
        }
    }
}