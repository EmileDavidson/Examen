using System.Collections.Generic;
using Runtime.Grid.GridPathFinding;
using Toolbox.Utils.Runtime;

namespace Runtime.Grid
{
    public class WorldManager : MonoSingleton<WorldManager>
    {
        public MyGrid worldGrid;
        
        public List<Shelf> shelves;
        public List<CashRegister> checkouts;

        public List<FixedPath> entryPaths = new List<FixedPath>();
        public List<FixedPath> exitPaths = new List<FixedPath>();

        protected override void Awake()
        {
            base.Awake();
            shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
            checkouts = new List<CashRegister>(FindObjectsOfType<CashRegister>());
        }
    }
}