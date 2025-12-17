using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    public class Cube : Shape
    {
        private double mSide;

        public Cube(string name, double side) : base ("Cube", name)
        {
            this.mSide = side;
            Build();
        }

        public double GetSide() { return mSide; }

        protected override void Build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            int p0Ind = mTriangulation.GetPointIndex(new Point(x, y, z));
            int p1Ind = mTriangulation.GetPointIndex(new Point(x + mSide, y, z));
            int p2Ind = mTriangulation.GetPointIndex(new Point(x + mSide, y + mSide, z));

            mTriangulation.AddTriangle(p0Ind, p2Ind, p1Ind); // front

            int p3Ind = mTriangulation.GetPointIndex(new Point(x, y + mSide, z));
            mTriangulation.AddTriangle(p0Ind, p3Ind, p2Ind); // front

            int p4Ind = mTriangulation.GetPointIndex(new Point(x, y, z + mSide));
            int p5Ind = mTriangulation.GetPointIndex(new Point(x + mSide, y, z + mSide));
            int p6Ind = mTriangulation.GetPointIndex(new Point(x + mSide, y + mSide, z + mSide));

            mTriangulation.AddTriangle(p4Ind, p5Ind, p6Ind); // back

            int p7Ind = mTriangulation.GetPointIndex(new Point(x, y + mSide, z + mSide));
            mTriangulation.AddTriangle(p4Ind, p6Ind, p7Ind); // back

            mTriangulation.AddTriangle(p7Ind, p6Ind, p2Ind); // top
            mTriangulation.AddTriangle(p7Ind, p2Ind, p3Ind); // top

            mTriangulation.AddTriangle(p0Ind, p1Ind, p5Ind); // bottom
            mTriangulation.AddTriangle(p0Ind, p5Ind, p4Ind); // bottom

            mTriangulation.AddTriangle(p5Ind, p1Ind, p2Ind); // right
            mTriangulation.AddTriangle(p5Ind, p2Ind, p6Ind); // right

            mTriangulation.AddTriangle(p0Ind, p4Ind, p7Ind); // left
            mTriangulation.AddTriangle(p0Ind, p7Ind, p3Ind); // left
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} S {GetSide()} ");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();

            double x = 0;
            double y = 0;
            double z = 0;

            pts.Add(new Point(x, y, z));//p1
            pts.Add(new Point(x + mSide, y, z));//p2
            pts.Add(new Point(x + mSide, y + mSide, z));//p3
            pts.Add(new Point(x, y + mSide, z));//p4
            pts.Add(new Point(x, y, z));//p1
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x, y, z + mSide));//p5
            pts.Add(new Point(x + mSide, y, z + mSide));//p6
            pts.Add(new Point(x + mSide, y + mSide, z + mSide));//p7
            pts.Add(new Point(x, y + mSide, z + mSide));//p8
            pts.Add(new Point(x, y, z + mSide));//p1
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x, y, z));//p1
            pts.Add(new Point(x, y, z + mSide));//p5
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x + mSide, y, z));//p2
            pts.Add(new Point(x + mSide, y, z + mSide));//p6
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x + mSide, y + mSide, z));//p3
            pts.Add(new Point(x + mSide, y + mSide, z + mSide));//p7
            vec.Add(pts);
            pts.Clear();

            pts.Add(new Point(x, y + mSide, z));//p4
            pts.Add(new Point(x, y + mSide, z + mSide));//p8
            vec.Add(pts);
            pts.Clear();

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
