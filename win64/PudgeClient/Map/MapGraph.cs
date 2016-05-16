using AIRLab.Mathematics;
using Pudge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.Map
{
    class MapGraph
    {
        public List<List<int>> AdjacencyList;

        public List<MapVertex> Vertices;

        public int IndexOf(MapVertex vertex)
        {
            return vertex.ID;
        }

    }
}
