using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IGridAble
    {
        [Tooltip("The index of the grid node this is used for the customer as point to go to but may also used for other things")]
        public int GridIndex { get; }
    }
}