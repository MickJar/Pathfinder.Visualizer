using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Visualizer.Data
{
    public class Node
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public State state { get; set; }

    }
}
