public class PathNode
{
    private int x;
    private int y;
    private bool blockedNode;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public int X => x;
    public int Y => y;
    
    public PathNode(int x, int y, bool blockedNode = false)
    {
        this.x = x;
        this.y = y;
        this.blockedNode = blockedNode;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetBlocked(bool doBlock)
    {
        blockedNode = doBlock;
    }

    public bool IsBlocked()
    {
        if (blockedNode) return true;
        return false;
    }
}
