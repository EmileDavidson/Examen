﻿using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Environment;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Interfaces;
using Utilities.MethodExtensions;
using Utilities.Other.Runtime;

namespace Runtime.Managers
{
    public class WorldManager : MonoSingleton<WorldManager>
    {
        public MyGrid worldGrid;
        
        public List<Shelf> shelves;
        public List<CashRegister> checkouts;

        public List<FixedPath> entryPaths = new();
        public List<FixedPath> exitPaths = new();
        private List<IGridAble> _gridables = new();

        protected override void Awake()
        {
            base.Awake();
            shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
            checkouts = new List<CashRegister>(FindObjectsOfType<CashRegister>());
            _gridables = ObjectExtensions.FindObjectByInterface<IGridAble>();
            
            shelves.RemoveAll(shelf => shelf.Item is null || shelf.Item.Type == ProductType.Unknown);

            if (worldGrid == null) return;
            if (worldGrid.ShouldGenerate())
            {
                worldGrid.GenerateGrid();
            }

            foreach (var gridable in _gridables)
            {
                worldGrid.GetNodeByIndex(gridable.GridIndex)?.SetLocationNode(true);
            }
        }
    }
}