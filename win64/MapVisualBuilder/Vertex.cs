using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapVisualBuilder
{
    class Vertex
    {
        public int ID { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Profit { get; set; }

        public double Danger { get; set; }

        public string Tag { get; set; }

        public Vertex()
        {

        }

        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
        }


        public double GetDistanceTo(Vertex v)
        {
            return Math.Sqrt((X - v.X) * (X - v.X) + (Y - v.Y) * (Y - v.Y));
        }
    }
}
