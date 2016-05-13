using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MapVisualBuilder
{
    class GraphBuilder
    {
        public int seed = 0;

        public List<Vertex> Vertices = new List<Vertex>();

        public List<Tuple<int, int>> Edges = new List<Tuple<int,int>>();

        public int AddNewVertex(double x, double y)
        {
            var vertex = new Vertex(x, y);
            vertex.ID = seed;
            seed++;
            Vertices.Add(vertex);
            return vertex.ID;
        }

        public void RemoveVertex(int id)
        {
            var vertexToRemove = Vertices.FirstOrDefault(v => v.ID == id);
            if(vertexToRemove == null)
            {
                return;
            }

            int num = Vertices.IndexOf(vertexToRemove);
            Vertices.RemoveAt(num);
            Edges = Edges.Where(edge => edge.Item1 != id && edge.Item2 != id).ToList();
        }

        public void AddEdge(int id1, int id2)
        {
            int v1 = Math.Min(id1, id2);
            int v2 = Math.Max(id1, id2);

            if(v1 < 0)
            {
                return;
            }

            var edge = Edges.FirstOrDefault(ed => ed.Item1 == v1 && ed.Item2 == v2);
            if(edge != null)
            {
                return;
            }

            Edges.Add(new Tuple<int, int>(v1, v2));
        }

        public void RemveEdge(int id1, int id2)
        {
            int v1 = Math.Min(id1, id2);
            int v2 = Math.Max(id1, id2);

            if (v1 < 0)
            {
                return;
            }

            var edge = Edges.FirstOrDefault(ed => ed.Item1 == v1 && ed.Item2 == v2);
            if (edge == null)
            {
                return;
            }

            Edges.RemoveAt(Edges.IndexOf(edge));
        }

        public string Serialize()
        {
            var result = JsonConvert.SerializeObject(this);
            return result;
        }

        public static GraphBuilder Deserialize(string json)
        {
            var instanse = JsonConvert.DeserializeObject<GraphBuilder>(json);
            return instanse;
        }


        public List<String> CreateMapSeed()
        {
            var result = new List<string>();
            result.Add("class MapSeed");
            result.Add("{");
            result.Add("\tpublic MapGraph CreateMapGraph()");
            result.Add("\t{");
            result.Add("\t\treturn new MapGraph");
            result.Add("\t\t{");


            result.Add("\t\t\tVertices = new List<MapVertex>");
            result.Add("\t\t\t{");
            Vertices.ForEach(v =>
            {
                result.Add(string.Format(CultureInfo.InvariantCulture, "\t\t\t\tnew MapVertex{{ X = {0}, Y = {1}, Profit = {2}, Danger = {3}, Tag = \"{4}\", ID = {5} }},", 
                    v.X, v.Y, v.Profit, v.Danger, v.Tag, Vertices.IndexOf(v)));
            });
            result.Add("\t\t\t},");//result.Add("Vertices = new List<MapVertex>");


            result.Add("\t\t\tAdjacencyList = new List<List<int>>");
            result.Add("\t\t\t{");
            Vertices.ForEach(v =>
            {
                var list = CreateAdjacencyString(v);
                result.Add(string.Format("\t\t\t\tnew List<int>{{ {0} }}, // {1}", list, Vertices.IndexOf(v)));
            });
            result.Add("\t\t\t}");//result.Add("AdjacencyList = new List<List<int>>");


            result.Add("\t\t};");//result.Add("return new MapGraph");
            result.Add("\t}");//result.Add("public MapGraph CreateMapGraph()");
            result.Add("}");//result.Add("class MapSeed");
            return result;
        }

        private string CreateAdjacencyString(Vertex v)
        {
            var vertexList = new List<int>();
            var edges = Edges.Where(edge => edge.Item1 == v.ID || edge.Item2 == v.ID);
            foreach(var edge in edges)
            {
                int id = edge.Item1 == v.ID ? edge.Item2 : edge.Item1;
                var vertex = Vertices.FirstOrDefault(ver => ver.ID == id);
                vertexList.Add(Vertices.IndexOf(vertex));
            }
            return string.Join(", ", vertexList);
        }
    }
}
