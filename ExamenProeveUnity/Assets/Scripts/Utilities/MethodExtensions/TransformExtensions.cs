using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.MethodExtensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Resets the transform to its default values.
        /// </summary>
        /// <param name="trans"></param>
        public static void ResetTransformation(this Transform trans)
        {
            trans.position = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(1, 1, 1);
        }
        
        /// <summary>
        /// Find all children of the Transform by tag (includes self)
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static List<Transform> FindChildrenByTag(this Transform transform, params string[] tags)
        {
            List<Transform> list = new List<Transform>();
            foreach (var tran in transform.Cast<Transform>().ToList())
            {
                list.AddRange(tran.FindChildrenByTag(tags)); // recursively check children
            }
            return list;
        }
        
         

    }
}
