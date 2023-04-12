using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IGridable
    {
        [field: SerializeField] public int gridIndex { get; set; }
    }
}