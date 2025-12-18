using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    public class Pyramid : Shape
    {
        private double mBaseLength;
        private double mBaseWidth;
        private double mHeight;

        public Pyramid(string name, double baseLength, double baseWidth, double height) : base("Pyramid", name)
        {
            mBaseLength = baseLength;
            mBaseWidth = baseWidth;
            mHeight = height;
            Build();
        }

        public double GetLength() { return mBaseLength; }
        public double GetWidth() { return mBaseWidth; }
        public double GetHeight() { return mHeight; }
        public double GetSlantHeight()
        {
            Point p1 = new Point();
            Point hPt = new Point(0, 0, mHeight);
            return p1.Distance(hPt);
        }

        protected override void Build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            double halfL = mBaseLength / 2.0; // center as origin
            double halfW = mBaseWidth / 2.0;

            int p0Ind = mTriangulation.GetPointIndex(new Point(x + halfL, y + halfW, z)); //b1 base points
            int p1Ind = mTriangulation.GetPointIndex(new Point(x + halfL, y - halfW, z)); //b2
            int p2Ind = mTriangulation.GetPointIndex(new Point(x - halfL, y - halfW, z)); //b3
            int p3Ind = mTriangulation.GetPointIndex(new Point(x - halfL, y + halfW, z)); //b4

            mTriangulation.AddTriangle(p0Ind, p2Ind, p3Ind);//base
            mTriangulation.AddTriangle(p2Ind, p0Ind, p1Ind);//base

            int apexInd = mTriangulation.GetPointIndex(new Point(x, y, z + mHeight)); //Apex

            mTriangulation.AddTriangle(p1Ind, p0Ind, apexInd);
            mTriangulation.AddTriangle(p2Ind, p1Ind, apexInd);
            mTriangulation.AddTriangle(p3Ind, p2Ind, apexInd);
            mTriangulation.AddTriangle(p0Ind, p3Ind, apexInd);
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} " +
                $"L {GetLength()} W {GetWidth()} H {GetHeight()}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();

            double x = 0;
            double y = 0;
            double z = 0;

            double halfL = mBaseLength / 2.0; // center as origin
            double halfW = mBaseWidth / 2.0;

            pts.Add(new Point(x + halfL, y + halfW, z)); //b1 base points
            pts.Add(new Point(x + halfL, y - halfW, z)); //b2
            pts.Add(new Point(x - halfL, y - halfW, z)); //b3
            pts.Add(new Point(x - halfL, y + halfW, z)); //b4
            pts.Add(new Point(x + halfL, y + halfW, z)); //b1
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x + halfL, y + halfW, z)); //b1
            pts.Add(new Point(x, y, z + mHeight));//Apex point
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x + halfL, y - halfW, z)); //b2
            pts.Add(new Point(x, y, z + mHeight));//Apex point
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x - halfL, y - halfW, z)); //b3
            pts.Add(new Point(x, y, z + mHeight));//Apex point
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x - halfL, y + halfW, z)); //b4
            pts.Add(new Point(x, y, z + mHeight));//Apex point
            vec.Add(pts);
            pts.Clear();

            foreach (var ptsVec in vec)
            {
                foreach (var pt in ptsVec)
                {
                    //pt.WriteXYZ(writer);
                    writer.Write(pt.GetX()); writer.Write(" ");
                    writer.Write(pt.GetY()); writer.Write(" ");
                    writer.Write(pt.GetZ()); writer.Write(" ");
                }
                writer.Write("\n\n");
            }
            writer.Write("\n\n");
        }
    }
}
