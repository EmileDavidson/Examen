using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.MethodExtensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// from a array of numbers returns value closest to given value.
        /// 
        /// current = 2, arr = {0,1,2,3,4,5} returns {2}
        /// current = 2, arr = {0,1,3,4,5} returns {1,3}
        /// current = 2, arr = {0,3,4,5} returns {3}
        /// </summary>
        /// <param name="current"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] FindClosestIndex(this int current, int[] arr)
        {
            if (arr.IsEmpty()) throw new IndexOutOfRangeException("Cannot get closest number from an empty list");
            if (arr.Contains(current)) return new []{current};
            if (arr.Length == 1) return new []{arr[0]};

            var ints = new List<int>(){arr[0]};
            var oldDistance = Math.Abs(current - arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                if(!(Math.Abs(current - arr[i]) <= oldDistance)) continue;
                if (Math.Abs(current - arr[i]) == oldDistance)
                {
                    ints.Add(arr[i]);
                    continue;
                }
                oldDistance = Math.Abs(current - arr[i]);
                ints.Clear();
                ints.Add(arr[i]);
            }
            return ints.ToArray();
        }
    }
}