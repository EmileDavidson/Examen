using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Runtime.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.MethodExtensions
{
    public static class ObjectExtensions
    {
        public static List<T> FindObjectByInterface<T>()
        {
            return Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToList();
        }

    }
}