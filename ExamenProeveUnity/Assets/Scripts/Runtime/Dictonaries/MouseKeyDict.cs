using System.Collections.Generic;
using Mono.Reflection;
using Runtime.Enums;

namespace Runtime.Dictonaries
{
    public class MouseKeyDict
    {
        public static Dictionary<MouseType, int> Dict = new Dictionary<MouseType, int>()
        {
            {MouseType.Left, 1},
            {MouseType.Right, 0}
        };
    }
}