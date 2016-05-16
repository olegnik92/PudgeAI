
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapVisualBuilder
{
    class MapRasterisator
    {
        public double CellSize = double.Parse(ConfigurationSettings.AppSettings["cellSize"]);
        public double MinDistance = double.Parse(ConfigurationSettings.AppSettings["minDistance"]);// half of pudge size + half tree size

        public byte[,] Rastr(MapRawInfo mapData)
        {
            int wLen = (int)(mapData.MapWidth / CellSize);
            int hLen = (int)(mapData.MapHeight / CellSize);
            var cells = new byte[wLen, hLen];
            for (int i = 0; i < wLen; i++)
            {
                for (int j = 0; j < hLen; j++)
                {
                    bool isNotInTree = mapData.TreesCoords.All(treePos => treePos.GetDistanceTo(GetGridCellCenter(i, j, mapData)) > MinDistance);
                    cells[i, j] = isNotInTree ? (byte)0 : (byte)1;
                }
            }

            return cells;
        }


        private Vertex GetGridCellCenter(int widthIndex, int heightIndex, MapRawInfo mapData)
        {
            return new Vertex(CellSize * widthIndex + (CellSize / 2) + mapData.WidthOffset,
                               CellSize * heightIndex + (CellSize / 2) + mapData.HeightOffset);
            
        }
    }
}
