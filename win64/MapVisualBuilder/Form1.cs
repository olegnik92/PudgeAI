using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapVisualBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SaveFileDialog.FileOk += new CancelEventHandler(ExportGraph);
            OpenFileDialog.FileOk += new CancelEventHandler(ImportGraph);

            LoadMap();
        }

        private byte[,] map;
        private GraphBuilder builder;
        private double wOffset = 0;
        private double hOffset = 0;
        private void LoadMap()
        {
            var rasterisator = new MapRasterisator();
            var mapInfo = new MapRawInfo();
            wOffset = mapInfo.WidthOffset;
            hOffset = mapInfo.HeightOffset;
            map = rasterisator.Rastr(mapInfo);
            Canvas.Width = pixelSize * map.GetLength(0);
            Canvas.Height = pixelSize * map.GetLength(1);
            builder = new GraphBuilder();
            BindingsConfig();
            CanvasRefresh();
        }

        BindingSource currrentVertexBindingSource;
        BindingSource edgeVertexBindingSource;
        private void BindingsConfig()
        {
            currrentVertexBindingSource = new BindingSource();
            currrentVertexBindingSource.DataSource = builder.Vertices;
            CurrentVertexComboBox.DataSource = currrentVertexBindingSource;
            CurrentVertexComboBox.DisplayMember = "ID";
            currrentVertexBindingSource.PositionChanged += (s, e) =>
            {
                var binding = (BindingSource)s;
                selectedVertices[0] = builder.Vertices[binding.Position];
                CanvasRefresh();
                RefreshIsEdgedCheckBox();
            };

            
            edgeVertexBindingSource = new BindingSource();
            edgeVertexBindingSource.DataSource = builder.Vertices;
            EdgedVertexComboBox.DataSource = edgeVertexBindingSource;
            EdgedVertexComboBox.DisplayMember = "ID";
            edgeVertexBindingSource.PositionChanged += (s, e) =>
            {
                var binding = (BindingSource)s;
                selectedVertices[1] = builder.Vertices[binding.Position];
                RefreshIsEdgedCheckBox();
            };

            XCoordTextBox.DataBindings.Clear();
            XCoordTextBox.DataBindings.Add("Text", currrentVertexBindingSource, "X", false, DataSourceUpdateMode.OnPropertyChanged);

            YCoordTextBox.DataBindings.Clear();
            YCoordTextBox.DataBindings.Add("Text", currrentVertexBindingSource, "Y", false, DataSourceUpdateMode.OnPropertyChanged);


            ProfitTextBox.DataBindings.Clear();
            ProfitTextBox.DataBindings.Add("Text", currrentVertexBindingSource, "Profit", false, DataSourceUpdateMode.OnPropertyChanged);

            DangerTextBox.DataBindings.Clear();
            DangerTextBox.DataBindings.Add("Text", currrentVertexBindingSource, "Danger", false, DataSourceUpdateMode.OnPropertyChanged);

            TagTextBox.DataBindings.Clear();
            TagTextBox.DataBindings.Add("Text", currrentVertexBindingSource, "Tag", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BindingsReset()
        {
            currrentVertexBindingSource.ResetBindings(false);
            edgeVertexBindingSource.ResetBindings(false);
        }

        private int pixelSize = int.Parse(ConfigurationSettings.AppSettings["pixelSize"]);
        private SolidBrush treeBrush = new SolidBrush(Color.Green);
        private SolidBrush pointBrush = new SolidBrush(Color.Blue);
        private int pointHalfSize = int.Parse(ConfigurationSettings.AppSettings["pointSize"]) / 2;
        private Pen edgePen = new Pen(Color.Blue, 2);
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {

        }

        private int InvertY(int y)
        {
            return Canvas.Height - y - pixelSize;
        }

        private void CanvasRefresh()
        {
            Canvas.SuspendLayout();
            var drawBuffer = new Bitmap(Canvas.Width, Canvas.Height);
            var driver = Graphics.FromImage(drawBuffer);
            driver.Clear(Color.White);
            if (map == null)
            {
                return;
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 1)
                    {
                        driver.FillRectangle(treeBrush, x * pixelSize, InvertY(y * pixelSize), pixelSize, pixelSize);
                    }
                }
            }


            builder.Vertices.ForEach(v =>
            {
                if (v.ID == selectedVertices[0].ID)
                {
                    pointBrush.Color = Color.Red;
                }
                else if (v.ID == selectedVertices[1].ID)
                {
                    pointBrush.Color = Color.Black;
                }
                else
                {
                    pointBrush.Color = Color.Blue;
                }
                driver.FillEllipse(pointBrush, (int)((v.X - wOffset) * pixelSize) - pointHalfSize, InvertY((int)((v.Y - hOffset) * pixelSize) + pointHalfSize), 2 * pointHalfSize, 2 * pointHalfSize);
            });

            builder.Edges.ForEach(edge =>
            {
                var v1 = builder.Vertices.First(v => v.ID == edge.Item1);
                var v2 = builder.Vertices.First(v => v.ID == edge.Item2);
                driver.DrawLine(edgePen, (int)((v1.X - wOffset) * pixelSize), InvertY((int)((v1.Y - hOffset) * pixelSize)),
                                         (int)((v2.X - wOffset) * pixelSize), InvertY((int)((v2.Y - hOffset) * pixelSize)));
            });

            Canvas.BackgroundImage = drawBuffer;
            Canvas.ResumeLayout();
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            var mouseE = (MouseEventArgs)e;
            var p = new Vertex
            {
                X = (double)mouseE.X / pixelSize + wOffset,
                Y = -((double)mouseE.Y / pixelSize + hOffset)
            };
            if(mouseE.Button == MouseButtons.Left)
            {
                builder.AddNewVertex(p.X, p.Y);
            } 
            else if(mouseE.Button == MouseButtons.Right)
            {
                SelectVertex(p);
            }

            BindingsReset();
            CanvasRefresh();
        }

        private List<Vertex> selectedVertices = new List<Vertex> { new Vertex { ID = -10 }, new Vertex { ID = -10 } };

        private void SelectVertex(Vertex p)
        {
            Vertex vertex = null;
            double minDist = double.PositiveInfinity;
            foreach(var v in builder.Vertices)
            {
                var dist = (v.X - p.X) * (v.X - p.X) + (v.Y - p.Y) * (v.Y - p.Y);
                if(dist < minDist)
                {
                    minDist = dist;
                    vertex = v;
                }
            }

            selectedVertices[1] = selectedVertices[0];
            selectedVertices[0] = vertex;


            CurrentVertexComboBox.SelectedIndex = builder.Vertices.IndexOf(selectedVertices[0]);
            EdgedVertexComboBox.SelectedIndex = builder.Vertices.IndexOf(selectedVertices[1]);
        }


        private void DeleteCurrentVertexButton_Click(object sender, EventArgs e)
        {
            builder.RemoveVertex(selectedVertices[0].ID);
            BindingsReset();
            CanvasRefresh();
        }

        private void IsEdgedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(IsEdgedCheckBox.Checked)
            {
                builder.AddEdge(selectedVertices[0].ID, selectedVertices[1].ID);
            }
            else
            {
                builder.RemveEdge(selectedVertices[0].ID, selectedVertices[1].ID);
            }

            CanvasRefresh();
        }

        private void RefreshIsEdgedCheckBox()
        {

            var edge = builder.Edges.FirstOrDefault(ed => (ed.Item1 == selectedVertices[0].ID && ed.Item2 == selectedVertices[1].ID)
                                                       || (ed.Item1 == selectedVertices[1].ID && ed.Item2 == selectedVertices[0].ID));

            IsEdgedCheckBox.Checked = (edge != null);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog.ShowDialog();
        }

        private void ExportGraph(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            File.WriteAllText(SaveFileDialog.FileName + ".json.txt", builder.Serialize());
            File.WriteAllLines(SaveFileDialog.FileName + ".seed.txt", builder.CreateMapSeed());
        }

        private void OpenGraphButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
        }

        private void ImportGraph(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            var json = File.ReadAllText(OpenFileDialog.FileName);
            builder = GraphBuilder.Deserialize(json);
            BindingsConfig();
            CanvasRefresh();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            CanvasRefresh();
        }

    }
}
