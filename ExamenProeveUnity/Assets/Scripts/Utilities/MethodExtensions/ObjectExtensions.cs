using System.Collections.Generic;
using System.Linq;
using Runtime.Interfaces;
using UnityEngine;

namespace Utilities.MethodExtensions
{
    public static class ObjectExtensions
    {
        public static List<T> FindObjectByInterface<T>() 
        {
            return  Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToList();
        }
    }
}