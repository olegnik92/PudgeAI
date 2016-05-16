using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MapVisualBuilder
{
    class MapRawInfo
    {

        public double MapWidth = double.Parse(ConfigurationSettings.AppSettings["mapWidth"]);

        public double MapHeight = double.Parse(ConfigurationSettings.AppSettings["mapHeight"]);

        public double WidthOffset = double.Parse(ConfigurationSettings.AppSettings["mapWidthOffset"]);

        public double HeightOffset = double.Parse(ConfigurationSettings.AppSettings["mapHeightOffset"]);

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
                var filePath = ConfigurationSettings.AppSettings["mapPath"];
                return File.ReadAllText(filePath);
            }
        }
    }
}
