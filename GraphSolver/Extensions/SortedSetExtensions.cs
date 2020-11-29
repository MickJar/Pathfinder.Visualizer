using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Visualizer.Extensions
{
    public static class SortedSetExtensions
    {
        public static T Deque<T>(this SortedSet<T> set)
        {
            T item = set.First();

            set.Remove(item);

            return item;
        }
    }
}
