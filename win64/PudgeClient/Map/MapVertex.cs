using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.Map
{
    class DiscreteMapVertex
    {
        public DiscreteMapVertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;

        public int Y;

        public double Profit = 0;

        public double Danger = 0;
    }

    class MapVertex
    {
        public int ID;

        public double X;

        public double Y;

        public double Profit = 0;

        public double Danger = 0;

        public string Tag; 
    }

}
