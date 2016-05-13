﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MapVisualBuilder
{
    class MapRawInfo
    {
        public const double TreeSize = 16;

        public double MapWidth = 340;
        public double MapHeight = 340;


        public double WidthOffset = -170;
        public double HeightOffset = -170;

        public MapRawInfo(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
        }

        private List<Vertex> treesCoords = null;
        public List<Vertex> TreesCoords
        {
            get
            {
                if (treesCoords != null)
                {
                    return treesCoords;
                }

                var data = JsonConvert.DeserializeObject<List<List<double>>>(TreesJsonData);
                treesCoords = data.Select(item => new Vertex(item[0], item[1])).ToList();
                return treesCoords;
            }
        }


        private string TreesJsonData
        {
            get
            {
                return @"
[
    [-160, 160],
    [-150, 160],
    [-140, 160],
    [-130, 160],
    [-120, 160],
    [-110, 160],
    [-100, 160],
    [-90, 160],
    [-80, 160],
    [-70, 160],
    [-60, 160],
    [-50, 160],
    [-40, 160],
    [-30, 160],
    [-20, 160],
    [-10, 160],
    [0, 160],
    [10, 160],
    [20, 160],
    [30, 160],
    [40, 160],
    [50, 160],
    [60, 160],
    [70, 160],
    [80, 160],
    [90, 160],
    [100, 160],
    [110, 160],
    [120, 160],
    [130, 160],
    [140, 160],
    [150, 160],
    [-160, 160],
    [-160, 150],
    [-160, 140],
    [-160, 130],
    [-160, 120],
    [-160, 110],
    [-160, 100],
    [-160, 90],
    [-160, 80],
    [-160, 70],
    [-160, 60],
    [-160, 50],
    [-160, 40],
    [-160, 30],
    [-160, 20],
    [-160, 10],
    [-160, 0],
    [-160, -10],
    [-160, -20],
    [-160, -30],
    [-160, -40],
    [-160, -50],
    [-160, -60],
    [-160, -70],
    [-160, -80],
    [-160, -90],
    [-160, -100],
    [-160, -110],
    [-160, -120],
    [-160, -130],
    [-160, -140],
    [-160, -150],
    [160, -160],
    [150, -160],
    [140, -160],
    [130, -160],
    [120, -160],
    [110, -160],
    [100, -160],
    [90, -160],
    [80, -160],
    [70, -160],
    [60, -160],
    [50, -160],
    [40, -160],
    [30, -160],
    [20, -160],
    [10, -160],
    [0, -160],
    [-10, -160],
    [-20, -160],
    [-30, -160],
    [-40, -160],
    [-50, -160],
    [-60, -160],
    [-70, -160],
    [-80, -160],
    [-90, -160],
    [-100, -160],
    [-110, -160],
    [-120, -160],
    [-130, -160],
    [-140, -160],
    [-150, -160],
    [160, -160],
    [160, -150],
    [160, -140],
    [160, -130],
    [160, -120],
    [160, -110],
    [160, -100],
    [160, -90],
    [160, -80],
    [160, -70],
    [160, -60],
    [160, -50],
    [160, -40],
    [160, -30],
    [160, -20],
    [160, -10],
    [160, 0],
    [160, 10],
    [160, 20],
    [160, 30],
    [160, 40],
    [160, 50],
    [160, 60],
    [160, 70],
    [160, 80],
    [160, 90],
    [160, 100],
    [160, 110],
    [160, 120],
    [160, 130],
    [160, 140],
    [160, 150],
    [-60, 0],
    [-70, -10],
    [-70, -20],
    [-80, -30],
    [-90, -40],
    [-100, -50],
    [-100, -60],
    [0, -60],
    [-10, -70],
    [-20, -70],
    [-30, -80],
    [-40, -90],
    [-50, -100],
    [-60, -100],
    [60, 0],
    [70, 10],
    [70, 20],
    [80, 30],
    [90, 40],
    [100, 50],
    [100, 60],
    [0, 60],
    [10, 70],
    [20, 70],
    [30, 80],
    [40, 90],
    [50, 100],
    [60, 100],
    [40, -80],
    [30, -70],
    [20, -70],
    [10, -60],
    [-40, 80],
    [-30, 70],
    [-20, 70],
    [-10, 60],
    [80, -40],
    [70, -30],
    [70, -20],
    [60, -10],
    [-80, 40],
    [-70, 30],
    [-70, 20],
    [-60, 10],
    [0, -150],
    [0, -140],
    [0, -130],
    [0, 150],
    [0, 140],
    [0, 130],
    [150, 0],
    [140, 0],
    [130, 0],
    [-150, 0],
    [-140, 0],
    [-130, 0],
    [140, -80],
    [130, -80],
    [120, -80],
    [80, -140],
    [80, -130],
    [80, -120],
    [-140, 80],
    [-130, 80],
    [-120, 80],
    [-80, 140],
    [-80, 130],
    [-80, 120]
]
                ";
            }
        }
    }
}
