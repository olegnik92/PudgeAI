using Priority_Queue;
using PudgeClient.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    class PudgePath
    {
        public List<int> Vertices;
        public Dictionary<int, bool> VertexReachMap;
        public PudgePath(List<int> vertices)
        {
            this.Vertices = vertices;
            VertexReachMap = new Dictionary<int, bool>();
            vertices.ForEach(v =>
            {
                VertexReachMap[v] = false;
            });
        }

        public void VertexReached(int vertex)
        {
            VertexReachMap[vertex] = true;
        }

        public bool IsEqualTo(PudgePath path)
        {
            PudgePath longPath, shortPath;
            if (path.Vertices.Count > this.Vertices.Count)
            {
                longPath = path;
                shortPath = this;
            }
            else
            {
                longPath = this;
                shortPath = path;
            }

            var lenDif = longPath.Vertices.Count - shortPath.Vertices.Count;
            for (int i = longPath.Vertices.Count - 1; i > -1; i--)
            {
                int j = i - lenDif;
                if(j < 0)
                {
                    if (longPath.VertexReachMap[longPath.Vertices[i]])
                    {
                        continue;
                    }

                    return false;
                }
          
                if(longPath.Vertices[i] != shortPath.Vertices[j]
                    && (!longPath.VertexReachMap[longPath.Vertices[i]] || !shortPath.VertexReachMap[shortPath.Vertices[j]]))
                {
                    return false;
                }
            }


            return true;
        }


        public double Count
        {
            get
            {
                return Vertices.Count;
            }
        }

        public double GetLength(MapGraph map)
        {
            double len = 0;
            for (int i = 1; i < Vertices.Count; i++ )
            {
                len += Math.Sqrt(Helper.SqrDist(map.Vertices[Vertices[i]].X, map.Vertices[Vertices[i]].Y,
                                                map.Vertices[Vertices[i - 1]].X, map.Vertices[Vertices[i - 1]].Y));
            }

            return len;
        }

        public int GetCurrentTargetIndex()
        {
            for (int i = 0; i < Vertices.Count; i++ )
            {
                if(!VertexReachMap[Vertices[i]])
                {
                    return Vertices[i];
                }
            }

            return Vertices[Vertices.Count - 1];
        }

        public int GetPrevTargetIndex(int curTargetIndex)
        {
            var prevIndex = Vertices[Math.Max(Vertices.IndexOf(curTargetIndex) - 1, 0)];
            return prevIndex;
        }

        public int GetPrevTargetIndex()
        {
            return GetPrevTargetIndex(GetCurrentTargetIndex());
        }

        public bool IsPathDone()
        {
            return Vertices.All(v => VertexReachMap[v]);
        }



        private static List<List<int>> searchMapCahce = new List<List<int>>();
        public static void BuildSearchMapCache(MapGraph map)
        {
            searchMapCahce = map.Vertices.Select(v => (List<int>)(null)).ToList();
            Parallel.For(0, map.Vertices.Count, start =>
            {
                searchMapCahce[start] = DijkstraSearch(start, map);
            });
        }


        public static List<PudgePath> FindPath(IEnumerable<int> targets, int start, MapGraph map, List<double> dangerMap = null)
        {
            List<int> searchMap;
            if (searchMapCahce.Count < map.Vertices.Count || searchMapCahce[start] == null || dangerMap != null)
            {
                searchMap = DijkstraSearch(start, map, dangerMap);
            }
            else
            {
                searchMap = searchMapCahce[start];
            }
            
            var result = targets.Select(t => BuildPath(searchMap, start, t)).ToList();
            return result;
        }

        private static List<int> BFSSearch(int start, MapGraph map)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);

            var searchMap = Enumerable.Range(0, map.Vertices.Count)
                .Select(i => -1).ToList();
            while (queue.Count > 0)
            {
                var vertexIndex = queue.Dequeue();
                map.AdjacencyList[vertexIndex].ForEach(vInd =>
                {
                    if (searchMap[vInd] != -1)
                    {
                        return;
                    }

                    searchMap[vInd] = vertexIndex;
                    queue.Enqueue(vInd);
                });
            }

            return searchMap;
        }

        private class QueueNode:FastPriorityQueueNode
        {
            public int Index;
        }

        private static List<int> DijkstraSearch(int start, MapGraph map, List<double> dangerMap = null)
        {
            var queue = new FastPriorityQueue<QueueNode>(map.Vertices.Count);
            var nodesList = new List<QueueNode>(map.Vertices.Count);
            for(int i = 0; i < map.Vertices.Count; i++)
            {
                var node = new QueueNode { Index = i };
                queue.Enqueue(node, i == start ? 0 : double.PositiveInfinity);
                nodesList.Add(node);
            }
            var searchMap = Enumerable.Range(0, map.Vertices.Count)
                .Select(i => -1).ToList();

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                var curV = map.Vertices[currentNode.Index];
                map.AdjacencyList[currentNode.Index].ForEach(vInd =>
                {
                    var v = map.Vertices[vInd];
                    var newPriority =  currentNode.Priority + Math.Sqrt(Helper.SqrDist(v.X, v.Y, curV.X, curV.Y));
                    newPriority += dangerMap == null ? 0 : dangerMap[map.IndexOf(curV)];
                    if(nodesList[vInd].Priority > newPriority)
                    {
                        searchMap[vInd] = currentNode.Index;
                        queue.UpdatePriority(nodesList[vInd], newPriority);
                    }
                });
            }

            return searchMap;
        }

        private static PudgePath BuildPath(List<int> searchMap, int start, int target)
        {
            if (searchMap[target] == -1)
            {
                return null;
            }

            var path = new List<int>();
            int current = target;
            while (current != start)
            {
                path.Add(current);
                current = searchMap[current];
            }
            path.Add(start);
            path.Reverse();
            return new PudgePath(path);
        }
    }
}
