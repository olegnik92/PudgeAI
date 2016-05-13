using PudgeClient.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    static class Helper
    {

        public static MapVertex GetСlosestVertex(MapGraph map, double x, double y)
        {
            double minDist = double.PositiveInfinity;
            MapVertex result = null;
            map.Vertices.ForEach(vertex =>
            {
                double dist = SqrDist(x,y, vertex.X, vertex.Y);

                if (dist < minDist)
                {
                    minDist = dist;
                    result = vertex;
                }
            });

            return result;
        } 

        public static Tuple<MapVertex, MapVertex> GetClosestEdge(MapGraph map, double x, double y)
        {
            var closestVertex = GetСlosestVertex(map, x, y);
            var edges = map.AdjacencyList[map.IndexOf(closestVertex)]
                           .Select(i => Tuple.Create(closestVertex, map.Vertices[i]));
            double minDist = double.PositiveInfinity;
            var result = default(Tuple<MapVertex, MapVertex>);
            foreach(var edge in edges)
            {
                var dx = edge.Item2.X - edge.Item1.X;
                var dy = edge.Item1.Y - edge.Item2.Y;
                var dist = (dy * x + dx * y + (edge.Item1.X * edge.Item2.Y - edge.Item2.X * edge.Item1.Y)) /
                            Math.Sqrt(dx * dx + dy * dy);

                if (dist < minDist)
                {
                    minDist = dist;
                    result = edge;
                }
            }
            return result;
        }


        public static List<Tuple<MapVertex, MapVertex>> GetAllEdges(MapGraph map)
        {
            var result = new List<Tuple<MapVertex, MapVertex>>();
            for(int i=0; i< map.AdjacencyList.Count; i++)
            {
                map.AdjacencyList[i].ForEach(j =>
                {
                    if (j > i)
                    {
                        result.Add(Tuple.Create(map.Vertices[i], map.Vertices[j]));
                    }
                });
            }

            return result;
        }

        public static double SqrDist(double x1, double y1, double x2, double y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }



        public static double GetMoveAngle(double directionX, double directionY)
        {
            if (Math.Abs(directionX) < 0.001)
            {
                return directionY > 90 ? 0 : 270;
            }
            var angle = Math.Atan(directionY / directionX);


            return NormalizeAngle((directionX > 0 ? angle : (angle + Math.PI)) * (180 / Math.PI));
        }


        public static double GetAngleDif(double from, double to)
        {
            from = NormalizeAngle(from);
            to = NormalizeAngle(to);

            var dif = to - from;
            if(Math.Abs(dif) < 180)
            {
                return dif;
            }

            if (dif > 0)
            {
                return -(360 - dif);
            }
            else
            {
                return 360 + dif;
            }
        }


        public static double NormalizeAngle(double a)
        {
            while (a < 0)
            {
                a += 360;
            }

            return a;
        }


    }
}
