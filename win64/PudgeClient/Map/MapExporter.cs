//using AIRLab.Mathematics;
//using Pudge;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PudgeClient.Map
//{
//    class MapExporter
//    {        
//        public const double CellSize = 5;
//        public const double MinDistance = 10;// half of pudge size

//        public const double MapWidth = 340;
//        public const double MapHeight = 340;



//        public void Export()
//        {
//            var treesData = new MapTreesData();
//            int wLen = (int)(MapWidth / CellSize);
//            int hLen = (int)(MapHeight / CellSize);
//            var freeCells = new bool[wLen, hLen];
//            for (int i = 0; i < wLen; i++)
//            {
//                for (int j = 0; j < hLen; j++)
//                {
//                    freeCells[i, j] = treesData.TreesCoords.All(treePos => treePos.GetDistanceTo(GetGridCellCenter(i, j)) > MinDistance);
//                }
//            }


//            var lines = new List<string>();
//            for (int i = 0; i < wLen; i++)
//            {
//                var line = "";
//                for (int j = 0; j < hLen; j++)
//                {
//                    Console.Write(freeCells[i, j] ? ' ' : 'X');
//                    line += freeCells[i, j] ? ' ' : 'X';
//                }
//                Console.Write('\n');
//                lines.Add(line);
//            }
//            File.WriteAllLines("mapTest.txt", lines);
//        }

//        private Point2D GetGridCellCenter(int widthIndex, int heightIndex)
//        {
//            return new Point2D(CellSize * widthIndex + (CellSize / 2) - (MapWidth / 2),
//                               CellSize * heightIndex + (CellSize / 2) - (MapHeight / 2));
            
//        }
//    }
//}
