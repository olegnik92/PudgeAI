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
        private List<int> vertices;
        private Dictionary<int, bool> vertexReachMap;
        public PudgePath(List<int> vertices)
        {
            this.vertices = vertices;
            vertexReachMap = new Dictionary<int, bool>();
            vertices.ForEach(v =>
            {
                vertexReachMap[v] = false;
            });
        }

        public void VertexReached(int vertex)
        {
            vertexReachMap[vertex] = true;
        }

        public bool IsEqualTo(PudgePath path)
        {
            PudgePath longPath, shortPath;
            if (path.vertices.Count > this.vertices.Count)
            {
                longPath = path;
                shortPath = this;
            }
            else
            {
                longPath = this;
                shortPath = path;
            }

            var lenDif = longPath.vertices.Count - shortPath.vertices.Count;
            for (int i = longPath.vertices.Count - 1; i > -1; i--)
            {
                int j = i - lenDif;
                if(j < 0)
                {
                    if (longPath.vertexReachMap[longPath.vertices[i]])
                    {
                        continue;
                    }

                    return false;
                }
          
                if(longPath.vertices[i] != shortPath.vertices[j]
                    && (!longPath.vertexReachMap[longPath.vertices[i]] || !shortPath.vertexReachMap[shortPath.vertices[j]]))
                {
                    return false;
                }
            }


            return true;
        }


        public double Length
        {
            get
            {
                return vertices.Count;
            }
        }

        public int GetCurrentTargetIndex()
        {
            for (int i = 0; i < vertices.Count; i++ )
            {
                if(!vertexReachMap[vertices[i]])
                {
                    return vertices[i];
                }
            }

            return vertices[vertices.Count - 1];
        }

        public bool IsPathDone()
        {
            return vertices.All(v => vertexReachMap[v]);
        }



        private static List<List<int>> searchMapCahce = new List<List<int>>();
        public static void BuildSearchMapCache(MapGraph map)
        {
            searchMapCahce = map.Vertices.Select(v => (List<int>)(null)).ToList();
            Parallel.For(0, map.Vertices.Count, start =>
            {
                searchMapCahce[start] = BFSSearch(start, map);
            });
        }


        public static List<PudgePath> FindPath(IEnumerable<int> targets, int start, MapGraph map)
        {
            List<int> searchMap; 
            if(searchMapCahce.Count < map.Vertices.Count || searchMapCahce[start] == null)
            {
                searchMap = BFSSearch(start, map);
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
