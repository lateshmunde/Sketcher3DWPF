using System;
using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    public class Cylinder : Shape
    {
        private double mRadius;
        private double mHeight;

        public Cylinder(string name, double radius, double height) : base("Cone", name)
        {
            this.mRadius = radius;
            this.mHeight = height;
            Build();
        }

        public double GetRadius() { return mRadius; }
        public double GetHeight() { return mHeight; }

        protected override void Build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            Point baseCenter = new Point(x, y, z);
            Point topCenter = new Point(x, y, mHeight);

            List<int> bPtsIndex = new List<int>();
            List<int> tPtsIndex = new List<int>();

            int baseCenterInd = mTriangulation.AddPoint(baseCenter);
            int topCenterInd = mTriangulation.AddPoint(topCenter);

            bPtsIndex.Add(mTriangulation.AddPoint(new Point(x + mRadius * Math.Cos(0), y + mRadius * Math.Sin(0), z)));
            tPtsIndex.Add(mTriangulation.AddPoint(new Point(x + mRadius * Math.Cos(0), y + mRadius * Math.Sin(0), z + mHeight)));

            int number = 72;
            double dTheta = 2 * Math.PI / number; // 0 to 180

            for (int i = 1; i <= number; i++)
            {
                double theta = i * dTheta;
                double x_ = mRadius * Math.Cos(theta);
                double y_ = mRadius * Math.Sin(theta);

                bPtsIndex.Add(mTriangulation.AddPoint(new Point(x + x_, y + y_, z)));
                tPtsIndex.Add(mTriangulation.AddPoint(new Point(x + x_, y + y_, z + mHeight)));

                // each 5 degree section has 4 triangles.
                mTriangulation.AddTriangle(bPtsIndex[i], bPtsIndex[i - 1], baseCenterInd);      // Base circle center, two points on it's circumference
                mTriangulation.AddTriangle(bPtsIndex[i - 1], bPtsIndex[i], tPtsIndex[i]);       // Cylinder surface triangle: b1, t1, b0 
                mTriangulation.AddTriangle(bPtsIndex[i - 1], tPtsIndex[i], tPtsIndex[i - 1]);   // Cylinder surface triangle: b0, t1, t0
                mTriangulation.AddTriangle(topCenterInd, tPtsIndex[i - 1], tPtsIndex[i]);       // Top circle center, two points on it's circumference

            }
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} " +
                $"R {GetRadius()} H {GetHeight()}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();

            double x = 0;
            double y = 0;
            double z = 0;

            int number = 72;
            double dTheta = 2 * Math.PI / number; // 0 to 180   

            for (int i = 0; i <= number; i++) //base
            {
                double theta = i * dTheta;
                double x1 = mRadius * Math.Cos(theta);
                double y1 = mRadius * Math.Sin(theta);
                pts.Add(new Point(x + x1, y + y1, z));
            }
            double x2 = mRadius * Math.Cos(0); // first point
            double y2 = mRadius * Math.Sin(0);
            pts.Add(new Point(x + x2, y + y2, z));
            vec.Add(pts);
            pts.Clear();

            for (int i = 0; i <= number; i++) //// height
            {
                double theta = i * dTheta;
                double x1 = mRadius * Math.Cos(theta);
                double y1 = mRadius * Math.Sin(theta);
                pts.Add(new Point(x + x1, y + y1, z + mHeight));
            }
            x2 = mRadius * Math.Cos(0); // first point
            y2 = mRadius * Math.Sin(0);
            pts.Add(new Point(x + x2, y + y2, z + mHeight));
            vec.Add(pts);
            pts.Clear();

            for (int i = 0; i <= number; i++) //base
            {
                double theta = i * dTheta;
                double x1 = mRadius * Math.Cos(theta);
                double y1 = mRadius * Math.Sin(theta);
                pts.Add(new Point(x + x1, y + y1, z));
                pts.Add(new Point(x + x1, y + y1, z + mHeight));
                vec.Add(pts);
                pts.Clear();
            }

            foreach (var ptsVec in vec)
            {
                foreach (var pt in ptsVec)
                {
                    pt.WriteXYZ(writer);
                }
                writer.Write("\n\n");
            }
            writer.Write("\n\n");
        }
    }
}
