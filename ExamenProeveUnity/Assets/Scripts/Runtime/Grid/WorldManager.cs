using System.Collections.Generic;
using Runtime.Grid.GridPathFinding;
using Runtime.Interfaces;
using Utilities.MethodExtensions;
using Toolbox.Utils.Runtime;

namespace Runtime.Grid
{
    public class WorldManager : MonoSingleton<WorldManager>
    {
        public MyGrid worldGrid;
        
        public List<Shelf> shelves;
        public List<CashRegister> checkouts;

        public List<FixedPath> entryPaths = new();
        public List<FixedPath> exitPaths = new();
        private List<IGridable> _gridables = new();

        protected override void Awake()
        {
            base.Awake();
            shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
            checkouts = new List<CashRegister>(FindObjectsOfType<CashRegister>());
            _gridables = ObjectExtensions.FindObjectByInterface<IGridable>();
            
            if (worldGrid == null) return;
            if (worldGrid.ShouldGenerate())
            {
                worldGrid.GenerateGrid();
            }

            foreach (var gridable in _gridables)
            {
                worldGrid.GetNodeByIndex(gridable.gridIndex)?.SetLocationNode(true);
            }
        }
    }
}