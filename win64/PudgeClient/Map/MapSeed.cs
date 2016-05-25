using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.Map
{
    class MapSeed
    {
        public MapGraph CreateMapGraph()
        {
            return new MapGraph
            {
                Vertices = new List<MapVertex>
            {
                new MapVertex{ X = -130, Y = -130, Profit = 0, Danger = 0, Tag = "", ID = 0 },
                new MapVertex{ X = 130, Y = 130, Profit = 0, Danger = 0, Tag = "", ID = 1 },
                new MapVertex{ X = 0, Y = 0, Profit = 10, Danger = 0, Tag = "", ID = 2 },
                new MapVertex{ X = 130, Y = -130, Profit = 10, Danger = 0, Tag = "", ID = 3 },
                new MapVertex{ X = -130, Y = 130, Profit = 10, Danger = 0, Tag = "", ID = 4 },
                new MapVertex{ X = 0, Y = 100, Profit = 10, Danger = 0, Tag = "", ID = 5 },
                new MapVertex{ X = 0, Y = -100, Profit = 10, Danger = 0, Tag = "", ID = 6 },
                new MapVertex{ X = -100, Y = 0, Profit = 10, Danger = 0, Tag = "", ID = 7 },
                new MapVertex{ X = 100, Y = 0, Profit = 10, Danger = 0, Tag = "", ID = 8 },
                new MapVertex{ X = -125, Y = -35, Profit = 0, Danger = 0, Tag = "", ID = 9 },
                new MapVertex{ X = -35, Y = -125, Profit = 0, Danger = 0, Tag = "", ID = 10 },
                new MapVertex{ X = 39.5, Y = 127, Profit = 0, Danger = 0, Tag = "", ID = 11 },
                new MapVertex{ X = 131, Y = 39.5, Profit = 0, Danger = 0, Tag = "", ID = 12 },
                new MapVertex{ X = -128.5, Y = -79.5, Profit = 0, Danger = 0, Tag = "", ID = 13 },
                new MapVertex{ X = -80, Y = -128, Profit = 0, Danger = 0, Tag = "", ID = 14 },
                new MapVertex{ X = -105, Y = -105, Profit = 0, Danger = 0, Tag = "", ID = 15 },
                new MapVertex{ X = 80, Y = 128, Profit = 0, Danger = 0, Tag = "", ID = 16 },
                new MapVertex{ X = 128, Y = 85, Profit = 0, Danger = 0, Tag = "", ID = 17 },
                new MapVertex{ X = 105, Y = 105, Profit = 0, Danger = 0, Tag = "", ID = 18 },
                new MapVertex{ X = -105, Y = 45, Profit = 0, Danger = 0, Tag = "", ID = 19 },
                new MapVertex{ X = -79, Y = 77.5, Profit = 0, Danger = 0, Tag = "", ID = 20 },
                new MapVertex{ X = -41, Y = 111, Profit = 0, Danger = 0, Tag = "", ID = 21 },
                new MapVertex{ X = 78, Y = -78, Profit = 0, Danger = 0, Tag = "", ID = 22 },
                new MapVertex{ X = 116, Y = -41, Profit = 0, Danger = 0, Tag = "", ID = 23 },
                new MapVertex{ X = 41.5, Y = -109.5, Profit = 0, Danger = 0, Tag = "", ID = 24 },
            },
                AdjacencyList = new List<List<int>>
            {
                new List<int>{ 15, 14, 13 }, // 0
				new List<int>{ 18, 17, 16 }, // 1
				new List<int>{ 20, 22, 15, 18 }, // 2
				new List<int>{ 22 }, // 3
				new List<int>{ 20 }, // 4
				new List<int>{ 11, 21 }, // 5
				new List<int>{ 10, 24 }, // 6
				new List<int>{ 19, 9 }, // 7
				new List<int>{ 23, 12 }, // 8
				new List<int>{ 7, 13 }, // 9
				new List<int>{ 14, 6 }, // 10
				new List<int>{ 16, 5 }, // 11
				new List<int>{ 8, 17 }, // 12
				new List<int>{ 9, 15, 0 }, // 13
				new List<int>{ 15, 10, 0 }, // 14
				new List<int>{ 13, 14, 2, 0 }, // 15
				new List<int>{ 18, 11, 1 }, // 16
				new List<int>{ 12, 18, 1 }, // 17
				new List<int>{ 17, 16, 2, 1 }, // 18
				new List<int>{ 20, 7 }, // 19
				new List<int>{ 4, 19, 21, 2 }, // 20
				new List<int>{ 5, 20 }, // 21
				new List<int>{ 24, 3, 23, 2 }, // 22
				new List<int>{ 22, 8 }, // 23
				new List<int>{ 6, 22 }, // 24
			}
            };
        }
    }









}
