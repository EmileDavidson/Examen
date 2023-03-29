using System.Collections.Generic;
using Mono.Reflection;
using Runtime.Enums;

namespace Runtime.Dictonaries
{
    public class MouseKeyDict
    {
        public static Dictionary<HandType, int> Dict = new Dictionary<HandType, int>()
        {
            {HandType.Left, 1},
            {HandType.Right, 0}
        };
    }
}