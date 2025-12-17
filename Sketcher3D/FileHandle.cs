using Sketcher3D.GeometryEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketcher3D
{
    public class FileHandle
    {
        public FileHandle() { }

        public static bool SaveToFile(string fileName, List<Shape> shapes)
        {
            using (var write = new StreamWriter(fileName))
            {
                foreach (var s in shapes)
                    s.Save(write);     
            }
            return true;
        }

        public static bool SaveToFileGNUPlot(string fileName, List<Shape> shapes)
        {
            using (var write = new StreamWriter(fileName))
            {
                
                foreach (var shape in shapes)
                {
                    write.WriteLine($"{shape.GetShapeType()}");
                    write.WriteLine($"{shape.GetShapeName()}");
                    shape.SaveForGNU(write);
                }  
            }
            return true;
        }

        public static void ReadSTL(string filename, Triangulation tri)
        {
            //List<float> vertices = new List<float>();
            List<Point> points = new List<Point>();

            Point normal = new Point();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("facet normal"))
                    {
                        string[] words = line.Split(' ');
                        float x = float.Parse(words[2]);
                        float y = float.Parse(words[3]);
                        float z = float.Parse(words[4]);

                        normal.SetX(x);
                        normal.SetY(y);
                        normal.SetZ(z);

                    }

                    if (line.Contains("vertex"))
                    {
                        string[] words = line.Split(' ');
                        float x = float.Parse(words[1]);
                        float y = float.Parse(words[2]);
                        float z = float.Parse(words[3]);

                        points.Add(new Point(x, y, z));
                        if (points.Count == 3)
                        {
                            int p1 = tri.GetPointIndex(points[0]);
                            int p2 = tri.GetPointIndex(points[1]);
                            int p3 = tri.GetPointIndex(points[2]);
                            tri.AddTriangle(p1, p2, p3, normal);
                            points.Clear();
                        }
                    }
                }
            }


        }
    }
}


