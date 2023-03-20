using System.Collections.Generic;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime
{
    public class Tester : MonoBehaviour
    {
        private List<int> items = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };
        
        [Button]
        public void Test()
        {
            Debug.Log(items.Get(-15)); 
            Debug.Log(items.Get(-14));
            Debug.Log(items.Get(-13));
            Debug.Log(items.Get(-12));
            Debug.Log(items.Get(-11));
            Debug.Log(items.Get(-10));
            Debug.Log(items.Get(-9));
            Debug.Log(items.Get(-8));
            Debug.Log(items.Get(-7));
            Debug.Log(items.Get(-6));
            Debug.Log(items.Get(-5));
            Debug.Log(items.Get(-4));
            Debug.Log(items.Get(-3));
            Debug.Log(items.Get(-2));
            Debug.Log(items.Get(-1));
            Debug.Log(items.Get(0));
            Debug.Log(items.Get(1));
            Debug.Log(items.Get(2));
            Debug.Log(items.Get(3));
            Debug.Log(items.Get(4));
            Debug.Log(items.Get(5));
            Debug.Log(items.Get(6));
            Debug.Log(items.Get(7));
            Debug.Log(items.Get(8));
            Debug.Log(items.Get(9));
            Debug.Log(items.Get(10));
            Debug.Log(items.Get(11));
            Debug.Log(items.Get(12));
            Debug.Log(items.Get(13));
            Debug.Log(items.Get(14));
            Debug.Log(items.Get(15));
            
        }
    }
}