using Sketcher3D.GeometryEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketcher3D
{
    internal class FileHandle
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
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');

                }
            }
        }
    }
}


