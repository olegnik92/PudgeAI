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
				new MapVertex{ X = -44, Y = -125, Profit = 0, Danger = 0, Tag = "", ID = 1 },
				new MapVertex{ X = 0.333333333333343, Y = -98.3333333333333, Profit = 10, Danger = 0, Tag = "", ID = 2 },
				new MapVertex{ X = 130, Y = -130, Profit = 10, Danger = 0, Tag = "", ID = 3 },
				new MapVertex{ X = -130, Y = 130, Profit = 10, Danger = 0, Tag = "", ID = 4 },
				new MapVertex{ X = 130, Y = 130, Profit = 0, Danger = 0, Tag = "", ID = 5 },
				new MapVertex{ X = 44.3333333333333, Y = -103, Profit = 0, Danger = 0, Tag = "", ID = 6 },
				new MapVertex{ X = 99.3333333333333, Y = -99.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 7 },
				new MapVertex{ X = 92.6666666666667, Y = -65.3333333333333, Profit = 0, Danger = 0, Tag = "", ID = 8 },
				new MapVertex{ X = 111.333333333333, Y = -32, Profit = 0, Danger = 0, Tag = "", ID = 9 },
				new MapVertex{ X = 99, Y = -0.333333333333343, Profit = 10, Danger = 0, Tag = "", ID = 10 },
				new MapVertex{ X = 125.666666666667, Y = 37.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 11 },
				new MapVertex{ X = 128, Y = 80, Profit = 0, Danger = 0, Tag = "", ID = 12 },
				new MapVertex{ X = 95.6666666666667, Y = 89.3333333333333, Profit = 0, Danger = 0, Tag = "", ID = 13 },
				new MapVertex{ X = 0, Y = 0, Profit = 10, Danger = 0, Tag = "", ID = 14 },
				new MapVertex{ X = -89, Y = -87.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 15 },
				new MapVertex{ X = -82, Y = -120, Profit = 0, Danger = 0, Tag = "", ID = 16 },
				new MapVertex{ X = -120, Y = -82, Profit = 0, Danger = 0, Tag = "", ID = 17 },
				new MapVertex{ X = -125.666666666667, Y = -33.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 18 },
				new MapVertex{ X = -98.6666666666667, Y = 0.333333333333343, Profit = 10, Danger = 0, Tag = "", ID = 19 },
				new MapVertex{ X = -105.666666666667, Y = 40.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 20 },
				new MapVertex{ X = -79, Y = 76.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 21 },
				new MapVertex{ X = -40.3333333333333, Y = 111.333333333333, Profit = 0, Danger = 0, Tag = "", ID = 22 },
				new MapVertex{ X = -0.333333333333343, Y = 99, Profit = 10, Danger = 0, Tag = "", ID = 23 },
				new MapVertex{ X = 44.6666666666667, Y = 129, Profit = 0, Danger = 0, Tag = "", ID = 24 },
				new MapVertex{ X = 81.3333333333333, Y = 117, Profit = 0, Danger = 0, Tag = "", ID = 25 },
				new MapVertex{ X = 65.6666666666667, Y = -64.6666666666667, Profit = 0, Danger = 0, Tag = "", ID = 26 },
				new MapVertex{ X = 67.3333333333333, Y = -87.3333333333333, Profit = 0, Danger = 0, Tag = "", ID = 27 },
			},
                AdjacencyList = new List<List<int>>
			{
				new List<int>{ 18, 1, 15 }, // 0
				new List<int>{ 16, 2, 0 }, // 1
				new List<int>{ 1, 6 }, // 2
				new List<int>{ 7 }, // 3
				new List<int>{ 21 }, // 4
				new List<int>{ 12, 13, 25 }, // 5
				new List<int>{ 7, 2, 27 }, // 6
				new List<int>{ 6, 3, 8, 26 }, // 7
				new List<int>{ 7, 9, 26, 27 }, // 8
				new List<int>{ 8, 10 }, // 9
				new List<int>{ 9, 11 }, // 10
				new List<int>{ 10, 12 }, // 11
				new List<int>{ 11, 5, 13, 25 }, // 12
				new List<int>{ 5, 12, 25, 14 }, // 13
				new List<int>{ 21, 26, 13, 15 }, // 14
				new List<int>{ 17, 16, 14, 0 }, // 15
				new List<int>{ 15, 1, 17 }, // 16
				new List<int>{ 18, 15, 16 }, // 17
				new List<int>{ 0, 17, 19 }, // 18
				new List<int>{ 20, 18 }, // 19
				new List<int>{ 21, 19 }, // 20
				new List<int>{ 22, 20, 4, 14 }, // 21
				new List<int>{ 23, 21 }, // 22
				new List<int>{ 24, 22 }, // 23
				new List<int>{ 25, 23 }, // 24
				new List<int>{ 13, 24, 5, 12 }, // 25
				new List<int>{ 14, 8, 27, 7 }, // 26
				new List<int>{ 26, 6, 8 }, // 27
			}
            };
        }
    }


}
